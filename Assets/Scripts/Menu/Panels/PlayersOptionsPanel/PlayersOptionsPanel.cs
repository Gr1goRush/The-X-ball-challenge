using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayersOptionsPanel : SwitchingPanel
{
    public override string PanelName => panelName;

    public const string panelName = "players_options";

    [SerializeField] private int defaultHeight, heightWithShop;

    [SerializeField] private TextMeshProUGUI playerIndexText;
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private Transform portraitsParent;
    [SerializeField] private PlayersOptionsPanelPortraitItem portraitItemOriginal;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameTypeBackgroundsShop backgroundsShop;
    [SerializeField] private RectTransform frame;

    private int selectedPortraitIndex = 0, playerIndex = 0;
    private bool initialized = false;

    private PlayersOptionsPanelPortraitItem[] portraitItems;

    void OnEnable()
    {
        if(!initialized)
        {
            int portraitsCount = PlayersManager.Instance.GetPortraitsCount();
            portraitItems = new PlayersOptionsPanelPortraitItem[portraitsCount];
            for (int i = 0; i < portraitsCount; i++)
            {
                PlayersOptionsPanelPortraitItem newItem = i == 0 ? portraitItemOriginal : Instantiate(portraitItemOriginal, portraitsParent);
                newItem.Set(i);

                int index = i;
                newItem.SetClickListener(() => SelectPortrait(index));

                portraitItems[i] = newItem;
            }

            nameField.onValueChanged.AddListener(NameInputValueChanged);

            initialized = true;
        }

        playerIndex = 0;
        SetDefaultForm();
    }

    private void SetDefaultForm()
    {
        SelectPortrait(0);
        nameField.text = string.Empty;
        NameInputValueChanged(string.Empty);

        playerIndexText.text = "Player " + (playerIndex + 1).ToString();

        Vector2 size = frame.sizeDelta;
        if(playerIndex == 0)
        {
            backgroundsShop.gameObject.SetActive(true);
            backgroundsShop.gameType = GameModeManager.Instance.GameType;
            backgroundsShop.UpdateItems();

            size.y = heightWithShop;
        }
        else
        {
            backgroundsShop.gameObject.SetActive(false);

            size.y = defaultHeight;
        }

        frame.sizeDelta = size;
    }

    private void SelectPortrait(int index)
    {
        selectedPortraitIndex = index;
        for (int i = 0; i < portraitItems.Length; i++)
        {
            portraitItems[i].SetSelected(i == selectedPortraitIndex);
        }
    }

    private void NameInputValueChanged(string v)
    {
        continueButton.interactable = !string.IsNullOrWhiteSpace(v);
    }

    public void Continue()
    {
        PlayersManager.Instance.SetPlayerData(playerIndex, new PlayerData
        {
            name = nameField.text,
            portraintIndex = selectedPortraitIndex,
        });

        playerIndex++;
        if(playerIndex >= PlayersManager.Instance.PlayersCount)
        {
            MenuController.Instance.LoadGame();
            return;
        }

        SetDefaultForm();
    }

    public void Back()
    {
        MenuController.Instance.PanelSwitcher.ShowPanel(GameTypePanel.panelName);
    }
}
