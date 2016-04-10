using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Google.Cast.RemoteDisplay.UI;
using Photon;

public class NetworkingManager : PunBehaviour {

    public Vector3[] spawnPositions;

    public static GameObject[] frogs = new GameObject[4];
    public static int count = 0;
    public int controlIndex = 0;
    

    public static bool isCaster;
    
    public Image hostBackground;
    public Image mainPicture;
    
    public Canvas castCanvas;
    public GameObject castButton;
    
    public Text PlayersConnected;
    
    public Image controls;
    public static Image staticControls;
    public Sprite[] controlSprites;
    public static Sprite[] staticControlSprites;
    public Sprite[] reticleSprites;
    public static Sprite[] staticReticleSprite;
    
    
    
    public CameraScript cameraScript;
    public ObjectSpawn objectSpawn;
    
    private bool gameStarted = false;
    
    public GameObject music;
    public GameObject[] objToActivateOnCasting;
    public GameObject[] objToDeactivateOnCasting;
    public GameObject[] objToDeactivateOnStart;
    public GameObject[] objToActivateOnStart;
   
    void Start(){
        PhotonNetwork.ConnectUsingSettings("0.1");
        staticControlSprites = controlSprites;
        staticReticleSprite = reticleSprites;
        staticControls = controls;
        staticControls.enabled = false;
        
        
        cameraScript.enabled = false;
        objectSpawn.enabled = false;
        
        PlayersConnected.enabled = false;
        mainPicture.enabled = true;
        hostBackground.enabled = false;
        
    }
    
    void Update(){
        if(isCaster && !gameStarted){
            PlayersConnected.text = (count+1) + " Players Connected";

            if(Input.touchCount == 1 || count == 3)
                StartGame();
        }
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
	
	public override void OnJoinedLobby()
    {
            PhotonNetwork.JoinRoom("Frog4");
        
    }
    
    public void Unfreeze(){
         
        for (int i = 0; i < frogs.Length; i++)
        {
            frogs[i].transform.position = spawnPositions[i];
            frogs[i].GetComponent<Player>().deactivate = false;
            frogs[i].GetComponent<Rigidbody2D>().isKinematic = false;
            frogs[i].GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        
        
       
    }
    
    private void StartGame(){
        gameStarted = true;
        
        GameObject go = PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity, 0);
        Controller controller = go.GetComponent<Controller>();
        controller.enabled = true;
        controller.controlledLocally = true;
        
        
        for (int i = 0; i < objToActivateOnStart.Length; i++)
        {
            objToActivateOnStart[i].SetActive(true);
        }
         for (int i = 0; i < objToDeactivateOnStart.Length; i++)
        {
            objToDeactivateOnStart[i].SetActive(false);
        }
        
        
        hostBackground.enabled = false;
        mainPicture.enabled = false;
        PlayersConnected.enabled = false;
        controls.enabled = true;
        
        
        for (int i = 0; i < frogs.Length; i++)
        {
            frogs[i].transform.position = spawnPositions[i];
            frogs[i].GetComponent<Player>().deactivate = true;
            frogs[i].GetComponent<Rigidbody2D>().isKinematic = true;
            frogs[i].GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        cameraScript.enabled = true;
        objectSpawn.enabled = true;
       
        
    }
    
    public override void OnPhotonJoinRoomFailed(object[] arr)
    {
            Debug.LogError("The Google Caster must create the photon room");
            
    }
    
    public void CastingStarted(){
        isCaster = true;
        PhotonNetwork.CreateRoom("Frog4");
       
        hostBackground.enabled = true;
        mainPicture.enabled = false;
        PlayersConnected.enabled = true;
        
        for (int i = 0; i < objToActivateOnCasting.Length; i++)
        {
            objToActivateOnCasting[i].SetActive(true);
        }
          for (int i = 0; i < objToDeactivateOnCasting.Length; i++)
        {
            objToDeactivateOnCasting[i].SetActive(false);
        }
        
        
        
    }
    
    public void CastingStopped(){
        PhotonNetwork.LeaveRoom();
        cameraScript.enabled = false;
        objectSpawn.enabled = false;
    }
    
	
    
    public override void OnJoinedRoom()
    {
        
        if(!isCaster && count < 4){
            GameObject go = PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity,0);
            Controller controller = go.GetComponent<Controller>();
            controller.enabled = true;
            controller.index = count;
            controller.controlledLocally = true;
            
            castButton.SetActive(false);
            mainPicture.enabled = false;
           music.SetActive(false);
        }   
        
    }
}
