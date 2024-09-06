using System.Collections;
using UnityEngine;

public delegate void GameActionInt(int value);

public class ResourcesManager : Singleton<ResourcesManager>
{

    public int Coins { get; private set; }

    public event GameActionInt OnCoinsAmountChanged;

    protected override void Awake()
    {
        base.Awake();

        Coins = PlayerSaves.GetInt("Coins", 0);
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        PlayerSaves.SetInt("Coins", Coins);

        OnCoinsAmountChanged?.Invoke(Coins);
    }

    public void SubtractCoins(int amount)
    {
        AddCoins(-amount);
    }

    public bool HasCoins(int amount)
    {
        return Coins >= amount;
    }
}