
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GenericProjectile : MonoBehaviour
{
	[SerializeField] private Rigidbody2D myRigidbody;
	[SerializeField] private float mySpeed;

	void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();

	}

	public void SetUp(Vector2 moveDirection)
	{
		myRigidbody.velocity = moveDirection.normalized * mySpeed;
	}
}
