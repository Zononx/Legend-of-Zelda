
using UnityEngine;

[System.Serializable]

public class LootPotionItem
{
	public PhysicalIventoryItem thisItemLoot;
	public float lootChancePotion;
}

[CreateAssetMenu]
public class LootPotion : ScriptableObject
{
	public LootPotionItem[] lootPotions;

	public PhysicalIventoryItem LootPotionItem()
	{
		float cumProb = 0; //xac suat tich luy
		int currentProb = Random.Range(0, 100); //xac suat hien tai
		for (int i = 0; i < lootPotions.Length; i++)
		{
			cumProb += lootPotions[i].lootChancePotion;
			if (currentProb <= cumProb)
			{
				return lootPotions[i].thisItemLoot;
			}
		}
		return null;
	}

}
