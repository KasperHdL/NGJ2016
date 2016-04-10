using UnityEngine;
using System.Collections;

public class ReticleRotation : MonoBehaviour {
	//public float length;

	private Vector2 direction;
	// Use this for initialization
	void Start () {
		direction = new Vector2(0,0);
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		SetReticle();
	}

	public void SetDirection(Vector2 dir){
        if(dir == Vector2.zero){
            gameObject.SetActive(false);
            return;
        }
        else
            gameObject.SetActive(true);
		direction = dir;
		SetReticle();
	}

	public void SetReticle(){
		//transform.position = direction * length;
        transform.rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg * (Mathf.Atan2(direction.y,direction.x) - Mathf.PI / 2));
	}

}
