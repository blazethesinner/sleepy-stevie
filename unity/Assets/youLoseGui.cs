using UnityEngine;
using System.Collections;

public class youLoseGui : MonoBehaviour {

	public GUIStyle styleButtons;

	void OnGUI () {
		// Make a button. We pass in the GUIStyle defined above as the style to use
		GUI.Label (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 200, 300, 100), "YOU LOOOOOOOOOSE ", styleButtons);
		if (GUI.Button (new Rect (Screen.width/2-150, Screen.height/2, 300, 50), "Wanna Play Again !", styleButtons)) {
			Application.LoadLevel ("game");
		}

		if (GUI.Button (new Rect (Screen.width/2-150, Screen.height/2+100, 300, 50), "Leave, cauz' I'm a naab", styleButtons)) {
			Application.Quit();
		}
	}
}
