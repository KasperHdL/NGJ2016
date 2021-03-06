﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Controller _controller;
    public ReticleRotation reticle;
    
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
