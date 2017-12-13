using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {
	public int x, y;
	public int myX, myY;
	GameControl myMouse;
	//GameControl myKey; 

	// Use this for initialization
	void Start () {
		myMouse = GameObject.Find("GameControl").GetComponent<GameControl>();
		//myKey = GameObject.Find ("GameControl").GetComponent<GameControl> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown () {
		myMouse.ProcessClick (gameObject, myX, myY);
	}
//	void KeyInput () {
//		myKey.ProcessKeyboard ();
//	}
}
