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
	public int buttonFingerId = -1;
    public int circleFingerId = -1;
    
    public bool debug = false;


    
    public bool controlledLocally = false;
    
    private Player _player;
    public GameObject player_prefab;
    
    public int touches;

    public int index;
    
    public Sprite[] spriteToungeIn;
    public Sprite[] spritesToungeOut;

	// Use this for initialization
	void Start () {
       
        
		button = false;
		lastRotation = 0;

		screenX = Screen.width;
		screenY = Screen.height;
		circleCenterPoint = new Vector2(screenX/4, screenY/2);
        
        NetworkingManager.staticControls.sprite = NetworkingManager.staticControlSprites[NetworkingManager.count];
        
        if(NetworkingManager.isCaster){
             
            GameObject p = (GameObject) Instantiate(player_prefab, new Vector3(0,0,-20), Quaternion.identity);
            NetworkingManager.frogs[NetworkingManager.count] = p;
            Player player = p.GetComponent<Player>();
            player.reticle.GetComponent<SpriteRenderer>().sprite = NetworkingManager.staticReticleSprite[NetworkingManager.count];
            player.SetController(this);
            _player = player;
        }
	}


	// Update is called once per frame
	void Update () {
        if(!controlledLocally)return;
     
        #if !UNITY_EDITOR
           if(Input.touchCount == 0){
                controlVector = Vector2.zero;
           }
           
           button = Input.touchCount == 2;
           
           for(int i = 0; i < Input.touchCount; i++){
                Touch t = Input.GetTouch(i);
                TouchPhase phase = t.phase;
                
                
                if(t.position.x < screenX/2){
                    circleFingerPoint = t.position;
                    Vector2 temp = (circleFingerPoint - circleCenterPoint);
                    controlVector = temp/temp.magnitude;

                    Vector2 tempRight = new Vector2(0, 1);

                    float rotationTemp = Vector2.Angle(tempRight, controlVector);

                    rt.transform.Rotate(Vector3.forward, rotationTemp-lastRotation, Space.Self);

                    lastRotation = rotationTemp;
                    
                    break;
                }
                
                    /*
                    if(buttonFingerId == -1 && t.fingerId != circleFingerId && t.position.x > screenX/2){
                        button = true;
                        buttonFingerId = t.fingerId;
                        continue;
                    }
                    if(circleFingerId == -1 && t.fingerId != buttonFingerId && t.position.x < screenX/2){
                        circleFingerPoint = t.position;
                        Vector2 temp = (circleFingerPoint - circleCenterPoint);
                        controlVector = temp/temp.magnitude;

                        Vector2 tempRight = new Vector2(0, 1);

                        float rotationTemp = Vector2.Angle(tempRight, controlVector);

                        rt.transform.Rotate(Vector3.forward, rotationTemp-lastRotation, Space.Self);

                        lastRotation = rotationTemp;
                        
                        circleFingerId = t.fingerId;
                        continue;
                    }     
                    if(t.fingerId == circleFingerId && t.position.x < screenX/2){
                        circleFingerPoint = t.position;
                        Vector2 temp = (circleFingerPoint - circleCenterPoint);
                        controlVector = temp/temp.magnitude;

                        Vector2 tempRight = new Vector2(0, 1);

                        float rotationTemp = Vector2.Angle(tempRight, controlVector);

                        rt.transform.Rotate(Vector3.forward, rotationTemp-lastRotation, Space.Self);

                        lastRotation = rotationTemp;
                        continue;
                    }
                    */
               
           }
        #else
          button = Input.GetMouseButton(0);
          controlVector = ((Vector2)Input.mousePosition - circleCenterPoint).normalized;
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
            stream.SendNext(Input.touchCount);
        }
        else
        {
           button           = (bool) stream.ReceiveNext();
           controlVector    = (Vector2) stream.ReceiveNext();
           
           touches   = (int) stream.ReceiveNext();
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