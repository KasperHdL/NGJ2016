using UnityEngine;
using System.Collections;

public class ObjectSpawn : MonoBehaviour {
    [SerializeField]
    private GameObject[] setOfGrapPoints = new GameObject[8];
    private GameObject[] ActiveGrapPoints = new GameObject[4];
    public GameObject toxicCloud;
    private float sizeToVectorFactor = 3.5536f;
    private Vector3 _CameraPrePos, _currentCameraPos;
    private Bounds _cameraBounds;
    private int index; 
   
	void Awake () {
        _cameraBounds = CameraSizing.CameraBounds(this.GetComponent<Camera>());
        _currentCameraPos = _cameraBounds.center;
        _CameraPrePos = _cameraBounds.center;
        ArrayManager(Instantiate(setOfGrapPoints[6],
                                    new Vector3(_cameraBounds.center.x, _cameraBounds.center.y+ 0.85f, -1),
                                    setOfGrapPoints[6].transform.rotation),
                                    ActiveGrapPoints);
        ArrayManager(Instantiate(setOfGrapPoints[1],
                                    new Vector3(_cameraBounds.center.x + ((_cameraBounds.extents.x/1.75f) * sizeToVectorFactor), _cameraBounds.center.y+ 0.85f, -1),
                                    setOfGrapPoints[1].transform.rotation),
                                    ActiveGrapPoints);
        ArrayManager(Instantiate(setOfGrapPoints[2],
                                    new Vector3((_cameraBounds.center.x + (_cameraBounds.extents.x*1.075f* sizeToVectorFactor)), _cameraBounds.center.y+ 0.85f, -1),
                                    setOfGrapPoints[2].transform.rotation),
                                    ActiveGrapPoints);
	}

	void Update ()
    {
        _cameraBounds = CameraSizing.CameraBounds(this.GetComponent<Camera>());
        if (_CameraPrePos.x < (_cameraBounds.center.x- (_cameraBounds.extents.x/1.75) * sizeToVectorFactor))
        {
            _CameraPrePos = _cameraBounds.center;
            index = Random.Range(1, setOfGrapPoints.Length);
            ArrayManager(Instantiate(setOfGrapPoints[index],
                                    new Vector3((_cameraBounds.center.x + (_cameraBounds.extents.x * sizeToVectorFactor)), _cameraBounds.center.y+ 0.85f, -1),
                                    setOfGrapPoints[index].transform.rotation), 
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
