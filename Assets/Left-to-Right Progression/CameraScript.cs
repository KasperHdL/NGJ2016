using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    private float i;
    private float countdownTime;
    public enum gameState
    {
        Menu,
        Begin,
        GameTime,
        EndSlow
    }
    public gameState _currentState = gameState.Begin;
    
    // Update is called once per frame
    void Update()
    {
        if (_currentState == gameState.Menu)
        {

        }
        else if (_currentState == gameState.Begin)
        {
            //Insert initial countdown
            i += Time.deltaTime;
            if (i >= 3)
                _currentState = gameState.GameTime;

        }
        else if (_currentState == gameState.GameTime)
        {
            CameraMovement();
        }
        else if (_currentState == gameState.EndSlow)
        {
            //MLG theme+zoom+slowmotion
        }
        else
            throw new System.Exception();
    }
    public void CameraMovement()
    {
        transform.position = new Vector3(transform.position.x + Time.deltaTime, transform.position.y, transform.position.z);
    }
}
