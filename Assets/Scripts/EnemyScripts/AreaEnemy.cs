using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : log
{
    //boundary: ranh gioi
    public Collider2D boundary;
    public override void checkDistance()
    {
		if (currentState.myState == GenericState.stun) return;
		targetDistance = Vector3.Distance(transform.position, target.position);

		if (targetDistance <= chaseRadius && targetDistance > attackRadius 
            && boundary.bounds.Contains(target.transform.position))
        {
            if (currentState.myState == GenericState.idle || currentState.myState == GenericState.walk
                && currentState.myState != GenericState.stun)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                target.position, speed * Time.deltaTime);

                ChangeAnim(temp - transform.position); //xac dinh huong di chuyen cua log
                myRigidbody.MovePosition(temp);

				currentState.ChangeState(GenericState.walk);
				anim.SetAnimParameter("wakeUp", true);
            }
        }
        else if (targetDistance > chaseRadius
            || !boundary.bounds.Contains(target.transform.position))
        {
            anim.SetAnimParameter("wakeUp", false);
        }
    }
}
