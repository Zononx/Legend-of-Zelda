using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Check script DialogController

public class BoundedNPC : Sign
{
    private Vector3 directionVector;
	private Transform myTransform;
	private Rigidbody2D myRigidbody;
	public float speed;
	private Animator anim;
	public Collider2D boundsCollider;

	private bool isMoving;
	public float minMoveTime;
	public float maxMoveTime;
	private float moveTimeSeconds;
	public float minWaitTime;
	public float maxWaitTime;
	private float waitTimeSeconds;

	// Start is called before the first frame update
	void Start()
    {
		moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
		waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);

		anim = GetComponent<Animator>();
		myTransform = GetComponent<Transform>();
		myRigidbody = GetComponent<Rigidbody2D>();
		ChangeDirection();
	}

    // Update is called once per frame
    public override void Update()
    {
		base.Update();
		if (isMoving)
		{
			moveTimeSeconds -= Time.deltaTime;
			if(moveTimeSeconds <= 0 )
			{
				moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);

				isMoving = false;
			}
			if (!playerInRange)
			{
				Move();
			}
		}
		else
		{
			waitTimeSeconds -= Time.deltaTime;
			if(waitTimeSeconds <= 0 ) 
			{
				ChooseDifferentDirection();
				isMoving = true;
				waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);
			}
		}
	}
	private void ChooseDifferentDirection()
	{
		Vector3 temp = directionVector;
		ChangeDirection();
		int loops = 0;
		while (temp == directionVector && loops < 100)
		{
			loops++;
			ChangeDirection();
		}
	}
	private void Move()
	{
		Vector3 temp = myTransform.position + directionVector * speed * Time.fixedDeltaTime;
		if (boundsCollider.bounds.Contains(temp))
		{
			myRigidbody.MovePosition(temp);
		}
		else
		{
			ChangeDirection();
		}
	}
	void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                //Walking Right
                directionVector = Vector3.right;
				break;
			case 1:
				//Walking Left 
				directionVector = Vector3.left;
				break;
			case 2:
				//Walking Up
				directionVector = Vector3.up;
				break;
			case 3:
				//Walking Down
				directionVector = Vector3.down;
				break;
			default:
				break;
		}
		UpdateAnimation();
    }
	void UpdateAnimation()
	{
		anim.SetFloat("MoveX", directionVector.x);
		anim.SetFloat("MoveY", directionVector.y);
		//anim.SetBool("moving", true);
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		ChooseDifferentDirection();
	}
}
