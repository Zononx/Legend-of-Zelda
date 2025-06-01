using System.Collections;
using UnityEngine;

//enumeration la mot kieu du lieu liet ke, co the duoc truy cap o bat ki dau trong du an
//public enum GenericState
//{
//    walk,
//    attack,
//    interact, // kiem tra trang thai tuong tac voi do vat, se dung im
//    stagger,
//    idle
//}

public class PlayerMovement : Movement
{
	[SerializeField] private AnimatorController anim;
	[SerializeField] private StateMachine currentState;
	[SerializeField] private float WeaponAttackDuration;
    [SerializeField] private ReceiveItem myItem;

	private Vector2 facingDirection = Vector2.down;
	private Vector2 tempMovement = Vector2.down;

    public VectorValue startingPosition;

	[SerializeField] private PlayerInventory playerInventory;

	// TODO ABILITY Break this off with the player ability system
	[Header("Ability Item")]
	[SerializeField] private InventoryItem bow;
	[SerializeField] private InventoryItem sword;
	[Header("Ability Stuff")]
	[SerializeField] private GenericAbilitiy fireballAbility;
	[SerializeField] private GenericAbilitiy dashAbility;
	[SerializeField] private GenericAbilitiy shootAbility;
	//public GameObject projectile;

	// Start is called before the first frame update
	void Start()
    {
        currentState.ChangeState(GenericState.idle);

        transform.position = startingPosition.initialValue;
    }
    // Update is called once per frame
    void Update()
    {
		if (currentState.myState == GenericState.receiveItem)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				currentState.ChangeState(GenericState.idle);
				anim.SetAnimParameter("receiveItem", false);
				myItem.ChangeSpriteState();
			}
			return;
		}
		if(!IsRestrictedState(currentState.myState))
		{
			GetInput();
			SetAnimation();
		}

    }

	bool IsRestrictedState(GenericState currentState)
	{
		if (currentState == GenericState.attack || currentState == GenericState.ability)
		{
			return true;
		}
		return false;
	}


	void SetState(GenericState newState)
	{
		currentState.ChangeState(newState);
	}

	void SetAnimation()
	{
		if (tempMovement.magnitude > 0)
		{
			anim.SetAnimParameter("moveX", Mathf.Round(tempMovement.x));
			anim.SetAnimParameter("moveY", Mathf.Round(tempMovement.y));
			anim.SetAnimParameter("moving", true);
			SetState(GenericState.walk);
			facingDirection = tempMovement;
		}
		else
		{
			anim.SetAnimParameter("moving", false);
			if (currentState.myState != GenericState.attack)
			{
				SetState(GenericState.idle);
			}
		}
	}
	void GetInput()
	{
		//player attack co
		if ((Input.GetButtonDown("weaponAttack") || Input.GetMouseButtonDown(0))
			&& currentState.myState != GenericState.attack && currentState.myState != GenericState.stun)
		{
			if(sword != null && playerInventory.IsItemInInventory(sword))
			{
				StartCoroutine(AttackCo());
				tempMovement = Vector2.zero;
				Motion(tempMovement);
			}
		}
		//New update for ability
		/* else if (Input.GetButtonDown("ability")) */
		else if (Input.GetKeyDown(KeyCode.L))
		{
			//Debug.Log("do ability now bruh");
			if (fireballAbility && bow != null && playerInventory.IsItemInInventory(bow))
			{
				StartCoroutine(AbilityCo(fireballAbility.duration));
			}
		}
		else if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if (dashAbility)
			{
				StartCoroutine(DashCo(dashAbility.duration));
			}
		}
		else if (Input.GetKeyDown(KeyCode.J))
		{
			if (shootAbility && bow != null && playerInventory.IsItemInInventory(bow))
			{
				StartCoroutine(ShootCo(shootAbility.duration));
			}
		}

		else if (currentState.myState != GenericState.attack || currentState.myState == GenericState.walk || currentState.myState == GenericState.idle)
		{
			tempMovement.x = Input.GetAxisRaw("Horizontal");
			tempMovement.y = Input.GetAxisRaw("Vertical");
			Motion(tempMovement);
		}
		else
		{
			tempMovement = Vector2.zero;
			Motion(tempMovement);
		}

	}

	public IEnumerator AttackCo()
    {
        myRigidbody.WakeUp();

		currentState.myState = GenericState.attack;
		anim.SetAnimParameter("attacking", true);
		yield return new WaitForSeconds(WeaponAttackDuration);
		currentState.ChangeState(GenericState.idle);
		anim.SetAnimParameter("attacking", false);
        
        if(currentState.myState != GenericState.receiveItem)
        {
            currentState.myState = GenericState.walk;
        }
    }

	public IEnumerator AbilityCo(float abilityDuration)
	{
		currentState.ChangeState(GenericState.ability);
		fireballAbility.Ability(transform.position, facingDirection, anim.Anim, myRigidbody);
		yield return new WaitForSeconds(abilityDuration);
		currentState.ChangeState(GenericState.idle);
	}
	public IEnumerator DashCo(float abilityDuration)
	{
		currentState.ChangeState(GenericState.ability);
		dashAbility.Ability(transform.position, facingDirection, anim.Anim, myRigidbody);
		yield return new WaitForSeconds(abilityDuration);
		currentState.ChangeState(GenericState.idle);
	}

	public IEnumerator ShootCo(float abilityDuration)
	{
		currentState.ChangeState(GenericState.ability);
		shootAbility.Ability(transform.position, facingDirection, anim.Anim, myRigidbody);
		yield return new WaitForSeconds(abilityDuration);
		currentState.ChangeState(GenericState.idle);
	}



	//// TODO ABILITY 
	//private IEnumerator SecondAttackCo()
	//   {
	//       myRigidbody.WakeUp();

	//       //anim.SetAnimParameter("attacking", true);
	//       currentState.myState = GenericState.attack;
	//       yield return null;
	//       MakeArrow();
	//	//anim.SetAnimParameter("attacking", false);
	//	yield return new WaitForSeconds(0.3f);

	//       if (currentState.myState != GenericState.receiveItem)
	//       {
	//           currentState.myState = GenericState.walk;
	//       }
	//   }
	//   // TODO ABILITY this should be part of the ability itself
	//   //Tao ra mui ten
	//   private void MakeArrow()
	//   {
	//	//Debug.Log("Arrow is made");
	//       if (playerMagic.maxMagic.RuntimeValue > 0)
	//       {
	//		Vector2 temp = new Vector2(anim.GetAnimFloat("moveX"), anim.GetAnimFloat("moveY"));
	//		Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
	//		arrow.SetUp(temp, ChooseArrowDirection());
	//           playerMagic.DecreaseMagic(arrow.magicCost);
	//           reduceMagic.Raise();
	//	}

	//   }

	//// TODO ABILITY this should also be part of the ability itself
	////Tinh toan huong ban cho mui ten
	////Mathf.Atan2 la ham tinh goc bang radian cua vector tren truc OXY => * Mathf.Rad2Deg de chuyen sang do.
	//Vector3 ChooseArrowDirection()
	//   {
	//       float temp = Mathf.Atan2(anim.GetAnimFloat("moveY"), anim.GetAnimFloat("moveX")) * Mathf.Rad2Deg;
	//       return new Vector3(0, 0, temp);
	//   }


	//public void RaiseItem()
	//{
	//    if(playerInventory.currentItem != null)
	//    {
	//        if (currentState.myState != GenericState.receiveItem)
	//        {
	//            anim.SetAnimParameter("receiveItem", true);
	//            currentState.myState = GenericState.receiveItem;
	//            receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
	//        }
	//        else
	//        {
	//            anim.SetAnimParameter("receiveItem", false);
	//            currentState.myState = GenericState.idle;
	//            receivedItemSprite.sprite = null;
	//            playerInventory.currentItem = null;
	//        }
	//    }
	//}


	// void UpdateAnimationAndMove()
	// {
	//     if (change != Vector3.zero)
	//     {
	//         MoveCharacter();
	//         //lam tron gia tri cua vector ve so nguyen gan nhat
	//         //fix loi: attack 2 huong bi active cung luc khi press 2 nut dieu huong cung luc
	//         change.x = Mathf.Round(change.x);
	//change.y = Mathf.Round(change.y);

	//animator.SetFloat("moveX", change.x);
	//         animator.SetFloat("moveY", change.y);
	//         animator.SetBool("moving", true);
	//     }
	//     else
	//     {
	//         animator.SetBool("moving", false);
	//     }
	// }

	//void MoveCharacter()
	//{
	//    change.Normalize(); //chuan hoa vector: khien nhan vat di chuyen giu nguyen toc do theo moi huong
	//    myRigidbody.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
	//}

	// TODO KNOCKBACK move the knockback to its own script
	//player nhan sat thuong
	//public void Knock(float knockTime)
	//{
	//	StartCoroutine(KnockCo(knockTime));
	/*
	// TODO HEALTH
	currentHealth.RuntimeValue -= damage;
	playerHealthSignal.Raise(); //goi su kien

	if (currentHealth.RuntimeValue > 0)
	{
		// TODO HEALTH
		StartCoroutine(KnockCo(knockTime));
	}
	else
	{
		this.gameObject.SetActive(false);
	}
	*/
	//}

	/*
	private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise(); //Screen Kick Effect
        if (myRigidbody != null)
        {
			StartCoroutine(FlashCo());
			yield return new WaitForSeconds(knockTime);     
            myRigidbody.velocity = Vector2.zero; // dat van toc cua player = 0, de ngay lap tuc dung player lai
            //yield return new WaitForSeconds(2*4*flashDuration); //2 lan yield flashDuration * 4 numberOfFlash * flashDuration
			currentState.myState = GenericState.idle; // chuyen sang trang thai idle
            myRigidbody.velocity = Vector2.zero; // dat vt=0 lan nua do dinh hieu ung stagger
        }
    }
    */
}
