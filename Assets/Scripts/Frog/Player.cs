using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private PlayerController _controller;
    
    public float maxSpeed = 5000;
    
    [HideInInspector]
    public Grappler grappler;
    [HideInInspector]    
    public Rigidbody2D body;
    
    void Awake(){
        body = GetComponent<Rigidbody2D>();
        grappler = GetComponent<Grappler>();
    }
    
    void Start(){
        //if(_controller == null)
          //  gameObject.SetActive(false);
    }
    
    public void SetController(PlayerController controller){
        _controller = controller;
        gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if(body.velocity.magnitude > maxSpeed)
            body.velocity = body.velocity.normalized * Mathf.Sign(body.velocity.magnitude) * maxSpeed;
        if(Input.GetKeyUp(KeyCode.Space))
            grappler.RetractTounge();
        if(Input.GetKeyDown(KeyCode.Space)){
            grappler.ShootTounge((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized);
        }
	    if(_controller == null)return;
        
        
	}
}
