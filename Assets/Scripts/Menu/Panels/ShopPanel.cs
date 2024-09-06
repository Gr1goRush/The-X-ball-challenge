using System.Collections;
using UnityEngine;

public class ShopPanel : SwitchingPanel
{
    public override string PanelName => panelName;

    public const string panelName = "shop";

    public void Back()
    {
        MenuController.Instance.PanelSwitcher.ShowPanel(GameTypePanel.panelName);
    }
}