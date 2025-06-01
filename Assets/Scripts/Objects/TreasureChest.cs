using UnityEngine;

public class TreasureChest : Interactable
{
	[SerializeField] private AnimatorController anim;
	[SerializeField] private BoolValue openValue;
	[SerializeField] private bool isOpen;
	[SerializeField] private SignalSender chestNotification;
	[SerializeField] private SpriteValue spriteValue;
	[SerializeField] private StringValue itemString;
	[SerializeField] private InventoryItem myItem;
	[SerializeField] private PlayerInventory playerInventory;

	// Start is called before the first frame update
	void Start()
	{
		isOpen = openValue.RuntimeValue;
		if (isOpen)
		{
			anim.SetAnimParameter("opened", true);
		}
	}
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && playerInRange)
		{
			if (isOpen)
			{
				return;
			}
			DisplayContents();
		}
	}
	void DisplayContents()
	{
		isOpen = !isOpen;
		anim.SetAnimParameter("opened", true);
		openValue.RuntimeValue = isOpen;
		spriteValue.value = myItem.itemImage;
		itemString.value = myItem.itemDescription;
		chestNotification.Raise();
		playerInventory.AddItem(myItem);
		myNotification.Raise();
	}
	public override void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag(otherTag) && !other.isTrigger)
		{
			playerInRange = true;
			if (!isOpen)
			{
				myNotification.Raise();
			}
		}
	}
	public override void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag(otherTag) && !other.isTrigger)
		{
			playerInRange = false;
			if (!isOpen)
			{
				myNotification.Raise();
			}
		}
	}







	//[Header("Contents")]
 //   public Item contents;
 //   public Inventory playerInventory;
 //   public bool isOpen;
 //   public BoolValue storedOpen;

 //   [Header("Signal and Dialog")]
 //   public SignalSender raiseItem;
 //   public GameObject dialogBox;
 //   public Text dialogText;

 //   [Header("Animator")]
 //   private Animator anim;

 //   // Start is called before the first frame update
 //   void Start()
 //   {
 //       anim = GetComponent<Animator>();
 //       isOpen = storedOpen.RuntimeValue;
        
 //       //neu chest isopen va load lai scene thi anim phai la opened
 //       if (isOpen)
 //       {
 //           anim.SetBool("opened", true);
 //       }
 //   }

 //   // Update is called once per frame
 //   void Update()
 //   {
 //       //if (Input.GetButtonDown("attack") && playerInRange)
 //       if (Input.GetKeyDown(KeyCode.E) && playerInRange)
 //       {
 //           if(!isOpen)
 //           {
 //               //Open the chest
 //               OpenChest();
 //           }
 //           else
 //           {
 //               //The chest is already open
 //               ChestAlreadyOpen();
 //           }
 //       }
 //   }
 //   public void OpenChest()
 //   {
 //       // Dialog box on
 //       dialogBox.SetActive(true);
 //       // Dialog text = contents text
 //       dialogText.text = contents.itemDescription;
 //       // add contents to the inventory
 //       playerInventory.AddItem(contents);
 //       playerInventory.currentItem = contents;
 //       // raise the signal to the player to animate
 //       raiseItem.Raise();
 //       // set the chest to opened
 //       isOpen = true;
 //       // raise to context clue
 //       context.Raise();
 //       //chest opened
 //       isOpen = true;
 //       anim.SetBool("opened",true);
 //       storedOpen.RuntimeValue = isOpen;
 //   }
 //   public void ChestAlreadyOpen()
 //   {
 //       // Dialog off
 //       dialogBox.SetActive(false);
 //       // raise the signal to the player to stop animating
 //       raiseItem.Raise();
 //   }
}
