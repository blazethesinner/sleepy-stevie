﻿using UnityEngine;
using System.Collections;

public class endScreen : MonoBehaviour {

	public GUIStyle styleButtons;
	private bool hasWon;
	// Use this for initialization
	void Start () {
		int hasWonStr = PlayerPrefs.GetInt ("hasWon");
		hasWon = (hasWonStr == 1);

	}
	
	// Update is called once per frame
	void Update () {
		if (hasWon) {
			GameObject.Find("YouLose").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("YouWon").GetComponent<SpriteRenderer>().enabled = true;
		}
		else{
			GameObject.Find("YouLose").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("YouWon").GetComponent<SpriteRenderer>().enabled = false;
		}
	}
	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2, 300, 75), "New Game", styleButtons)) {
			Application.LoadLevel ("game");
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 100, 300, 75), "Main Menu", styleButtons)) {
			Application.LoadLevel ("mainMenu");
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 200, 300, 75), "Leave Game", styleButtons)) {
			Application.Quit ();
		}
	}
}
