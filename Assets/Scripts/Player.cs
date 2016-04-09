using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Player : MonoBehaviour {

	public Canvas canvas;
	public Image panelImage;

	private int screenX;
	private int screenY;
	private Vector2 circleCenterPoint;
	private Vector2 circleFingerPoint;
	private Vector2 controlVector;

	// Use this for initialization
	void Start () {
		screenX = Screen.width;
		screenY = Screen.height;
		circleCenterPoint = new Vector2(screenX/4, screenY/4);
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < Input.touchCount; i++){
			if(Input.GetTouch(i).phase == TouchPhase.Began && Input.GetTouch(i).position.x > screenX/2){
				ButtonPressed();
			}

			if(Input.GetTouch(i).phase == TouchPhase.Moved && Input.GetTouch(i).position.x < screenX/2){
				circleFingerPoint = Input.GetTouch(0).position;
				Vector2 temp = (circleFingerPoint - circleCenterPoint);
				controlVector = temp/temp.magnitude;
				panelImage.color = new Color(controlVector.x, controlVector.y, 0);
			}
		}
	}

	public void ButtonPressed(){
		Debug.Log("HEY");
		float r;
		float g;
		float b;

		r = Random.Range(0, 1f);
		g = Random.Range(0, 1f);
		b = Random.Range(0, 1f);

		Color col = new Color(r,g,b);

		panelImage.color = col;
	}
}