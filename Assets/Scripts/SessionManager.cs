using UnityEngine;
using System.Collections;
using Google.Cast.RemoteDisplay;
//IsCasting() = true if casting event is running
public class SessionManager : MonoBehaviour {

	public Camera cam;
	private CastRemoteDisplayManager CastDisplayManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(CastDisplayManager.IsCasting() && cam.enabled == false){
			cam.enabled = true;
		} else if(!CastDisplayManager.IsCasting() && cam.enabled == true){
			cam.enabled = false;
		}
	}
}
