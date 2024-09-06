using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : Singleton<MenuController>
{
    public PanelSwitcher PanelSwitcher => panelSwitcher;
    [SerializeField] private PanelSwitcher panelSwitcher;

    private void Start()
    {
        panelSwitcher.ShowPanel(GameTypePanel.panelName);
    }

    public void SelectGameType(GameType gameType, PlayersCount playersCount)
    {
        GameModeManager.Instance.GameType = gameType;
        GameModeManager.Instance.PlayersCount = playersCount;

        if(playersCount == PlayersCount.TwoPlayers)
        {
            GameModeManager.Instance.GameMode = GameMode.Time;

            panelSwitcher.ShowPanel(PlayersOptionsPanel.panelName);
        }
        else
        {
            panelSwitcher.ShowPanel(GameModePanel.panelName);
        }
    }

    public void SelectGameMode(GameMode gameMode)
    {
        GameModeManager.Instance.GameMode = gameMode;

        LoadGame();
    }

    public void LoadGame()
    {
        ScenesLoader.LoadGame();
    }
}
