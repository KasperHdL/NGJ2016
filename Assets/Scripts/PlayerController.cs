using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public bool buttonDown = false;
    public bool controlledLocally = false;
    
    private Player _player;
    public GameObject player_prefab;

    void Start(){
        if(NetworkingManager.isCaster){
            GameObject p = (GameObject) Instantiate(player_prefab, Vector3.zero, Quaternion.identity);
            Player player = p.GetComponent<Player>();
            player.SetController(this);
            _player = player;
        }
    }

    void Update(){
        if(controlledLocally)
            buttonDown = Input.GetKey(KeyCode.Space);
    }
    
    
    public void SetPlayer(Player player){
        _player = player;
    }
          // in an "observed" script:
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(buttonDown);
        }
        else
        {
           buttonDown = (bool) stream.ReceiveNext();
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
