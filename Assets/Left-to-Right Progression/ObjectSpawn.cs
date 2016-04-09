using UnityEngine;
using System.Collections;

public class ObjectSpawn : MonoBehaviour {
    [SerializeField]
    private GameObject[] setOfGrapPoints = new GameObject[1];
    private GameObject[] ActiveGrapPoints = new GameObject[5];
    private float sizeToVectorFactor = 3.5536f;
    private Vector3 _CameraPrePos, _currentCameraPos;
    private Bounds _cameraBounds;
	// Use this for initialization
	void Awake () {
        _cameraBounds = CameraSizing.CameraBounds(this.GetComponent<Camera>());
        _currentCameraPos = _cameraBounds.center;
        _CameraPrePos = _cameraBounds.center;
        for (int i = 2; i < ActiveGrapPoints.Length; i++)
        {

        }
	}
	
	// Update is called once per frame
	void Update () {
        _cameraBounds = CameraSizing.CameraBounds(this.GetComponent<Camera>());
           if (_CameraPrePos.x < (_cameraBounds.center.x- (_cameraBounds.extents.x/2) * sizeToVectorFactor))
        {
            _CameraPrePos = _cameraBounds.center;
            
            ArrayManager(Instantiate(setOfGrapPoints[0],
                                    new Vector2((_cameraBounds.center.x + (_cameraBounds.extents.x * sizeToVectorFactor)), _cameraBounds.center.y),
                                    setOfGrapPoints[0].transform.rotation), 
                                    ActiveGrapPoints);
            
            
        }
	
	}
    private void ArrayManager(Object other, GameObject[] array)
    {
        Destroy(array[0]);
        for (int i = 1; i < array.Length; i++)
        {
            array[i - 1] = array[i];
        }
        array[array.Length - 1] = other as GameObject;
    }
}
