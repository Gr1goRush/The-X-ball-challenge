using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModePanel : SwitchingPanel
{
    public override string PanelName => panelName;

    public const string panelName = "game_mode";

    public void SelectEasyMode()
    {
        SelectMode(GameMode.Easy);
    }

    public void SelectHardMode()
    {
        SelectMode(GameMode.Hard);
    }

    public void SelectTimeMode()
    {
        SelectMode(GameMode.Time);
    }

    private void SelectMode(GameMode gameMode)
    {
        MenuController.Instance.SelectGameMode(gameMode);
    }

    public void Back()
    {
        MenuController.Instance.PanelSwitcher.ShowPanel(GameTypePanel.panelName);
    }
}
