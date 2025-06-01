using UnityEngine;

public class DogMovement : Sign
{
	[Header("Components")]
	[SerializeField] private AnimatorController anim;
	[SerializeField] private Rigidbody2D myRigidbody;
	[SerializeField] private Collider2D boundsCollider;

	[Header("Movement Settings")]
	[SerializeField] private float speed = 2f;
	private Vector3 directionVector;

	[Header("State Timers")]
	[SerializeField] private float minIdleTime = 2f;
	[SerializeField] private float maxIdleTime = 5f;
	[SerializeField] private float minMoveTime = 3f;
	[SerializeField] private float maxMoveTime = 6f;

	[SerializeField] private float standUpDelay = 1f;

	private float stateTimer;
	private bool isWalking;
	private bool isStandUp;

	private void Start()
	{
		EnterIdleState();
	}

	public override void Update()
	{
		stateTimer -= Time.deltaTime;

		if (stateTimer <= 0)
		{
			if (isWalking)
			{
				EnterIdleState();
			}
			else if (!isStandUp && !playerInRange) 
			{
				isStandUp = true; 
				anim.SetAnimParameter("wakeUp", true); 
				stateTimer = standUpDelay;
			}
			else if (!playerInRange)
			{
				EnterWalkState();
			}
		}
		if (isWalking && !playerInRange)
		{
			Move();
		}
		if (Input.GetKeyDown(KeyCode.E) && playerInRange)
		{
			MainMenu mainMenu = new MainMenu();
			mainMenu.EndGame();
		}
	}

	private void EnterIdleState()
	{
		isWalking = false;
		isStandUp = false;
		anim.SetAnimParameter("wakeUp", false);
		anim.SetAnimParameter("walking", false);
		stateTimer = Random.Range(minIdleTime, maxIdleTime);
	}

	private void EnterWalkState()
	{
		isWalking = true;
		anim.SetAnimParameter("wakeUp", true);
		anim.SetAnimParameter("walking", true);
		SetRandomDirection();
		stateTimer = Random.Range(minMoveTime, maxMoveTime);
	}

	private void SetRandomDirection()
	{
		Vector3[] directions = { Vector3.right, Vector3.left, Vector3.up, Vector3.down };
		directionVector = directions[Random.Range(0, directions.Length)];
		UpdateAnimationDirection(directionVector);
	}

	private void Move()
	{
		Vector3 newPosition = transform.position + directionVector * speed * Time.deltaTime;

		// Kiểm tra vị trí có nằm trong giới hạn không
		if (boundsCollider.bounds.Contains(newPosition))
		{
			myRigidbody.MovePosition(newPosition);
		}
		else
		{
			SetRandomDirection(); // Đổi hướng nếu di chuyển vượt khỏi giới hạn
		}
	}

	private void UpdateAnimationDirection(Vector3 direction)
	{
		Vector2 normalizedDirection = new Vector2(direction.x, direction.y).normalized;
		anim.SetAnimParameter("moveX", normalizedDirection.x);
		anim.SetAnimParameter("moveY", normalizedDirection.y);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		SetRandomDirection();
	}

}
