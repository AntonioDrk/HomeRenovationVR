using System;
using UnityEngine;

public class UIStoreManager : MonoBehaviour
{
	[SerializeField] private ItemsStoreDatabase itemsDB;
	
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private GameObject itemContainerPrefab;
    
    [SerializeField] private Transform categoriesContainer;
    [SerializeField] private GameObject categoryContainerPrefab;

    [SerializeField] private Transform furnitureContainer;
    
    private ItemCategory currentCategory;

    private void Start()
    {
	    GenerateStoreCategoriesUI();
	    GenerateStoreItemsUI(currentCategory);
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
	    
	    var obj = Instantiate(item.prefab, furnitureContainer);
	    obj.name = item.name;
	    
	    Mission mission = MissionsManager.Instance.CheckMission(ActionType.BUY, item.name);
	    if (mission != null)
	    {
		    MissionsManager.Instance.OnMissionDone(mission);
	    }
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
