using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayersOptionsPanelPortraitItem : MonoBehaviour
{
    [SerializeField] private Image borderImage, portraitImage;
    [SerializeField] private Button _button;

    public void Set(int portraitIndex)
    {
        portraitImage.sprite = PlayersManager.Instance.GetPortrait(portraitIndex);
    }

    public void SetSelected(bool selected)
    {
        Color color = Color.white;
        color.a = selected ? 1 : 0;
        borderImage.color = color;

        _button.interactable = !selected;
    }

    public void SetClickListener(UnityAction action)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(action);
    }
}
