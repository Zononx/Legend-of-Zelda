using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPowerUp : PowerUp
{
    public FloatValue maxMagicValue;
    public float magicValue;

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            maxMagicValue.RuntimeValue += magicValue;
            if(maxMagicValue.RuntimeValue > maxMagicValue.initialValue)
            {
                maxMagicValue.RuntimeValue = maxMagicValue.initialValue;

			}
			powerupSignal.Raise();
            Destroy(this.gameObject);
        }
	}
}
