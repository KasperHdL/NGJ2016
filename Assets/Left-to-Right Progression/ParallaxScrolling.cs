using UnityEngine;
using System.Collections;

public class ParallaxScrolling : MonoBehaviour {

    public Vector2 speed = new Vector2(2, 0);
    public float parallaxSpeed;
    private Vector2 direction = new Vector2(1, 0);
    private GameObject Camera;
    private CameraScript Script;
    public bool debug;
	
	void Awake () {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        Script = Camera.GetComponent<CameraScript>();
	}
	
	void Update () {

        if (Script._currentState == CameraScript.gameState.GameTime)
        {
            Vector3 movement = new Vector3((speed.x * direction.x) / parallaxSpeed, 0, 0);
            movement *= Time.deltaTime;
            if (debug)
                Debug.Log("Movement vector: " + movement + " speed: " + speed.x + " direction: " + direction.x);
            transform.Translate(movement);
        }
	}
}
