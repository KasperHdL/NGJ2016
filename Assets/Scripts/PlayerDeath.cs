using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDeath : MonoBehaviour {
    private Camera _mainCamera;
    private Bounds _cameraBounds;
    private GameObject[] _playersArray = new GameObject[4];
    private List<GameObject> _players;
    
    
   
	
	void Awake () {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _cameraBounds = CameraSizing.CameraBounds(_mainCamera);
        _playersArray = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject i in _playersArray)
        {
            _players.Add(i);
        }
	
	}
	void OnTriggerEnter2D(Collider2D other)
    {
        if(_players.Contains(other.gameObject)) {

            PlayerDie(other.gameObject);
        }
    }
    void PlayerDie(GameObject player)
    {
        //Disable the player for the round
        Destroy(player,1);
    }
}
