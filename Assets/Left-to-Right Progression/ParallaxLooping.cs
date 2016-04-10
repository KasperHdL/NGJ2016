using UnityEngine;
using System.Collections;

public class ParallaxLooping : MonoBehaviour {
    private float sizeToVectorFactor = 3.5536f;
    private GameObject _camera;
    private Vector3 _CameraPrePos, _currentCameraPos;
    private Bounds _cameraBounds;
    private int mod;

    private Transform[] _backgrounds;
    // Use this for initialization
    void Start () {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _cameraBounds = CameraSizing.CameraBounds(_camera.GetComponent<Camera>());
        _backgrounds = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            _backgrounds[i] = transform.GetChild(i);
            _backgrounds[i].position = new Vector3((_cameraBounds.extents.x * sizeToVectorFactor)*i, _backgrounds[i].position.y, _backgrounds[i].position.z);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        _cameraBounds = CameraSizing.CameraBounds(_camera.GetComponent<Camera>());
        if (_CameraPrePos.x < (_cameraBounds.center.x - (_cameraBounds.extents.x*0.90) * sizeToVectorFactor))
        {
            _backgrounds[mod%3].position = new Vector3(((_cameraBounds.extents.x * sizeToVectorFactor)) + _camera.transform.position.x, _backgrounds[(mod % 3)].position.y, _backgrounds[(mod % 3)].position.z);
            mod++;
            _CameraPrePos = _cameraBounds.center;
        }
    }
}
