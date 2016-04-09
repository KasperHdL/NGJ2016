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
    public bool isToungePulling;
    
    void Awake(){
        joint = GetComponent<SpringJoint2D>();
        joint.enabled = false;
    }
    
    void Update(){
        if(isToungeOut){
            toungeLength = (tounge.transform.position - transform.position).magnitude;
            
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
