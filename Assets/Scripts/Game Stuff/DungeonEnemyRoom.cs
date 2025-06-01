using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    public Door[] doors;
    public GameObject dog;
	private void Update()
	{
        CheckEnemies();
	}
	public void CheckEnemies()
    {
		if (enemies == null || enemies.Length == 0)
		{
			OpenDoors();
			return;
		}

		foreach (var enemy in enemies)
		{
			if (enemy.gameObject.activeInHierarchy)
			{
				return;
			}
		}
		OpenDoors();
        dog.SetActive(true);
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Activate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true); //activate parent
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
            CloseDoors();
            virtualCamera.SetActive(true);
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
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
    public void CloseDoors()
    {
        for(int i = 0; i < doors.Length; i++)
        {
            doors[i].Close();
        }
    }
    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
    }
}
