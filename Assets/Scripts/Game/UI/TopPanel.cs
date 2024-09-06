using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private PlayerHealthPanel playerHealthPanel;
    [SerializeField] private GameObject coinsPanel;

    [SerializeField] private PlayerScorePanel[] playersScorePanels;
    [SerializeField] private TextMeshProUGUI[] playersNamesTexts;

    void Start()
    {
        bool twoPlayers = GameModeManager.Instance.PlayersCount == PlayersCount.TwoPlayers;

        playersScorePanels[1].gameObject.SetActive(twoPlayers);

        for (int i = 0; i < playersScorePanels.Length; i++)
        {
            PlayerData playerData = PlayersManager.Instance.GetPlayerData(i);

            PlayerScorePanel panel = playersScorePanels[i];
            panel.SetScore(0);
            panel.SetPortrait(playerData.portraintIndex);

            playersNamesTexts[i].text = playerData.name;
        }

        playerHealthPanel.gameObject.SetActive(GameModeManager.Instance.GameMode == GameMode.Hard);
        playerHealthPanel.SetHealth(3);

        coinsPanel.SetActive(!twoPlayers);
    }

    public void ShowTime()
    {
        timeText.transform.parent.gameObject.SetActive(true);
    }

    public void HideTime()
    {
        timeText.transform.parent.gameObject.SetActive(false);
    }

    public void SetTime(int seconds)
    {
        timeText.text = TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");
    }

    public void SetScore(int playerIndex, int score)
    {
        playersScorePanels[playerIndex].SetScore(score);
    }

    public void SetHealth(int health)
    {
        playerHealthPanel.SetHealth(health);
    }

    public void ShowName(int playerIndex)
    {
        for (int i = 0; i < playersNamesTexts.Length; i++)
        {
            playersNamesTexts[i].transform.parent.gameObject.SetActive(i == playerIndex);
        }
    }

    public void Pause()
    {
        GameController.Instance.PauseGame();

        CommonUIManager.Instance.SettingsPanel.OnDisabled += UnPause;
        CommonUIManager.Instance.ShowSettings(true);
    }

    public void UnPause()
    {
        CommonUIManager.Instance.SettingsPanel.OnDisabled -= UnPause;

        GameController.Instance.UnPauseGame();
    }

    public void Restart()
    {
        GameController.Instance.Restart();
    }

    private void OnDestroy()
    {
        if (CommonUIManager.Instance != null && CommonUIManager.Instance.SettingsPanel != null)
        {
            CommonUIManager.Instance.SettingsPanel.OnDisabled -= UnPause;
        }
    }
}
