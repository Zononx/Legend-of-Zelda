using UnityEngine;

public class GenericMagic : MonoBehaviour
{
	[SerializeField] private float currentMagic;
	[SerializeField] private float maxMagic;

	public void SetMagic(float amount)
	{
		currentMagic = amount;
	}

	public virtual void DecreaseMagic(float amountToUse)
	{
		currentMagic -= amountToUse;
		if (currentMagic <= 0)
		{
			currentMagic = 0;
		}
	}
	public virtual void AddMagic(float amountToAdd)
	{
		currentMagic += amountToAdd;
		if (currentMagic > maxMagic)
		{
			currentMagic = maxMagic;
		}
	}
	public bool CanUseMagic(float amountToUse)
	{
		if (currentMagic >= amountToUse)
		{
			return true;
		}
		return false;
	}
	public void UseAllMagic()
	{
		currentMagic = 0;
	}

	public void FillMagic()
	{
		currentMagic = maxMagic;
	}
}
