using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Enemy[] enemies;
    public pot[] pots;
    public GameObject virtualCamera;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            //Activate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
				foreach (Transform child in enemies[i].transform) //activate child
				{
					if (child.CompareTag("enemy"))
					{
						child.gameObject.SetActive(true);
					}
				}

			}
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            virtualCamera.SetActive(true);
        }
    }
    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Deactivate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
                foreach (Transform child in enemies[i].transform) //deActivate child
				{
					if (child.CompareTag("enemy"))
					{
						child.gameObject.SetActive(false);
					}
				}
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
            virtualCamera.SetActive(false);
        }
    }
	public void OnDisable()
	{
        if (virtualCamera)
        {
            virtualCamera.SetActive(false);
        }
        else return;
	}
	public void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }

}
