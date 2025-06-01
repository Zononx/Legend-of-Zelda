using UnityEngine;

/*
 * This script is a generic health component for
 * any item that needs to have health.  This can
 * be added to the player, enemies, pots or grass
 * in the scene.  It can also be extended by
 * inheriting from it for specific interactions desired.
 */

public class GenericHealth : MonoBehaviour
{
    [Tooltip("Max and current health \n Set this to one for pots")]
    [Header("Health values")]
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;

	public void SetHealth(float amount)
	{
		currentHealth = amount;
	}

	public virtual void Damage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			this.GetComponentInParent<Transform>().gameObject.SetActive(false);
		}
	}

	public void Heal(float amount)
	{
		currentHealth += amount;
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
	}

	public void Kill()
	{
		currentHealth = 0;
	}

	public void FullHeal()
	{
		currentHealth = maxHealth;
	}
}
