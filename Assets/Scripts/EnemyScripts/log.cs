using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : Enemy
{
    [Header("Target Variables")]
	public Transform target;
	[SerializeField] public string targetTag;
	[SerializeField] public float chaseRadius;
	[SerializeField] public float attackRadius;
	public float targetDistance;

	[Header("Animator")]
	[SerializeField] public AnimatorController anim;

	// Start is called before the first frame update
	public virtual void Start()
    {
        currentState.ChangeState(GenericState.idle);
		target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Transform>(); //tim thay diem va cham cua player
		anim.SetAnimParameter("wakeUp",true);
    }

    // Update is called once per frame
    void FixedUpdate() //Update()
    {
        checkDistance();
    }
    // virtual: cho phep phuong thuc co the dc overridden
    public virtual void checkDistance()
    {
		// neu kc <= ban kinh truy duoi && kc > tam tan cong cua log
		// cap nhat di chuyen ngam vao player
		if (currentState.myState == GenericState.stun) return;

		targetDistance = Vector3.Distance(transform.position, target.position);

		if (targetDistance <= chaseRadius && targetDistance > attackRadius) 
        {
            if(currentState.myState == GenericState.idle || currentState.myState == GenericState.walk
                && currentState.myState != GenericState.stun)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                ChangeAnim(temp - transform.position); //xac dinh huong di chuyen cua log
                myRigidbody.MovePosition(temp);
                currentState.ChangeState(GenericState.walk);
                anim.SetAnimParameter("wakeUp", true);
            }
        }
        else if (targetDistance > chaseRadius)
        {
            anim.SetAnimParameter("wakeUp", false);
        }
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
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0) 
            {
                SetAnimFloat(Vector2.right);
            }
            else if(direction.x < 0)
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
