using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CategoryContainerUI : MonoBehaviour
{
    [SerializeField] private Text categoryTitle;
    [SerializeField] private Button selectCategoryBtn; 
    
    public void SetCategoryTitle(string txt)
    {
        categoryTitle.text = txt;
    }
    
    public void OnSelectCategory(UnityAction<ItemCategory> action, ItemCategory category)
    {
        selectCategoryBtn.onClick.RemoveAllListeners();
        selectCategoryBtn.onClick.AddListener(() => action.Invoke(category));
    }
}