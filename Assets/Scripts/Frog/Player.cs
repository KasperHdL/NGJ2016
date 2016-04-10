using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Controller _controller;
    public ReticleRotation reticle;
    
    public float maxSpeed = 5000;
    
    [HideInInspector]
    public Grappler grappler;
    [HideInInspector]    
    public Rigidbody2D body;
    
    public bool deactivate = false;
    void Awake(){
        body = GetComponent<Rigidbody2D>();
        grappler = GetComponent<Grappler>();
    }
    
  
    
    public void SetController(Controller controller){
        _controller = controller;
        
        _controller.index = NetworkingManager.count;
        grappler.spriteToungeIn = _controller.spriteToungeIn[NetworkingManager.count];
        grappler.spriteToungeOut = _controller.spritesToungeOut[NetworkingManager.count++];
        GetComponent<SpriteRenderer>().sprite = grappler.spriteToungeIn;
        gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if(_controller == null || deactivate)return;

        bool isButtonDown = _controller.GetButtonState();

        Vector2 dir = _controller.GetControllerDirection();
        reticle.SetDirection(dir);

        if(isButtonDown && !grappler.isToungeOut){
//            Debug.Log("Running");
            grappler.ShootTounge(dir);
        }
        else if(!isButtonDown && grappler.isToungeOut)
            grappler.RetractTounge();
   
      
        
        
	}
}
