using UnityEngine;

public class Enemy : Movement
{
	//need stateMachine --
	//health --

	//animator -- put in log
	//[SerializeField] private AnimatorController anim;

	//knock -- todo knock put on own knock script

	[SerializeField] public StateMachine currentState;
	[SerializeField] private EnemyHealth enemyHealth; //spawn in room
	
	[Header("Enemy Stats")]
    public string enemyName;
    public Vector2 homePosition;

    private void Awake()
    {
        //bo sung: tranh luc khoi tao ra quai bi spawn sai vitri
        homePosition = transform.position;
    }

	//new spawn in room
    private void OnEnable()
    {
        transform.position = homePosition;
		enemyHealth.SetHealth((int)enemyHealth.maxEnemyHealth.initialValue);
		currentState.ChangeState(GenericState.idle);
	}
	public void SetState(GenericState newState)
	{
		currentState.ChangeState(newState);
	}


	////quai nhan sat thuong
	//public void Knock(Rigidbody2D myRigidbody, float knockTime)
	//{
	//	StartCoroutine(KnockCo(myRigidbody, knockTime));
	//	//TakeDamage(damage);
	//}

	//private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
	//{
	//	if (myRigidbody != null)
	//	{
	//		yield return new WaitForSeconds(knockTime);
	//		myRigidbody.velocity = Vector2.zero; // dat van toc cua enemy = 0
	//		currentState = EnemyState.idle;
	//		myRigidbody.velocity = Vector2.zero;
	//	}
	//}

	//private void TakeDamage(float damage)
	//{
	//    health -= damage;
	//    if(health <= 0)
	//    {
	//        DeathEffect();
	//        MakeLoot();
	//        if(roomSignal != null)
	//        {
	//            roomSignal.Raise();
	//        }

	//        this.gameObject.SetActive(false);
	//    }
	//}
	//private void MakeLoot()
	//{
	//    if(thisLoot != null)
	//    {
	//        PowerUp currentPow = thisLoot.LootPowerUp();
	//        if (currentPow != null)
	//        {
	//            Instantiate(currentPow.gameObject, transform.position, Quaternion.identity);
	//        }
	//    }
	//}

	//private void DeathEffect()
	//{
	//    if(deathEffect != null)
	//    {
	//        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
	//        Destroy(effect, deathEffectDelay);
	//    }
	//}



}
