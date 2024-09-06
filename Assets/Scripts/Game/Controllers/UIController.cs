using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    public TopPanel TopPanel => topPanel;
    public CompletePanel CompletePanel => completePanel;

    [SerializeField] private TopPanel topPanel;
    [SerializeField] private GameObject dragImage;
    [SerializeField] private TextMeshProUGUI movingPlayerNameText;
    [SerializeField] private CompletePanel completePanel;

    public void SetDragImageActive(bool v)
    {
        dragImage.SetActive(v);
    }

    public void SetMovingPlayerName(string playerName)
    {
        movingPlayerNameText.text = playerName;
        movingPlayerNameText.transform.parent.gameObject.SetActive(!string.IsNullOrWhiteSpace(playerName));
    }
}
