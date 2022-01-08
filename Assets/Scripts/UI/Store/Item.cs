using UnityEngine;

[System.Serializable]
public class Item
{ 
    public string name;
    public GameObject prefab;
    public Sprite image;
    public int price; 
    public ItemCategory category;
}
