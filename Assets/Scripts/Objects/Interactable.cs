using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class: vat co the dc player tuong tac
public class Interactable : MonoBehaviour
{
	[SerializeField] public bool playerInRange;
	[SerializeField] public SignalSender myNotification;
	[SerializeField] public string otherTag;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
			myNotification.Raise();
            playerInRange = true;
        }
    }
	public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
			myNotification.Raise();
            playerInRange = false;
        }
    }
}
