using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    private float i, countdownTime, cameraSpeed = 1;
    private int counter;
    private bool coroutineStarted = false;
    [SerializeField]
    private GameObject[] countDown = new GameObject[4];
    private GameObject Three, Two, One, Go;
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
            if(!coroutineStarted)
            {
                StartCoroutine(CountDown());
                coroutineStarted = true;
            }
            if (i >= 4)
            
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
    }
    public void CameraMovement()
    {
        transform.position = new Vector3(transform.position.x +(cameraSpeed*Time.deltaTime), transform.position.y, transform.position.z);
    }
    private IEnumerator CountDown()
    {
        Three = Instantiate(countDown[0], new Vector3(0, 0,-5), Quaternion.identity) as GameObject;
        StartCoroutine(Iteration(Three));
        yield return new WaitForSeconds(1);
        Destroy(Three);


        Two = Instantiate(countDown[1], new Vector3(0, 0,-5), Quaternion.identity) as GameObject;
        StartCoroutine(Iteration(Two));
        yield return new WaitForSeconds(1);
        Destroy(Two);
       

        One = Instantiate(countDown[2], new Vector3(0, 0,-5), Quaternion.identity) as GameObject;
        StartCoroutine(Iteration(One));
        yield return new WaitForSeconds(1);
        Destroy(One);
       

        Go = Instantiate(countDown[3], new Vector3(0, 0,-5), Quaternion.identity) as GameObject;
        StartCoroutine(Iteration(Go));
        yield return new WaitForSeconds(1);
        Destroy(Go);
       
    }

    private IEnumerator Iteration(GameObject other)
    {
        counter = 0;
        while(counter < 100)
        {
           
            if(other == null)
            {
                yield break;
            }
            other.transform.localScale = new Vector3(other.transform.localScale.x + 5 * Time.deltaTime, other.transform.localScale.z, other.transform.localScale.y + 5 * Time.deltaTime);
            counter++;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
