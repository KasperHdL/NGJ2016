using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private Controller _controller;
    
    public float maxSpeed = 5000;
    
    [HideInInspector]
    public Grappler grappler;
    [HideInInspector]    
    public Rigidbody2D body;
    
    void Awake(){
        body = GetComponent<Rigidbody2D>();
        grappler = GetComponent<Grappler>();
    }
    
  
    
    public void SetController(Controller controller){
        _controller = controller;
        gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if(_controller == null)return;
        
        #if UNITY_ANDROID
            if(_controller.GetButtonState())
                grappler.ShootTounge(_controller.GetControllerDirection());
            else
                grappler.RetractTounge();
        #else
            
            if(Input.GetKeyUp(KeyCode.Space))
                grappler.RetractTounge();
            if(Input.GetKeyDown(KeyCode.Space)){
                grappler.ShootTounge((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized);
            }
        #endif
      
        
        
	}
}
