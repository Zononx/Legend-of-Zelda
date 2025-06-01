using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : GenericHealth
{
	[SerializeField] private FlashColor flash;
	[SerializeField] private FloatValue maxHealthValue;
	[SerializeField] private SignalSender updateHeartsUI;

	[SerializeField] private SignalSender playerGotHit;

	private void Start()
	{
		SetHealth((int)maxHealthValue.RuntimeValue);
		updateHeartsUI.Raise();
	}

	public override void Damage(float damage)
	{
		base.Damage(damage);
		playerGotHit.Raise(); //Screen Kick Effect
		maxHealthValue.RuntimeValue -= damage;
		updateHeartsUI.Raise();
		if (currentHealth > 0)
		{
			if (flash)
			{
				flash.StartFlash();
			}
		}
		if (maxHealthValue.RuntimeValue <= 0)
		{
			maxHealthValue.RuntimeValue = 0;
			MainMenu mainMenu = new MainMenu();
			mainMenu.GameOver();
		}
	}
}
