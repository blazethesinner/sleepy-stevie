using UnityEngine;
using System.Collections;

public class GUIBarScript : MonoBehaviour {

	public GUIStyle styleButtons;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Label (new Rect (100, 30, 100, 50), "Battery :", styleButtons);
		GUI.Label (new Rect (200, 30, 100, 50), "Life :", styleButtons);
		GUI.Label (new Rect (300, 30, 100, 50), "Time :", styleButtons);
	}
}
