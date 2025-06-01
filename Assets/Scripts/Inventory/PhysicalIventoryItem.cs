using UnityEngine;

public class PhysicalIventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            AddItemToInventory();
            Destroy(this.gameObject);
        }
	}

	//them vat pham vao kho do sau khi nhat o overworld
	void AddItemToInventory()
    {
        if (playerInventory && thisItem)
        {
            if (playerInventory.myInventory.Contains(thisItem))
            {
				thisItem.numberHeld += 1;
			}
			else
			{
				playerInventory.myInventory.Add(thisItem);
				thisItem.numberHeld += 1;
			}
		}
        
    }
}
