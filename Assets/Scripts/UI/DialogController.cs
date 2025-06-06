using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class DialogController : MonoBehaviour
{
	[SerializeField] private StringValue stringText;
	[SerializeField] private SignalSender dialogNotification;
	[SerializeField] private TextMeshProUGUI dialogText;
	[SerializeField] private GameObject dialogObject;
	[SerializeField] private bool dialogActive = false;

	public void ActivateDialog()
	{
		dialogActive = !dialogActive;
		if (dialogActive)
		{
			SetDialog();
		}
		else
		{
			DeactivateDialog();
		}
	}

	void SetDialog()
	{
		dialogObject.SetActive(true);
		dialogText.text = stringText.value;
	}

	void DeactivateDialog()
	{
		dialogObject.SetActive(false);
	}
}
