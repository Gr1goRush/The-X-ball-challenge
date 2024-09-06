using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameTypeBackgroundsShopItem : MonoBehaviour
{
    [SerializeField] private GameObject pricePanel, statePanel;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI priceText, stateText;
    [SerializeField] private Button _button;

    [SerializeField] private Color selectedColor, defaultColor;

    public void SetIcon(Sprite icon)
    {
        iconImage.sprite = icon;
    }

    public void SetPrice(int price)
    {
        priceText.text = price.ToString();
        pricePanel.SetActive(true);
        statePanel.SetActive(false);
    }

    public void SetState(bool selected)
    {
        stateText.text = selected ? "Selected" : "Select";
        stateText.color = selected ? selectedColor : defaultColor;
        pricePanel.SetActive(false);
        statePanel.SetActive(true);
    }

    public void AddClickListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
}
