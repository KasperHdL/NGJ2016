using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Google.Cast.RemoteDisplay;

public class CameraScript : MonoBehaviour
{
    private float i, cameraSpeed = 1;
    private float countdownTime;
    private bool coroutineStarted = false;
    private CastRemoteDisplayManager CastDisplayManager;

    public Image logo;
    public bool slowMotion = false;
    public bool start = false;
    public bool cameraMovement = false;

    // Update is called once per frame
    void Update()
    {
        // DEBUGGING IF'S FOR ADJUSTMENT
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cameraSpeed += 0.25f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cameraSpeed -= 0.25f;
        }

        // TITLE SCREEN
        if (!CastDisplayManager.IsCasting() && logo.enabled == false)
        {
            logo.enabled = true;
            start = false;
            cameraMovement = false;
            i = 0;
        }
        // GAME SCROLL BEGIN
        else if (CastDisplayManager.IsCasting())
        {
            logo.enabled = false;
         
             i += Time.deltaTime;
            if(i>=6 && !coroutineStarted)
            {
                StartCoroutine(CountDown());
                coroutineStarted = true;
            }
            if (i >= 10)
                start = true;
        }
        else if (start && !cameraMovement){
            CameraMovement();
            cameraMovement = true;
        }
        else if (slowMotion)
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
