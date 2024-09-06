using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTypeBackgroundsShop : MonoBehaviour
{
    public GameType gameType;

    [SerializeField] private bool updateOnEnable = true;
    [SerializeField] private GameTypeBackgroundsShopItem itemOriginal;

    private List<GameTypeBackgroundsShopItem> items;

    void OnEnable()
    {
        if (updateOnEnable)
        {
            UpdateItems();
        }
    }

    public void UpdateItems()
    {
        GameTypeBackgroundsData backgroundsData = BackgroundsManager.Instance.GetData(gameType);

        if(items == null)
        {
            items = new List<GameTypeBackgroundsShopItem>();
        }

        int lastItemIndex = 0;
        for (int i = 0; i < backgroundsData.count; i++)
        {
            GameTypeBackgroundsShopItem shopItem;
            if(i < items.Count)
            {
                shopItem = items[i];
            }
            else
            {
                shopItem = Instantiate(itemOriginal, itemOriginal.transform.parent);
                items.Add(shopItem);

                int index = i;
                shopItem.AddClickListener(() => ClickItem(index));
            }

            Sprite icon = BackgroundsManager.Instance.LoadIcon(backgroundsData, i);
            shopItem.SetIcon(icon);

            SetItem(shopItem, i, backgroundsData);

            shopItem.gameObject.SetActive(true);
            lastItemIndex = i;
        }

        for (int i = lastItemIndex + 1; i < items.Count; i++)
        {
            items[i].gameObject.SetActive(false);
        }

        itemOriginal.gameObject.SetActive(false);
    }

    private void ClickItem(int index)
    {
        GameTypeBackgroundsData backgroundsData = BackgroundsManager.Instance.GetData(gameType);
        if (backgroundsData.itemsAvailableStates[index])
        {
            BackgroundsManager.Instance.Select(gameType, index);
            backgroundsData.selectedItemIndex = index;

            for (int i = 0; i < backgroundsData.count; i++)
            {
                SetItem(items[i], i, backgroundsData);
            }
        }
        else
        {
            if (BackgroundsManager.Instance.Buy(gameType, index))
            {
                items[index].SetState(false);
            }
        }
    }

    private void SetItem(GameTypeBackgroundsShopItem shopItem, int index, GameTypeBackgroundsData backgroundsData)
    {
        bool available = backgroundsData.itemsAvailableStates[index];
        if (available)
        {
            bool selected = backgroundsData.selectedItemIndex == index;
            shopItem.SetState(selected);
        }
        else
        {
            shopItem.SetPrice(backgroundsData.price);
        }
    }
}
