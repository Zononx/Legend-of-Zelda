using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : GenericHealth
{
    [Header("Death Effects")]
    [SerializeField] public FloatValue maxEnemyHealth;
	[SerializeField] private float deathEffectDelay = 1f;
	[SerializeField] private LootTable thisLoot;
	[SerializeField] private GameObject deathEffect;

	[Header("Death Signals")]
	public SignalSender roomSignal;

	private void Start()
	{
		SetHealth((int)maxEnemyHealth.initialValue);
	}
	public override void Damage(float damage)
    {
        base.Damage(damage);
        if(currentHealth <= 0)
        {
			DeathEffect();
			MakeLoot();
			// For enemy dungeon room
			if (roomSignal != null)
			{
				roomSignal.Raise();
			}
			this.GetComponentInParent<Transform>().gameObject.SetActive(false);
		}
    }
	private void DeathEffect()
    {
		if (deathEffect != null)
		{
			GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
			Destroy(effect, deathEffectDelay);
		}
	}
	private void MakeLoot()
	{
		if (thisLoot != null)
		{
			PowerUp currentPow = thisLoot.LootPowerUp();
			if (currentPow != null)
			{
				Instantiate(currentPow.gameObject, transform.position, Quaternion.identity);
			}
		}
	}
}
