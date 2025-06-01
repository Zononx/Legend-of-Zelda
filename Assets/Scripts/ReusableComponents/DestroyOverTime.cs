using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
	[Header("Lifetime")]
	[SerializeField] private float lifeTime;

	// Update is called once per frame
	void Update()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
		{
			Destroy(this.gameObject);
		}
	}
}
