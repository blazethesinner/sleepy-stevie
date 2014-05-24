using UnityEngine;
using System.Collections;

public class options : MonoBehaviour {

	// Use this for initialization
	public GUIStyle styleButtons;
	public string isSoundOn;
	public string isMusicOn;
	public float luminosity;

	void OnGUI () {
		// Make buttons. We pass in the GUIStyle defined above as the style to use
		GUI.Label (new Rect (Screen.width / 2 -50, Screen.height / 2 -100, 100, 50), "Sound :", styleButtons);
		if (GUI.Button (new Rect (Screen.width / 2 + 50, Screen.height / 2 -100, 50, 50), isSoundOn, styleButtons)) {
			if(isSoundOn=="On")
				isSoundOn="Off";
			else
				isSoundOn="On";
			PlayerPrefs.SetString("sound",isSoundOn);
		}

		GUI.Label (new Rect (Screen.width / 2 -50, Screen.height / 2, 100, 50), "Music :", styleButtons);
		if (GUI.Button (new Rect (Screen.width / 2 + 50, Screen.height / 2 , 50, 50), isMusicOn, styleButtons)) {
			if(isMusicOn=="On")
				isMusicOn="Off";
			else
				isMusicOn="On";
			PlayerPrefs.SetString("music",isMusicOn);
		}

		if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 200, 200, 50), "Back To Menu", styleButtons))
				Application.LoadLevel ("mainMenu");
	}
}
