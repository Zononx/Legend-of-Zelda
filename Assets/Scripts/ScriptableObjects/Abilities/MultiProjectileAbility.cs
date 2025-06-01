
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Multi-Projectile Ability", fileName = "New MultiProjectile Ability")]
public class MultiProjectileAbility : GenericAbilitiy
{
	[SerializeField] private GameObject thisProjectile;
	[SerializeField] private int numberOfProjectiles;
	[SerializeField] private float projectileSpread; //do. lan toa? dan. -- angle spread
	public override void Ability(Vector2 playerPosition, Vector2 playerFacingDirection,
			Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
	{
		//Make sure the player has enough magic
		if (playerMagic.RuntimeValue >= magicCost)
		{
			playerMagic.RuntimeValue -= magicCost;
			usePlayerMagic.Raise();
		}
		else
		{
			return;
		}

		//setup direction
		float facingRotation = Mathf.Atan2(playerFacingDirection.y, playerFacingDirection.x) * Mathf.Rad2Deg;
		float startRotation = facingRotation + projectileSpread / 2f;
		float angleIncrease = projectileSpread / ((float)numberOfProjectiles - 1f);

		for (int i = 0; i < numberOfProjectiles; i++)
		{
			float tempRot = startRotation - angleIncrease * i;
			GameObject newProjectile = Instantiate(thisProjectile, playerPosition, Quaternion.Euler(0f, 0f, tempRot));
			GenericProjectile temp = newProjectile.GetComponent<GenericProjectile>();
			if (temp)
			{
				temp.SetUp(new Vector2(Mathf.Cos(tempRot * Mathf.Deg2Rad),Mathf.Sin(tempRot * Mathf.Deg2Rad)));
			}
		}
		
	}
}
