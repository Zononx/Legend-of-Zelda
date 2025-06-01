using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : log
{
	public override void Start()
    {
		currentState.ChangeState(GenericState.idle);
		target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Transform>(); //tim thay diem va cham cua player
		anim.SetAnimParameter("stay", true);
	}
	// Update is called once per frame
    public override void checkDistance()
    {
		// neu kc <= ban kinh truy duoi && kc > tam tan cong cua log
		// cap nhat di chuyen ngam vao player
		if (currentState.myState == GenericState.stun) return;

		targetDistance = Vector3.Distance(transform.position, target.position);

        if (targetDistance <= chaseRadius && targetDistance > attackRadius)
        {
            if (currentState.myState == GenericState.idle || currentState.myState == GenericState.walk
                && currentState.myState != GenericState.stun)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                target.position, speed * Time.deltaTime);

                ChangeAnim(temp - transform.position); //xac dinh huong di chuyen cua log
                myRigidbody.MovePosition(temp);

				currentState.ChangeState(GenericState.walk);
				anim.SetAnimParameter("stay", false);

			}
        }
        else if (targetDistance <= chaseRadius && targetDistance <= attackRadius)
        {
            if ( currentState.myState == GenericState.walk && currentState.myState != GenericState.stun)
            {
                StartCoroutine(AttackCo());
            }
        }
		else if (targetDistance > chaseRadius)
		{
			anim.SetAnimParameter("stay", true);
		}

	}
    public IEnumerator AttackCo()
    {
		currentState.ChangeState(GenericState.attack);
        anim.SetAnimParameter("attack", true);
        yield return new WaitForSeconds(1f);
		currentState.ChangeState(GenericState.walk);
		anim.SetAnimParameter("attack", false);

    }
}
