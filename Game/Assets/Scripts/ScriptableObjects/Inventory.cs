using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfMoney;

    public void AddItem(Item itemToAdd)
    {
        // Is the item a key?
        if(itemToAdd.isMoney)
        {
            numberOfMoney++;
        }
        else
        {
            if(!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }
}
