using System;
using UnityEngine;

public class UIStoreManager : MonoBehaviour
{
	[SerializeField] private ItemsStoreDatabase itemsDB;
	
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private GameObject itemContainerPrefab;
    
    [SerializeField] private Transform categoriesContainer;
    [SerializeField] private GameObject categoryContainerPrefab;

    private ItemCategory currentCategory;

    private void Start()
    {
	    GenerateStoreCategoriesUI();
	    GenerateStoreItemsUI(currentCategory);
    }

    private Vector3 GetCurrentPosition()
    {
        return new Vector3(0, 0, 0);
    }

    private void TakeMoney(int price)
    {
        // money -= price;
    }

    void GenerateStoreCategoriesUI()
    {
	    var categories = Enum.GetValues(typeof(ItemCategory));
	    foreach (var category in categories)
	    {
		    CategoryContainerUI categoryContainer = Instantiate(categoryContainerPrefab, categoriesContainer)
			    .GetComponent<CategoryContainerUI>();
		    
		    categoryContainer.gameObject.name = "Category-" + category;
		    categoryContainer.SetCategoryTitle(category.ToString());
		    
		    categoryContainer.OnSelectCategory(DisplayCategory, (ItemCategory)category);
	    }
    }
    
    void GenerateStoreItemsUI(ItemCategory category)
    {
	    currentCategory = category;
		for (int i = 0; i < itemsDB.itemsCount; i++)
		{
			Item item = itemsDB.GetItem(i);
			if (item.category == category)
			{
				ItemContainerUI itemContainer =
					Instantiate(itemContainerPrefab, itemsContainer).GetComponent<ItemContainerUI>();
				
				itemContainer.gameObject.name = "Item" + i + "-" + item.name;
				itemContainer.SetItemImage(item.image);
				itemContainer.SetItemName(item.name);
				itemContainer.SetItemPrice(item.price);

				itemContainer.OnBuyItem(BuyItem, item);
			}
		}
	}
    
    private void BuyItem(Item item)
    {
	    // check money >= price
	    TakeMoney(item.price);
	    Instantiate(item.prefab, GetCurrentPosition(), Quaternion.identity);
	    MissionsManager.Instance.CheckMission(ActionType.BUY, item.name);
    }
    
    private void DisplayCategory(ItemCategory category)
    {
	    if (currentCategory == category)
	    {
		    return;
	    }
	    
	    DestroyStoreItemsUI();
	    GenerateStoreItemsUI(category);
    }

    private void DestroyStoreItemsUI()
    {
	    for(int i=0; i<itemsContainer.childCount; i++)
	    {	
		    Destroy(itemsContainer.GetChild(i).gameObject);
	    }
    }
}
