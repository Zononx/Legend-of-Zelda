
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Projectile Ability", fileName = "New Projectile Ability")]
public class ProjectileAbility : GenericAbilitiy
{
	[SerializeField] private GameObject thisProjectile;
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
		GameObject newProjectile = Instantiate(thisProjectile, playerPosition, Quaternion.Euler(0f,0f,facingRotation));

		GenericProjectile temp = newProjectile.GetComponent<GenericProjectile>();
		if(temp)
		{
			temp.SetUp(playerFacingDirection);
		}
	}
}
