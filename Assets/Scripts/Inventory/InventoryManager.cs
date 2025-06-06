using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //textmeshpro

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot; //vi tri o trong trong kho do
	[SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
	[SerializeField] private GameObject useButton;
    public InventoryItem currentItem;

    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    public void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                if (playerInventory.myInventory[i].numberHeld > 0 || playerInventory.myInventory[i].itemName == "Bottle")
                {
					GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
					temp.transform.SetParent(inventoryPanel.transform);
					temp.transform.localScale = Vector3.one; // dam bao scale luon = 1; loi do canvas bi scale default la 0.61 nen child bi +0.61 = 1.61
					InventorySlot newSlot = temp.GetComponent<InventorySlot>();
					if (newSlot)
					{
						newSlot.Setup(playerInventory.myInventory[i], this);
					}
				}
            }
        }
    }

	// Start is called before the first frame update
	void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("",false);
	}

    public void SetupDescriptionAndButton(string newDescriptionString, bool isButtonUsable, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescriptionString;
        useButton.SetActive(isButtonUsable);
    }

    void ClearInventorySlots()
    {
		for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }

	}

    public void UseButtonPressed()
    {
        if (currentItem)
        {
            currentItem.Use();
            //Clear all of the inventory slots
            ClearInventorySlots();
            //Refill all slots with new numbers
            MakeInventorySlots();
            if (currentItem.numberHeld == 0)
            {
				SetTextAndButton("", false);
			}
		}
    }
}
