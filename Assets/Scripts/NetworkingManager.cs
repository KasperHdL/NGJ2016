using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Google.Cast.RemoteDisplay.UI;
using Photon;

public class NetworkingManager : PunBehaviour {

    public GameObject[] frogs = new GameObject[4];
    private int count = 1;
    

    public static bool isCaster;
    
    public Image hostBackground;
    public Image mainPicture;
    
    public Canvas castCanvas;
    public GameObject castButton;
    
    public Text PlayersConnected;
    
    public Image controls;
    public Sprite[] controlSprites;
    
    
    public CameraScript cameraScript;
    public ObjectSpawn objectSpawn;
    
    private bool gameStarted = false;
   
    void Start(){
        PhotonNetwork.ConnectUsingSettings("0.1");
        controls.enabled = false;
        
        PlayersConnected.enabled = false;
        mainPicture.enabled = true;
        hostBackground.enabled = false;
        
    }
    
    void Update(){
        if(isCaster && !gameStarted){
            PlayersConnected.text = PhotonNetwork.countOfPlayers + " Players Connected";

            if(Input.touchCount == 1 || PhotonNetwork.countOfPlayers == 4)
                StartGame();
        }
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
	
	public override void OnJoinedLobby()
    {
            PhotonNetwork.JoinRoom("Frog1");
        
    }
    
    private void StartGame(){
        gameStarted = true;
        
        GameObject go = PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity, 0);
        Controller controller = go.GetComponent<Controller>();
        controller.enabled = true;
        controller.controlledLocally = true;
        frogs[0] = go;
        controls.sprite = controlSprites[0];
        
        
        controls.enabled = true;
        
        hostBackground.enabled = false;
        mainPicture.enabled = false;
        PlayersConnected.enabled = false;
        
        cameraScript.enabled = true;
        objectSpawn.enabled = true;
        
    }
    
    public override void OnPhotonJoinRoomFailed(object[] arr)
    {
            Debug.LogError("The Google Caster must create the photon room");
            
    }
    
    public void CastingStarted(){
        isCaster = true;
        PhotonNetwork.CreateRoom("Frog1");
       
        hostBackground.enabled = true;
        mainPicture.enabled = false;
        PlayersConnected.enabled = true;
        
        
        
    }
    
    public void CastingStopped(){
        PhotonNetwork.LeaveRoom();
        cameraScript.enabled = false;
        objectSpawn.enabled = false;
    }
    
	
    
    public override void OnJoinedRoom()
    {
        
        if(!isCaster && count < 4){
            castButton.SetActive(false);
            mainPicture.enabled = false;
            GameObject go = PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity, 0);
            Controller controller = go.GetComponent<Controller>();
            controller.enabled = true;
            controller.controlledLocally = true;
            
            controls.sprite = controlSprites[count];
            controls.enabled = true;
            
            frogs[count++] = go;
        }   
        
    }
}
