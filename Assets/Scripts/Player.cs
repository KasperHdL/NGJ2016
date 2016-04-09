using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Player : MonoBehaviour {
	public Canvas canvas;
	public Image panelImage;
	public Image controllerImage;

	private int screenX;
	private int screenY;
	private Vector2 circleCenterPoint;
	private Vector2 circleFingerPoint;
	private Vector2 controlVector;
	private RectTransform rt;
	private float lastRotation;
	private bool button;
	private int buttonFingerIndex;

	// Use this for initialization
	void Start () {
		button = false;
		rt = controllerImage.GetComponent<RectTransform>();
		lastRotation = 0;

		screenX = Screen.width;
		screenY = Screen.height;
		circleCenterPoint = new Vector2(screenX/4, screenY/4);
	}

	// Update is called once per frame
	void Update () {
		for(int i = 0; i < Input.touchCount; i++){
			if(Input.GetTouch(i).phase == TouchPhase.Began && Input.GetTouch(i).position.x > screenX/2){
				button = true;
				buttonFingerIndex = i;
			}

			if(Input.GetTouch(i).phase == TouchPhase.Moved && Input.GetTouch(i).position.x < screenX/2){
				circleFingerPoint = Input.GetTouch(i).position;
				Vector2 temp = (circleFingerPoint - circleCenterPoint);
				controlVector = temp/temp.magnitude;

				Vector2 tempRight = new Vector2(0, 1);

				float rotationTemp = Vector2.Angle(tempRight, controlVector);

				rt.transform.Rotate(Vector3.forward, rotationTemp-lastRotation, Space.Self);

				lastRotation = rotationTemp;
			}
		}

		if(button = true && Input.GetTouch(buttonFingerIndex).phase == TouchPhase.Ended){
			button = false;
		}
	}

	public Vector2 GetControllerDirection(){
		return controlVector;
	}

	public bool GetButtonState(){
		return button;
	}

	public void SetPanelColor(Color col){
		panelImage.color = col;
	}
}