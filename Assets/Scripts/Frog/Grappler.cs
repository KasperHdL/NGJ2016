using UnityEngine;
using System.Collections;

public class Grappler : MonoBehaviour {

    [HideInInspector]
    public SpringJoint2D joint;
    [HeaderAttribute("Physics")]
    public float maxLength;
    public float retractLength;
    
    public float pullLength;

    private float toungeLength;
    
    public Tounge tounge;
    
    public bool isToungeOut = false;
    
    public bool isToungeJointed;
    
    public float pullIncrement;
    
    public float angularSpeed;
    public bool isToungePulling;
    
    private Rigidbody2D body;
    
    void Awake(){
        joint = GetComponent<SpringJoint2D>();
        body = GetComponent<Rigidbody2D>();
        joint.enabled = false;
    }
    
    void Update(){
        if(isToungeOut){
            Vector2 v = (tounge.transform.position - transform.position);
            toungeLength = v.magnitude;
            transform.rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg * (Mathf.Atan2(v.y,v.x) - Mathf.PI/2));
            
            
        }
    }

	
    public void ShootTounge(Vector2 dir){
        isToungeOut = true;
        tounge.transform.position = transform.position;
        tounge.gameObject.SetActive(true);
        joint.enabled = false;
        tounge.ShootTounge(dir);
    }
    
    public void RetractTounge(){
        isToungeOut = false;
        joint.enabled = false;
        
        if(isToungeJointed){
            Vector2 d = (transform.position - tounge.transform.position).normalized;
            float angle = Vector2. Angle(d,body.velocity);
            Debug.Log("d: " + d + ", b: " + body.velocity.normalized + " angle: " + angle);
            body.angularVelocity = angularSpeed * Mathf.Sign(-angle) * body.velocity.magnitude;
        }
        
        isToungeJointed = false;
        tounge.gameObject.SetActive(false);
    }
    
    public void ToungeHit(){
        joint.enabled = true;
        toungeLength = (tounge.transform.position - transform.position).magnitude;
        joint.distance = toungeLength;
        
        
        if(toungeLength > tounge.transform.position.y || transform.position.y < 0){ //change when water is in the game
            joint.frequency = (toungeLength - pullLength) / pullLength;
            joint.distance = tounge.transform.position.y;
            
        }
                
        isToungeJointed = true;
    }
    
}
