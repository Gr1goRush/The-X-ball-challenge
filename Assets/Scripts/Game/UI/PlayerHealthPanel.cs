using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthPanel : MonoBehaviour
{
    [SerializeField] private Image[] items;

    [SerializeField] private Sprite emptySprite, fillSprite;

    public void SetHealth(int health)
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].sprite = i < health ? fillSprite : emptySprite;
        }
    }
}
