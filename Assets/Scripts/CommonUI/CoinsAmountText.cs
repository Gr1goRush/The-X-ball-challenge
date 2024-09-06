using System.Collections;
using TMPro;
using UnityEngine;

public class CoinsAmountText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _text;

    void Start()
    {
        SetCoins(ResourcesManager.Instance.Coins);

        ResourcesManager.Instance.OnCoinsAmountChanged += SetCoins;
    }

    void SetCoins(int amount)
    {
        _text.text = amount.ToString();
    }

    private void OnDestroy()
    {
        if (ResourcesManager.Instance != null)
        {
            ResourcesManager.Instance.OnCoinsAmountChanged -= SetCoins;
        }
    }
}