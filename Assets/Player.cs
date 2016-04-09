using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Player : MonoBehaviour {

	public Canvas canvas;
	public Image panelImage;

	private int screenX;
	private int screenY;

	// Use this for initialization
	void Start () {
		screenX = Screen.width;
		screenY = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).position.x > screenX/2){
			ButtonPressed();
		}
	}

	public void ButtonPressed(){
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