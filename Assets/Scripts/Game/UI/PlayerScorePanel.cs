using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScorePanel : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image portraitImage;

    public void SetScore(int score)
    {
        scoreText.text = score.ToString("00");
    }

    public void SetPortrait(int portraitIndex)
    {
        if(portraitIndex < 0)
        {
            portraitImage.sprite = null;
        }
        else
        {
            portraitImage.sprite = PlayersManager.Instance.GetPortrait(portraitIndex);
        }
    }
}