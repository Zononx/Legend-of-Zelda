using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : MonoBehaviour
{
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door[] thisDoor;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storedValue.RuntimeValue;
        if(active)
        {
            ActivateSwitch();
        }
    }
    public void ActivateSwitch()
    {
        active = true;
        storedValue.RuntimeValue = active;
        for (int i = 0; i < thisDoor.Length; i++)
        {
            thisDoor[i].Open();
        }
        mySprite.sprite = activeSprite;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if it the player?
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            ActivateSwitch();
        }
    }
}
