using UnityEngine;
using System.Collections;

public class GUIBarScript : MonoBehaviour {

	public GUIStyle styleButtons;
	public GUIStyle flashlightBar;
	public Texture emptyBattery;
	public Texture fullBattery;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Label (new Rect (100, 30, 100, 50), "Battery :" + ((int)LightBehaviour.batteryLife).ToString() + "%", styleButtons);
		GUI.Label (new Rect (200, 30, 100, 50), "Life :" + playerMovementScript.life, styleButtons);
		GUI.Label (new Rect (300, 30, 100, 50), "Time :" + (int)Time.time, styleButtons);


		/* Still working on image
		 * GUI.DrawTexture (new Rect (100, 30, 100, 50), fullBattery);
		GUI.Label (new Rect (100, 30, 100, 50), fullBattery, styleButtons);

		GUI.BeginGroup (new Rect (100, 30, 100, 50));

		GUI.Box (new Rect (100, 30, 100, 50), fullBattery, styleButtons);

		//GUI.BeginGroup (new Rect (0, 0, (float) LightBehaviour.batteryLife * 256, 32));

		//GUI.Box (new Rect (0, 0, 256, 32), fullBattery, flashlightBar);

		//GUI.EndGroup ();
		GUI.EndGroup ();*/

	}
}
