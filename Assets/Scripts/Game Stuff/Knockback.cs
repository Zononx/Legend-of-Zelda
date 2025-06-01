using DG.Tweening;
using UnityEngine;

public class Knockback : MonoBehaviour
{
	[SerializeField] private float thrust; //luc day
	[SerializeField] private float knockTime;
    [SerializeField] private string otherTag;

	private void ApplyKnockBack(StateMachine ownState, Rigidbody2D temp, Collider2D other, float knockForce, float duration)
	{
		if (ownState.myState != GenericState.stun)
		{
			ownState.ChangeState(GenericState.stun);

			Vector2 direction = other.transform.position - transform.position;
			temp.DOMove((Vector2)other.transform.position + (direction.normalized * knockForce), duration)
				.SetEase(Ease.OutQuad)
				.OnComplete(() =>
				{
					ownState.ChangeState(GenericState.idle);
				});
		}
	}

	// xu ly va cham knockback voi tag enemy
	public void OnTriggerEnter2D(Collider2D other)
    {
		//for pot
		if (other.gameObject.CompareTag("breakable") && this.gameObject.GetComponent<PolygonCollider2D>().isTrigger)
		{
			other.GetComponent<pot>().Smash();
		}
		//for other
		if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
		{
			//Debug.Log("Knockback applied to: " + otherTag);
			Rigidbody2D temp = other.GetComponentInParent<Rigidbody2D>();
			StateMachine ownState = other.GetComponentInParent<StateMachine>();
			if (temp && ownState)
			{
				ApplyKnockBack(ownState, temp, other, thrust, knockTime);
			}
		}
	}
}



//SetEase(Ease.OutQuad)
//SetEase: Xac dinh cach DOTween dieu chinh toc do di chuyen.
//Ease.OutQuad: hieu ung toc do giam dan.

//OnComplete
//la mot callback: call method ngay sau khi DOMove hoan tat. thay cho coroutine