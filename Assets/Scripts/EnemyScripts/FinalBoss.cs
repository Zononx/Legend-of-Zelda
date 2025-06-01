using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Enemy
{
	[Header("Target Variables")]
	public Transform target;
	[SerializeField] private string targetTag;
	[SerializeField] private float chaseRadius;
	[SerializeField] private float attackRadius;
	private float targetDistance;

	[Header("Animator")]
	[SerializeField] private AnimatorController anim;

	[Header("Attack Settings")]
	[SerializeField] private float attackCooldown;
	private bool isAttacking;

	private void Start()
	{
		target = GameObject.FindGameObjectWithTag(targetTag)?.transform;
		myRigidbody = GetComponent<Rigidbody2D>();

		currentState.ChangeState(GenericState.idle);
		anim.SetAnimParameter("isIdle", true);
	}

	private void FixedUpdate()
	{
		if (target == null) return;

		targetDistance = Vector3.Distance(transform.position, target.position);

		if (currentState.myState != GenericState.stun)
		{
			if (targetDistance <= attackRadius && !isAttacking)
			{
				HandleAttack();
			}
			else if (targetDistance <= chaseRadius)
			{
				ChasePlayer();
			}
			else
			{
				IdleState();
			}
		}
	}

	private void IdleState()
	{
		if (currentState.myState != GenericState.idle)
		{
			currentState.ChangeState(GenericState.idle);
			anim.SetAnimParameter("isIdle", true);
			anim.SetAnimParameter("isWalk", false);
			anim.SetAnimParameter("isAttackA", false);
			anim.SetAnimParameter("isAttackB", false);
		}
	}

	private void ChasePlayer()
	{
		if (currentState.myState == GenericState.idle || currentState.myState == GenericState.walk)
		{
			Vector3 targetPosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
			ChangeAnim(targetPosition - transform.position);
			myRigidbody.MovePosition(targetPosition);

			currentState.ChangeState(GenericState.walk);
			anim.SetAnimParameter("isWalk", true);
			anim.SetAnimParameter("isIdle", false);
		}
	}

	private void HandleAttack()
	{
		if (!isAttacking)
		{
			StartCoroutine(PerformAttackSequence());
		}
	}

	private IEnumerator PerformAttackSequence()
	{
		isAttacking = true;
		currentState.ChangeState(GenericState.attack);

		int attackType = Random.Range(1, 4);
		anim.SetAnimParameter("isWalk", false);
		anim.SetAnimParameter("isIdle", false);

		if (attackType == 1)
		{
			StartCoroutine(PerformSingleAttack("isAttackA"));
			yield return new WaitForSeconds(0.5f);// delay attack
		}
		else if (attackType == 2)
		{
			StartCoroutine(PerformSingleAttack("isAttackB"));
			yield return new WaitForSeconds(0.5f);// delay attack
		}
		else if (attackType == 3)
		{
			StartCoroutine(PerformSingleAttack("isAttackA"));	
			anim.SetAnimParameter("isIdle", false);
			anim.SetAnimParameter("isWalk", false);
			StartCoroutine(PerformSingleAttack("isAttackB"));
		}

		yield return new WaitForSeconds(attackCooldown); 
		isAttacking = false;

		// ktra kc sau khi attack
		if (targetDistance > attackRadius)
		{
			if (currentState.myState != GenericState.walk)
			{
				currentState.ChangeState(GenericState.walk);
				anim.SetAnimParameter("isWalk", true);
				anim.SetAnimParameter("isIdle", false);
			}
		}
		else
		{
			if (currentState.myState != GenericState.idle)
			{
				currentState.ChangeState(GenericState.idle);
				anim.SetAnimParameter("isIdle", true);
				anim.SetAnimParameter("isWalk", false);
			}
		}
	}
	private IEnumerator PerformSingleAttack(string attackParameter)
	{
		anim.SetAnimParameter(attackParameter, true);
		yield return new WaitForSeconds(0.4f); // duration hitbox
		anim.SetAnimParameter(attackParameter, false);
	}
	private void SetAnimFloat(Vector2 setVector)
	{
		anim.SetAnimParameter("moveX", setVector.x);
		anim.SetAnimParameter("moveY", setVector.y);
	}
	//check huong di chuyen de sua animation
	public void ChangeAnim(Vector2 direction)
	{
		//Debug.Log(direction);
		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
		{
			if (direction.x > 0)
			{
				SetAnimFloat(Vector2.right);
			}
			else if (direction.x < 0)
			{
				SetAnimFloat(Vector2.left);
			}
		}
		else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
		{
			if (direction.y > 0)
			{
				SetAnimFloat(Vector2.up);
			}
			else if (direction.y < 0)
			{
				SetAnimFloat(Vector2.down);
			}
		}
	}
}
