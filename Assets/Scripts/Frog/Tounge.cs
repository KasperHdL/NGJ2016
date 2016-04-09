using UnityEngine;
using System.Collections;

public class Tounge : MonoBehaviour {

    [HideInInspector]
    public Rigidbody2D body;
    
    public Grappler grappler;
    
    public float forceTounge = 10000;

	// Use this for initialization
	void Awake () {
	    body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "Grabable"){
            grappler.ToungeHit();
            transform.position = coll.transform.position;
            body.isKinematic = true;
        }
    }
    
    public void ShootTounge(Vector2 dir){
        body.isKinematic = false;
        body.velocity = Vector2.zero;
        body.AddForce(dir * forceTounge * Time.deltaTime);
    }
}
