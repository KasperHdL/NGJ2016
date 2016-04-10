﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Photon;

public class NetworkingManager : PunBehaviour {

    public static bool isCaster;
    
    public bool gonnaCastMan = false;
    
    public Image controllerPanel;
    

	// Use this for initialization
	void Start () {
        PhotonNetwork.ConnectUsingSettings("0.1");
        isCaster = gonnaCastMan;
    }
 
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
	
	public override void OnJoinedLobby()
    {
        if(isCaster)
            PhotonNetwork.CreateRoom("Frog");
        else
            PhotonNetwork.JoinRoom("Frog");
        
    }
    public override void OnPhotonJoinRoomFailed(object[] arr)
    {
            Debug.LogError("The Google Caster must create the photon room");
            
    }
    
    
	public void SetPanelColor(Color col){
		controllerPanel.color = col;
	}
    
    public override void OnJoinedRoom()
    {
        GameObject go = PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity, 0);
        Controller controller = go.GetComponent<Controller>();
        controller.enabled = true;
        controller.controlledLocally = true;
        
        
        
    }
}
