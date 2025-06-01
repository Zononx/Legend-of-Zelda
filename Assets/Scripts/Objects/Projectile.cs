using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Stuff")]
    public float moveSpeed;
    public Vector2 directionToMove;
    public Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    public void Launch(Vector2 intialVec)
    {
        myRigidbody.velocity = intialVec * moveSpeed;

    }
    //update video fix project 84/95: dang bo doan va cham nay
    public void OnTriggerEnter2D(Collider2D collision)
    {
		if (!collision.isTrigger)
        {
			Destroy(this.gameObject);
		}
    }
}
