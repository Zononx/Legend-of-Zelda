
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource SFXSource;

	[Header("Audio Clip")]
	public AudioClip backGround;


	private void Start()
	{
		if (musicSource != null && backGround != null)
		{
			musicSource.clip = backGround;
			musicSource.Play();
		}
		else return;
	}
	public void PlaySFX(AudioClip audioClip)
	{
		if (SFXSource != null)
		{
			SFXSource.PlayOneShot(audioClip);
		}
		else return;
	}
}
