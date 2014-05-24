using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {
	
	public GUIStyle styleButtons;
	public GUIStyle titleStyle;

	enum state{main, options, intro};
	private state currentState;

	private bool isSoundOn;
	private bool isMusicOn;
	void Start(){
		currentState = state.main;
		isSoundOn = true;
		isMusicOn = true;
	}

	void OnGUI () {

		if (currentState == state.main) {
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2, 300, 50), "New Game", styleButtons)) {
				Application.LoadLevel ("game");
			}
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 100, 300, 50), "Options", styleButtons)) {
				currentState = state.options;
			}
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 200, 300, 50), "Leave Game", styleButtons)) {
				Application.Quit ();
			}

			GUI.Label (new Rect (100, 100, 760, 100), "Sleepy Stevie", titleStyle);
		}

		if (currentState == state.options) {
			GUI.Label (new Rect (Screen.width / 2 -50, Screen.height / 2 -100, 100, 50), "Sound :", styleButtons);
			if (GUI.Button (new Rect (Screen.width / 2 + 50, Screen.height / 2 -100, 50, 50), onOff(isSoundOn), styleButtons)) {
				isSoundOn=!isSoundOn;
				PlayerPrefs.SetString("sound",onOff(isSoundOn));
			}
			
			GUI.Label (new Rect (Screen.width / 2 -50, Screen.height / 2, 100, 50), "Music :", styleButtons);
			if (GUI.Button (new Rect (Screen.width / 2 + 50, Screen.height / 2 , 50, 50), onOff(isMusicOn), styleButtons)) {
				isMusicOn=!isMusicOn;
				PlayerPrefs.SetString("music",onOff(isMusicOn));
			}
			
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 200, 200, 50), "Back To Menu", styleButtons))
				currentState=state.main;
		}
	}

	string onOff(bool condition){
		if (condition)
			return "On";
		else
			return "Off";
	}
}
