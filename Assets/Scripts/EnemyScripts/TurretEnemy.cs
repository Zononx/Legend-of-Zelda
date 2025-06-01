using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : log
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    private void Update()
    {
        if(canFire == false)
        {
			fireDelaySeconds -= Time.deltaTime;
			if (fireDelaySeconds <= 0)
			{
				canFire = true;
				fireDelaySeconds = fireDelay;
			}
		}
        
    }

	//Fix bug method: can see the line between player and enemy; circle of chaseRadius,attackRadius
	/*
	private void OnDrawGizmos()
	{
        Gizmos.DrawLine(transform.position, target.position);
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
	}
    */

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
                if (canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<Projectile>().Launch(tempVector);
                    canFire = false;
					currentState.ChangeState(GenericState.walk);
					anim.SetAnimParameter("wakeUp", true);
                }
            }
        }
        else if (targetDistance > chaseRadius)
        {
            anim.SetAnimParameter("wakeUp", false);
        }
    }
}
