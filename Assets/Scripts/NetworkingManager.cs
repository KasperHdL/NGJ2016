using UnityEngine;
using System.Collections;
using Photon;

public class NetworkingManager : PunBehaviour {

	// Use this for initialization
	void Start () {
            PhotonNetwork.ConnectUsingSettings("0.1");
    }
 
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
	
	public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }
    void OnJoinedRoom()
    {
        GameObject go = PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity, 0);
        PlayerController controller = go.GetComponent<PlayerController>();
        controller.enabled = true;
        controller.controlledLocally = true;
    }
}
