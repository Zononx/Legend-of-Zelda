using UnityEngine;
using UnityEngine.UI;

public class PlayerMagic : GenericMagic
{
	[SerializeField] private FloatValue maxMagicValue;
	public FloatValue maxMagic => maxMagicValue;

	public Slider magicSlider;
	
	// Start is called before the first frame update
	void Start()
	{
		magicSlider.maxValue = maxMagicValue.initialValue;
		magicSlider.value = maxMagicValue.RuntimeValue;
		SetMagic(maxMagicValue.RuntimeValue);
	}
	public override void AddMagic(float amountToAdd)
	{
		base.AddMagic(amountToAdd);
		UpdateMagicUI();
	}
	public override void DecreaseMagic(float amountToAdd)
	{
		base.DecreaseMagic(amountToAdd);
		UpdateMagicUI();
	}
	public void UpdateMagicUI()
	{
		magicSlider.value = maxMagicValue.RuntimeValue;
		if (magicSlider.value > magicSlider.maxValue)
		{
			magicSlider.value = magicSlider.maxValue;
			maxMagicValue.RuntimeValue = maxMagicValue.initialValue;
		}
		else if (magicSlider.value < 0)
		{
			magicSlider.value = 0;
			maxMagicValue.RuntimeValue = 0;
		}
	}
}
