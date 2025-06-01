
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Sign : Interactable
{
	[SerializeField] private SignalSender signNotification;
	[SerializeField] private StringValue signText;
	[SerializeField] private string newSignText;
	[SerializeField] private bool dialogActive = false;


    // Update is called once per frame
    public virtual void Update()
    {
        //if (Input.GetButtonDown("attack") && playerInRange)
        if (Input.GetKeyDown(KeyCode.E) && playerInRange) 
        {
			//check active of obj dialog
			dialogActive = !dialogActive;
			signText.value = newSignText;
			signNotification.Raise();
		}
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
		base.OnTriggerExit2D(other);
		if (other.gameObject.CompareTag("Player") && !other.isTrigger)
		{
			if (dialogActive)
			{
				dialogActive = !dialogActive;
				signNotification.Raise();
			}
		}
	}

}
