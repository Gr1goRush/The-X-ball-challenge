using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public struct PlayerGameState
{
    public int points, health;
}

public class GameController : Singleton<GameController>
{
    public bool Paused { get; private set; }

    public Playground Playground => playground;

    [SerializeField] private int hitPoints = 3, hitCoins = 1, randomizeGoalPointsCondition = 15, obstaclesPointsCondition = 10,  gameTime = 120;

    [SerializeField] private Playground playground;

    private bool gameIsActive = false, healthWasAddedLastRound = false;
    private int movePlayerIndex = 0;

    private PlayerGameState[] playersStates;

    private const int maxHealth = 3;

    void Start()
    {
        Paused = false;

        int playersCount = GameModeManager.Instance.GetPlayersCountAsNumber();

        playersStates = new PlayerGameState[playersCount];
        for (int i = 0; i < playersStates.Length; i++)
        {
            playersStates[i] = new PlayerGameState
            {
                points = 0,
                health = maxHealth
            };
        }

        UIController.Instance.TopPanel.ShowName(-1);
        UIController.Instance.TopPanel.HideTime();
        UIController.Instance.SetMovingPlayerName(null);

        BeginGame();
    }

    IEnumerator Countdown()
    {
        UIController.Instance.TopPanel.SetTime(gameTime);
        UIController.Instance.TopPanel.ShowTime();

        for (int currentTime = gameTime; currentTime >= 0; currentTime--)
        {
            if(!gameIsActive)
            {
                 yield break;
            }

            UIController.Instance.TopPanel.SetTime(currentTime);

            yield return new WaitForSeconds(1f);

            if(currentTime <= 0)
            {
                if (TwoPlayers())
                {
                    int nextMovePlayerIndex = movePlayerIndex + 1;
                    if (nextMovePlayerIndex < GameModeManager.Instance.GetPlayersCountAsNumber())
                    {
                        movePlayerIndex = nextMovePlayerIndex;
                        currentTime = gameTime + 1;
                        NextRound();
                    }else
                    {
                        StopTwoPlayersGame();
                        yield break;
                    }

                }
            }
        }

        StopGame();
        UIController.Instance.CompletePanel.ShowTimeout();
    }

    private void StopTwoPlayersGame()
    {
        int winnerPlayer = -1;
        int winnerPoints = 0;
        for (int i = 0; i < playersStates.Length; i++)
        {
            if (playersStates[i].points > winnerPoints)
            {
                winnerPoints = playersStates[i].points;
                winnerPlayer = i;
            }
        }

        if(winnerPlayer >= 0)
        {
            UIController.Instance.CompletePanel.ShowWinner(PlayersManager.Instance.GetPlayerData(winnerPlayer).name);
        }
        else
        {
            UIController.Instance.CompletePanel.ShowLose();
        }
    }

    private void BeginGame()
    {
        gameIsActive = true;

        InputController.Instance.OnSwipeCompleted += OnSwipeCompleted;
        InputController.Instance.OnSwipeCancel += OnSwipeCancel;
        InputController.Instance.OnSwipeStarted += OnSwipeStarted;

        if (IsTimeMode())
        {
            StartCoroutine(Countdown());
        }

        NextRound();
    }

    //private bool AnyPlayerReachedPointsCount(int count)
    //{
    //    foreach (var item in playersStates)
    //    {
    //        if (item.points >= count)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    private bool MovePlayerReachedPointsCount(int count)
    {
        return playersStates[movePlayerIndex].points >= count;
    }

    public void AddHealth(int amount)
    {
        playersStates[movePlayerIndex].health += amount;
        UIController.Instance.TopPanel.SetHealth(playersStates[movePlayerIndex].health);

        if(amount > 0)
        {
            healthWasAddedLastRound = true;
        }
    }

    private bool RandomizeBallPosition()
    {
        return MovePlayerReachedPointsCount(randomizeGoalPointsCondition);
    }

    private bool ActivateObstacles()
    {
        return MovePlayerReachedPointsCount(obstaclesPointsCondition);
    }

    public void Restart()
    {
        NextRound();
    }

    public void NextRound()
    {
        healthWasAddedLastRound = false;

        playground.Ball.SetDefault();

        if (RandomizeBallPosition())
        {
            playground.Ball.SetRandomPosition();
        }
        else
        {
            playground.Ball.SetDefaultPosition();
        }
        CameraController.Instance.SetPosition(playground.Ball.GetPosition());

        if (IsHardMode() && playersStates[movePlayerIndex].health < maxHealth)
        {
            playground.HealthSpawnSystem.Spawn();
        }
        else
        {
            playground.HealthSpawnSystem.Hide();
        }

        playground.SetObstaclesActive(ActivateObstacles());

        if (TwoPlayers())
        {
            UIController.Instance.SetMovingPlayerName(PlayersManager.Instance.GetPlayerData(movePlayerIndex).name);
            UIController.Instance.TopPanel.ShowName(movePlayerIndex);
        }
        UIController.Instance.SetDragImageActive(true);
    }

    private void OnSwipeStarted()
    {
        UIController.Instance.SetDragImageActive(false);
    }

    private void OnSwipeCancel()
    {
        UIController.Instance.SetDragImageActive(true);
    }

    private void OnSwipeCompleted(Vector2 distance)
    {
        if (!gameIsActive || playground.Ball.Throwing || Paused)
        {
            return;
        }

        Vector3 force = distance / Screen.dpi;
        force.z = force.y;

        playground.Ball.Throw(force);
    }

    public void StopRound(bool hit)
    {
        if (!gameIsActive)
        {
            return;
        }

        if (hit)
        {
            VibrationManager.Instance.Vibrate();

            playersStates[movePlayerIndex].points += hitPoints;

            if (!TwoPlayers())
            {
                ResourcesManager.Instance.AddCoins(hitCoins);
            }

            UIController.Instance.TopPanel.SetScore(movePlayerIndex, playersStates[movePlayerIndex].points);
        }
        else
        {
            if (IsHardMode() && !healthWasAddedLastRound)
            {
                AddHealth(-1);

                if(playersStates[movePlayerIndex].health <= 0)
                {
                    StopGame();
                    UIController.Instance.CompletePanel.ShowLose();
                    return;
                }
            }
        }

        NextRound();
    }

    private void StopGame()
    {
        if (!gameIsActive)
        {
            return;
        }

        gameIsActive = false;

        playground.Ball.gameObject.SetActive(false);
        playground.GoalContainer.gameObject.SetActive(false);

        UIController.Instance.SetDragImageActive(false);
    }

    public void PauseGame()
    {
        Paused = true;
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Paused = false;
        Time.timeScale = 1f;
    }

    private bool TwoPlayers()
    {
        return GameModeManager.Instance.PlayersCount == PlayersCount.TwoPlayers;
    }

    private bool IsHardMode()
    {
        return GameModeManager.Instance.GameMode == GameMode.Hard;
    }

    private bool IsTimeMode()
    {
        return GameModeManager.Instance.GameMode == GameMode.Time;
    }

    private void OnDestroy()
    {
        if (Paused)
        {
            UnPauseGame();
        }
    }
}
