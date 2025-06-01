using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Generic Ability", fileName = "New Generic Ability")]
public class GenericAbilitiy : ScriptableObject
{
	public float magicCost;
	public float duration; //thoi gian thuc hien

	public FloatValue playerMagic;
	public SignalSender usePlayerMagic;

	public virtual void Ability(Vector2 playerPosition, Vector2 playerFacingDirection, 
		Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
	{

	}
}
