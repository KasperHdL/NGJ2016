using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	public AudioSource aS;
	public AudioClip intro;
	public AudioClip loop;

	private bool introHasPlayed;

	// Use this for initialization
	void Start () {
		introHasPlayed = false;
		aS.volume = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		if(!introHasPlayed && !aS.isPlaying){
			aS.clip = intro;
			aS.Play();
			introHasPlayed = true;
		}
		if(introHasPlayed && !aS.isPlaying){
			aS.clip = loop;
			aS.Play();
		}	
	}
}
