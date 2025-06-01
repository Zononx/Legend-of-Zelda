using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;
    public float lifeTime;
    private float lifeTimeCounter;

    public float magicCost;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimeCounter = lifeTime;
	}
	private void Update()
	{
        lifeTimeCounter -= Time.deltaTime;
        if (lifeTimeCounter <= 0 )
        {
            Destroy(this.gameObject);
        }
	}
	public void SetUp(Vector2 velocity, Vector3 direction)
    {
        //chuan hoa vector: ko bay nhanh hon theo 1 so huong khac
        //Quaternion la mot cach de tim ra huong ma thu gi do dang huong toi trong KG 3D
        //Euler se chuyen doi thanh goc do theo huong XYZ

        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //note: chua sua va cham voi map se destroy
        if(collision.gameObject.CompareTag("enemy"))
        {
            Destroy(this.gameObject);
        }
        
    }
}
