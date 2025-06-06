using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    //textmeshpro
using UnityEngine.UI; 

public class InventorySlot : MonoBehaviour
{
	// SerializeField private: Bien khai bao private nhung van co the duoc chinh sua ben trong Inspector
	[Header("UI Stuff to change")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variables from the item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = "" + thisItem.numberHeld;
        }
    }

    public void ClickedOn()
    {
		if (thisItem)
        {
            
            thisManager.SetupDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem);
        }
    }
}
