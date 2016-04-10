using UnityEngine;
using System.Collections;

public class ReticleRotation : MonoBehaviour {
	//public float length;

	private Vector2 direction;
	private float lastRotation;
	// Use this for initialization
	void Start () {
		lastRotation = 0;
		direction = new Vector2(0,1);
	}
	
	// Update is called once per frame
	void Update () {
		SetReticle();
	}

	public void SetDirection(Vector2 dir){
		direction = dir;
		SetReticle();
	}

	public void SetReticle(){
		//transform.position = direction * length;
        Vector2 tempRight = new Vector2(0, 1);
		float rotationTemp = Vector2.Angle(direction, tempRight);
		transform.Rotate(Vector3.forward, rotationTemp-lastRotation, Space.Self); 
		lastRotation = rotationTemp;
	}

}
