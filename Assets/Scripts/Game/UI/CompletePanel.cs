using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompletePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bottomText;
    [SerializeField] private GameObject healthPanel;
    [SerializeField] private Image headerImage;

    [SerializeField] private Sprite timeoutSprite, loseSprite, winnerSprite;

    public void ShowTimeout()
    {
        headerImage.sprite = timeoutSprite;
        ShowText("00:00");
        gameObject.SetActive(true);
    }

    public void ShowWinner(string playerName)
    {
        headerImage.sprite = winnerSprite;
        ShowText(playerName);
        gameObject.SetActive(true);
    }

    public void ShowLose()
    {
        headerImage.sprite = loseSprite;

        bottomText.gameObject.SetActive(false);
        healthPanel.gameObject.SetActive(true);

        gameObject.SetActive(true);
    }

    public void Close()
    {
        ScenesLoader.LoadMenu();
    }

    private void ShowText(string text)
    {
        bottomText.gameObject.SetActive(true);
        bottomText.text = text;

        healthPanel.gameObject.SetActive(false);
    }
}
