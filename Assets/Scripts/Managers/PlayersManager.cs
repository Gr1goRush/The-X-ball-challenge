using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    public string name;
    public int portraintIndex;
}

public class PlayersManager : Singleton<PlayersManager>
{
    public int PlayersCount => players.Length;

    [SerializeField] private Sprite[] portraits;

    private PlayerData[] players;

    protected override void Awake()
    {
        base.Awake();

        if(players == null || players.Length == 0)
        {
            players = new PlayerData[]
            {
                new PlayerData
                {
                    name = "",
                    portraintIndex = GetRandomPortraitIndex(),
                },
                new PlayerData
                {
                    name = "",
                    portraintIndex = -1,
                }
            };
        }
    }

    public PlayerData GetPlayerData(int playerIndex)
    {
        return players[playerIndex];
    }

    public void SetPlayerData(int playerIndex, PlayerData playerData)
    {
        players[playerIndex] = playerData;
    }

    public Sprite GetPortrait(int index)
    {
        return portraits[index];
    }

    public int GetPortraitsCount()
    {
        return portraits.Length;
    }

    public int GetRandomPortraitIndex()
    {
        return Random.Range(0, portraits.Length);
    }
}
