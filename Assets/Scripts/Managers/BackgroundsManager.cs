using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct GameTypeBackgroundsData
{
    public GameType gameType;
    public string fileNamePrefix, iconNamePrefix, path;
    public int count, price;

    [HideInInspector] public int selectedItemIndex;

    [HideInInspector] public bool[] itemsAvailableStates;

    public string GetSaveKey(int backgroundIndex)
    {
        return "Background" + gameType.ToString() + "_" + backgroundIndex.ToString(); 
    }

    public string GetSelectedItemSaveKey()
    {
        return "Background" + gameType.ToString() + "_Selected";
    }
}

public class BackgroundsManager : Singleton<BackgroundsManager>
{
    [SerializeField] private GameTypeBackgroundsData[] backgroundsData;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < backgroundsData.Length; i++)
        {
            GameTypeBackgroundsData gameTypeBackgroundsData = backgroundsData[i];

            bool[] itemsAvailableStates = new bool[gameTypeBackgroundsData.count];
            for (int backgroundIndex = 0; backgroundIndex < itemsAvailableStates.Length; backgroundIndex++)
            {
                itemsAvailableStates[backgroundIndex] = backgroundIndex == 0 || PlayerSaves.GetInt(gameTypeBackgroundsData.GetSaveKey(backgroundIndex), 0) == 1;
            }

            gameTypeBackgroundsData.itemsAvailableStates = itemsAvailableStates;
            gameTypeBackgroundsData.selectedItemIndex = PlayerSaves.GetInt(gameTypeBackgroundsData.GetSelectedItemSaveKey(), 0);

            backgroundsData[i] = gameTypeBackgroundsData;
        }
    }

    public bool Buy(GameType gameType, int backgroundIndex)
    {
        int dataIndex = FindDataIndex(gameType);
        GameTypeBackgroundsData gameTypeBackgroundsData = backgroundsData[dataIndex];

        if (!ResourcesManager.Instance.HasCoins(gameTypeBackgroundsData.price))
        {
            return false;
        }

        ResourcesManager.Instance.SubtractCoins(gameTypeBackgroundsData.price);

        bool[] itemsAvailableStates = gameTypeBackgroundsData.itemsAvailableStates;
        itemsAvailableStates[backgroundIndex] = true;
        gameTypeBackgroundsData.itemsAvailableStates = itemsAvailableStates;
        backgroundsData[dataIndex] = gameTypeBackgroundsData;

        PlayerSaves.SetInt(gameTypeBackgroundsData.GetSaveKey(backgroundIndex), 1);

        return true;
    }

    public void Select(GameType gameType, int backgroundIndex)
    {
        int dataIndex = FindDataIndex(gameType);
        GameTypeBackgroundsData gameTypeBackgroundsData = backgroundsData[dataIndex];

        gameTypeBackgroundsData.selectedItemIndex = backgroundIndex;
        backgroundsData[dataIndex] = gameTypeBackgroundsData;

        PlayerSaves.SetInt(gameTypeBackgroundsData.GetSelectedItemSaveKey(), backgroundIndex);
    }

    private int FindDataIndex(GameType gameType)
    {
        for (int i = 0; i < backgroundsData.Length; i++)
        {
            GameTypeBackgroundsData gameTypeBackgroundsData = backgroundsData[i];
            if(gameTypeBackgroundsData.gameType == gameType)
            {
                return i;
            }
        }

        return -1;
    }

    public GameTypeBackgroundsData GetData(GameType gameType)
    {
        int dataIndex = FindDataIndex(gameType);
        return backgroundsData[dataIndex];
    }

    public Sprite LoadIcon(GameTypeBackgroundsData gameTypeBackgroundsData, int iconIndex)
    {
        string path = Path.Combine("Backgrounds", gameTypeBackgroundsData.path, gameTypeBackgroundsData.iconNamePrefix + iconIndex.ToString());
        return Resources.Load<Sprite>(path);
    }

    public Sprite LoadBackground(GameTypeBackgroundsData gameTypeBackgroundsData, int backgroundIndex)
    {
        string path = Path.Combine("Backgrounds", gameTypeBackgroundsData.path, gameTypeBackgroundsData.fileNamePrefix + backgroundIndex.ToString());
        return Resources.Load<Sprite>(path);
    }

    public Sprite LoadSelectedBackground(GameType gameType)
    {
        GameTypeBackgroundsData gameTypeBackgroundsData = GetData(gameType);
        return LoadBackground(gameTypeBackgroundsData, gameTypeBackgroundsData.selectedItemIndex);
    }
}
