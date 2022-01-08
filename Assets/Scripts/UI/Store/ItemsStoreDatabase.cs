using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ItemsStoreDatabase", menuName = "Create Database/Items store database")]
public class ItemsStoreDatabase : ScriptableObject
{
    [SerializeField] private List<Item> items;

    public int itemsCount => items.Count;

    public Item GetItem(int id)
    {
        return items[id];
    }
}