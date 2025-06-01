using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Dash Ability", fileName = "New Dash Ability")]
public class DashAbility : GenericAbilitiy
{
	public float dashForce;

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

		if(playerRigidbody)
		{
			//normalized de duong cheo no longer
			Vector3 dashVector = playerRigidbody.transform.position + (Vector3)playerFacingDirection.normalized * dashForce;
			playerRigidbody.DOMove(dashVector, duration);
		}
	}
}
