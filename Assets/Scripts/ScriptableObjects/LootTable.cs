using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attribute nay cho phep Unity hien thi & quan ly cac obj của class Loot trong giao dien (UI)
//cua Unity (Inspector). Cac obj nay se co the dc luu & chinh sua trong Inspector.
[System.Serializable]
public class Loot
{
    public PowerUp thisLoot;
    public float lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;
	//Y tuong Ham LootPowerUp:
	//random(0,100) -> currentProb;
	//chay loop het cac ptu co trong list Loot;
	//cumProb se dc + them mot luong = ty le rot cua vat pham i (lootChance)
	//so sanh bien random currentProb & cumProb
	//neu gia tri random nam trong khoang ty le ra vat pham i => tra ra vat pham do
	//ko thi ko rot ra vat pham gi
	public PowerUp LootPowerUp()
    {
        float cumProb = 0; //xac suat tich luy
        int currentProb = Random.Range(0,100); //xac suat hien tai
        for (int i = 0; i < loots.Length; i++)
        {
            cumProb += loots[i].lootChance;
            if (currentProb <= cumProb)
            {
                return loots[i].thisLoot;
            }
        }
        return null;
    }

}
