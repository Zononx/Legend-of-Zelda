using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pot : MonoBehaviour
{
    private Animator anim;
	[SerializeField] private LootPotion thisLoot;

	// Start is called before the first frame update
	void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    public void Smash()
    {
        anim.SetBool("smash", true);
        StartCoroutine(breakCo());
		MakeLoot();
	}

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
    }
	private void MakeLoot()
	{
		if (thisLoot != null)
		{
			PhysicalIventoryItem currentPow = thisLoot.LootPotionItem();
			if (currentPow != null)
			{
				Instantiate(currentPow.transform, transform.position, Quaternion.identity);
			}
		}
	}
}
