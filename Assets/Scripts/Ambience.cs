using UnityEngine;
using System.Collections;

public class Ambience : MonoBehaviour {

	public AudioSource aS;
	public AudioClip ambience;

	// Use this for initialization
	void Start () {
		aS.clip = ambience;
	}
	
	// Update is called once per frame
	void Update () {
		if(!aS.isPlaying){
			aS.Play();
		}
	}
}
