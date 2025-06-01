using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : log
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;
    //Override
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

                //ChangeState(EnemyState.walk);
                anim.SetAnimParameter("wakeUp", true);
            }
        }
        //ngoai vung chase
        else if (targetDistance > chaseRadius)
        {
            //ngoai vung gioi han di chuyen, di chuyen ve 2 diem Patrol
            if(Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                path[currentPoint].position, speed * Time.deltaTime);
                ChangeAnim(temp - transform.position); //xac dinh huong di chuyen cua log
                myRigidbody.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
            
        }
    }
    //cap nhat di chuyen trong vung Patrol ponits
    private void ChangeGoal()
    {
        // di den last point thi reset
        if(currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        //cap nhat point
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }

    }
}
