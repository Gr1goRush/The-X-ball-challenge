using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUIManager : Singleton<CommonUIManager>
{
    public SettingsPanel SettingsPanel => settingsPanel;
    [SerializeField] private SettingsPanel settingsPanel;

    public void ShowSettings(bool allowBackToMenu)
    {
        settingsPanel.allowBackToMenu = allowBackToMenu;
        settingsPanel.gameObject.SetActive(true);    
    }

}
