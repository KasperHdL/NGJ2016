using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public bool buttonDown = false;
    public bool controlledLocally = false;

    void Update(){
        if(controlledLocally)
            buttonDown = Input.GetKey(KeyCode.Space);
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
        
}
