using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicReaction : MonoBehaviour
{
	//Cap nhat theo huong: tao ra mot scriptableObject moi kieu floatValue ten PlayerMagic - sua doi mot so cho

	public FloatValue playerMagic;
	public SignalSender magicSignal;

	public void Use(int amountToIncrease)
	{
		playerMagic.RuntimeValue += amountToIncrease;
		if(playerMagic.RuntimeValue > playerMagic.initialValue)
		{
			playerMagic.RuntimeValue = playerMagic.initialValue;
		}
		magicSignal.Raise();

	}
	//Chi so Magic cua player nam trong ScriptableOject: Inventory
	//cap nhat va sua doi chi so Magic theo file nay

	//public Inventory playerMagic;
	//   public SignalSender magicSignal;

	//   public void Use(int amountToIncrease)
	//   {
	//       playerMagic.currentMagic += amountToIncrease;
	//       magicSignal.Raise();

	//}

}
