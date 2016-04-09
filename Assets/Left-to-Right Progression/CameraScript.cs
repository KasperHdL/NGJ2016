using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    private float i, cameraSpeed = 1;
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cameraSpeed += 0.25f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cameraSpeed -= 0.25f;
        }
        if (_currentState == gameState.Menu)
        {

        }
        else if (_currentState == gameState.Begin)
        {
         
             i += Time.deltaTime;

            if (i >= 10)
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
        transform.position = new Vector3(transform.position.x +(cameraSpeed*Time.deltaTime), transform.position.y, transform.position.z);
    }
    private IEnumerator CountDown()
    {
        //3
        yield return new WaitForSeconds(1);
        //2
        yield return new WaitForSeconds(1);
        //1
        yield return new WaitForSeconds(1);
        //Go
    }
}
