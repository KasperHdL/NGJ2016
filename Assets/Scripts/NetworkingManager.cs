﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Photon;

public class NetworkingManager : PunBehaviour {

    public static bool isCaster;
    
    public Image controllerPanel;
   
    void Start(){
        PhotonNetwork.ConnectUsingSettings("0.1");
        
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
	
	public override void OnJoinedLobby()
    {
            PhotonNetwork.JoinRoom("Frog1");
        
    }
    public override void OnPhotonJoinRoomFailed(object[] arr)
    {
            Debug.LogError("The Google Caster must create the photon room");
            
    }
    
    public void CastingStarted(){
        isCaster = true;
        PhotonNetwork.CreateRoom("Frog1");
        
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
