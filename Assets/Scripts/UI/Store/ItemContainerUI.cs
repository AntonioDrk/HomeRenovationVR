using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemContainerUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemPriceText; 
    [SerializeField] private Button itemBuyButton; 
    
    public void SetItemImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }

    public void SetItemName(string txt)
    {
        itemNameText.text = txt;
    }
    
    public void SetItemPrice(int price)
    {
        itemPriceText.text = price.ToString();
    }
    
    public void OnBuyItem(UnityAction<Item> action, Item item)
    {
        itemBuyButton.onClick.RemoveAllListeners();
        itemBuyButton.onClick.AddListener(() => action.Invoke(item));
    }
}
