using UnityEngine;
using System.Collections;

public class GrapPoint : MonoBehaviour {
    [SerializeField]
    private float EulerTurn, rotationSpeed=1;
    void Awake()
    {
        rotationSpeed = 50;
    }
	void Update () {
        EulerTurn = (Time.deltaTime*rotationSpeed);
        transform.Rotate(0,0,EulerTurn);
        //Debug.Log(new Vector3(0, 0, transform.rotation.z + EulerTurn));
	}
}
