using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTypePanel : SwitchingPanel
{
    public override string PanelName => panelName;

    public const string panelName = "game_type";

    public void SelectBasketballTeam()
    {
        MenuController.Instance.SelectGameType(GameType.Basketball, PlayersCount.TwoPlayers);
    }

    public void SelectBasketballSolo()
    {
        MenuController.Instance.SelectGameType(GameType.Basketball, PlayersCount.Single);
    }

    public void SelectFootballTeam()
    {
        MenuController.Instance.SelectGameType(GameType.Football, PlayersCount.TwoPlayers);
    }

    public void SelectFootballSolo()
    {
        MenuController.Instance.SelectGameType(GameType.Football, PlayersCount.Single);
    }

    public void ShowSettingsPanel()
    {
        CommonUIManager.Instance.ShowSettings(false);
    }

    public void ShowShopPanel()
    {
        MenuController.Instance.PanelSwitcher.ShowPanel(ShopPanel.panelName);
    }
}
