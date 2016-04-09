using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Controller : MonoBehaviour {
	private int screenX;
	private int screenY;
	private Vector2 circleCenterPoint;
	private Vector2 circleFingerPoint;
	public Vector2 controlVector;
	private RectTransform rt;
	private float lastRotation;
	public bool button;
	private int buttonFingerIndex;
    
    public bool debug = false;


    
    public bool controlledLocally = false;
    
    private Player _player;
    public GameObject player_prefab;


	// Use this for initialization
	void Start () {
        if(NetworkingManager.isCaster){
            GameObject p = (GameObject) Instantiate(player_prefab, Vector3.zero, Quaternion.identity);
            Player player = p.GetComponent<Player>();
            player.SetController(this);
            _player = player;
        }
        
		button = false;
		lastRotation = 0;

		screenX = Screen.width;
		screenY = Screen.height;
		circleCenterPoint = new Vector2(screenX/4, screenY/4);
	}

	// Update is called once per frame
	void Update () {
        if(!controlledLocally)return;
     
        #if !UNITY_EDITOR
           for(int i = 0; i < Input.touchCount; i++){
                TouchPhase phase = Input.GetTouch(i).phase;
                if(phase != TouchPhase.Ended && phase != TouchPhase.Canceled){
                    if(Input.GetTouch(i).position.x > screenX/2){
                        button = true;
                        buttonFingerIndex = i;
                    }

                    if(Input.GetTouch(i).position.x < screenX/2){
                        circleFingerPoint = Input.GetTouch(i).position;
                        Vector2 temp = (circleFingerPoint - circleCenterPoint);
                        controlVector = temp/temp.magnitude;

                        Vector2 tempRight = new Vector2(0, 1);

                        float rotationTemp = Vector2.Angle(tempRight, controlVector);

                        rt.transform.Rotate(Vector3.forward, rotationTemp-lastRotation, Space.Self);

                        lastRotation = rotationTemp;
                    }

                }else{
                    if(Input.GetTouch(i).position.x > screenX/2){
                        button = false;
                    }
                }
            }
        #else
       
       #endif
	}

	public Vector2 GetControllerDirection(){
		return controlVector;
	}

	public bool GetButtonState(){
		return button;
	}

    
    
    public void SetPlayer(Player player){
        _player = player;
    }
          // in an "observed" script:
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            
            stream.SendNext(button);
            stream.SendNext(controlVector);
        }
        else
        {
           button           = (bool) stream.ReceiveNext();
           controlVector    = (Vector2) stream.ReceiveNext();
        }
        
        
    }
    void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerDisconnected: " + player);
        if(player.ID == PhotonNetwork.player.ID){
            Destroy(_player.gameObject);
            //play some kind of animation
        }
    
        
    }
}