using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {
	
	public GUIStyle styleButtons;
	
	void OnGUI () {
		// Make a button. We pass in the GUIStyle defined above as the style to use
		if (GUI.Button (new Rect (Screen.width/2-150, Screen.height/2, 300, 50), "New Game, cauz' I'm OP", styleButtons)) {
			Application.LoadLevel ("game");
		}
		if (GUI.Button (new Rect (Screen.width/2-150, Screen.height/2+100, 300, 50), "Options, cauz' I wanna cheat", styleButtons)) {
			Application.LoadLevel ("optionScreen");
		}
		if (GUI.Button (new Rect (Screen.width/2-150, Screen.height/2+200, 300, 50), "Leave, cauz' I'm a coward", styleButtons)) {
			Application.Quit();
		}
	}
	

}
