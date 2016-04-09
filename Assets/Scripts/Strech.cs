using UnityEngine;
using System.Collections;

public class Strech : MonoBehaviour {

	private RectTransform rt;
	private float imageWidth;
	private float imageHeight;
	private float screenX;
	private float screenY;
	private Rect tempRect;

	// Use this for initialization
	void Start () {
		screenX = Screen.width;
		screenY = Screen.height;
		rt = GetComponent<RectTransform>();
		imageWidth = rt.rect.width/2;
		imageHeight = rt.rect.height/2;

		if(screenX < screenY){
			rt.transform.localScale = new Vector3(screenX/imageWidth, screenX/imageWidth, 0);
		}
		else{
			rt.transform.localScale = new Vector3(screenY/imageHeight, screenY/imageHeight, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
