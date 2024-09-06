using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameType
{
    Basketball, Football
}

public enum GameMode
{
    Easy, Hard, Time
}

public enum PlayersCount
{
    Single, TwoPlayers
}

public class GameModeManager : Singleton<GameModeManager>
{
    [HideInInspector] public GameType GameType = GameType.Basketball;
    [HideInInspector] public GameMode GameMode = GameMode.Easy;
    [HideInInspector] public PlayersCount PlayersCount = PlayersCount.Single;

    public int GetPlayersCountAsNumber()
    {
        return PlayersCount == PlayersCount.TwoPlayers ? 2 : 1;
    }
}
