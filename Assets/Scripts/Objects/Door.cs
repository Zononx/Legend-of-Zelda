using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;

    public PlayerInventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public InventoryItem smallKey;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(playerInRange && thisDoorType == DoorType.key)
            {
                if (playerInventory.IsItemInInventory(smallKey))
                {
                    if(playerInventory.canUseItem(smallKey))
                    {
                        playerInventory.UseItem(smallKey);
                        Open();
                    }
                }
            }
        }
    }
    public void Open()
    {
        //Turn off the door's sprite
        doorSprite.enabled = false;
        //set open to true
        open = true;
        //turn off the door's box collider 
        physicsCollider.enabled = false;
    }
    public void Close() 
    {
        //Turn off the door's sprite
        doorSprite.enabled = true;
        //set open to true
        open = false;
        //turn off the door's box collider 
        physicsCollider.enabled = true;
    }
}
