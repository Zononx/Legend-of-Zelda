using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
	public List<InventoryItem> myInventory = new List<InventoryItem>();
	public void AddItem(InventoryItem newItem)
	{
		if (!myInventory.Contains(newItem))
		{
			myInventory.Add(newItem);
		}
		else
		{
			newItem.numberHeld++;
		}
	}
	public void RemoveItem(InventoryItem newItem)
	{
		if (myInventory.Contains(newItem))
		{
			myInventory.Remove(newItem);
		}
	}
	public void UseItem(InventoryItem newItem)
	{
		if (myInventory.Contains(newItem))
		{
			if (newItem.numberHeld > 0)
			{
				newItem.numberHeld--;
			}
		}
	}
	public bool IsItemInInventory(InventoryItem newItem)
	{
		return myInventory.Contains(newItem);
	}

	public bool canUseItem(InventoryItem newItem)
	{
		return newItem.numberHeld > 0;
	}
}
