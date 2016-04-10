using UnityEngine;
using System.Collections;

public class GrapPoint : MonoBehaviour {
    private float RotationTime, EulerAngle;
	// Use this for initialization
	void Awake () {
        RotationTime = 50;
        transform.Rotate(new Vector3(0,0,Random.Range(0, 360)));
	}
	
	// Update is called once per frame
	void Update () {
        EulerAngle = Time.deltaTime * RotationTime;
        transform.Rotate(new Vector3(0, 0, EulerAngle));
	}
}
