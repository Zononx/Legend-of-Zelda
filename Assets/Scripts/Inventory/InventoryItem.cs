using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
[System.Serializable]
public class InventoryItem : ScriptableObject
{
	public string itemName;
	public string itemDescription;
	public Sprite itemImage;
	public int numberHeld; //so luong vat pham
	public bool usable; //vat pham co the su dung?
	public bool unique; //vat pham duy nhat ?
	public UnityEvent thisEvent;

	public void Use()
	{
		//Debug.Log("Using Item");
		thisEvent.Invoke();
	}
	public void DecreaseAmount(int amountToDecrease)
	{
		numberHeld -= amountToDecrease;
		if (numberHeld < 0)
		{
			numberHeld = 0;
		}
	}
}
