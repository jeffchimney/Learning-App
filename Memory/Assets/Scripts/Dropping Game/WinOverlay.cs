using UnityEngine;
using System.Collections;

public class WinOverlay : MonoBehaviour 
{
	protected bool isShowing = false;
	public GameObject menu; // Assign in inspector
	public AudioClip congratulations;
	AudioSource playAudio;

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			isShowing = !isShowing;
		}

		if(isShowing)
		{
			if(!GetComponent<ParticleSystem>().isPlaying)
			{
				GetComponent<ParticleSystem>().Play();
				menu.SetActive(isShowing);
				playAudio.PlayOneShot(congratulations);
			}
		}else{
			if(GetComponent<ParticleSystem>().isPlaying)
			{
				GetComponent<ParticleSystem>().Stop();
				menu.SetActive(isShowing);
			}
		}
	}
}