using UnityEngine;
using System.Collections;

public class GUIBarScript : MonoBehaviour {

	public GUIStyle styleButtons;
	public GUIStyle flashlightBar;
	public Texture emptyBattery;
	public Texture fullBattery;
	public Texture emptyHealth;
	public Texture fullHealth;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Label (new Rect (100, 30, 100, 50), "Battery :" + ((int)LightBehaviour.batteryLife).ToString() + "%", styleButtons);
		GUI.Label (new Rect (200, 30, 100, 50), "Life :" + playerMovementScript.life, styleButtons);
		GUI.Label (new Rect (300, 30, 100, 50), "Time :" + (int)Time.time, styleButtons);
		fullBattery = (Texture) Resources.Load ("fullBattery");
		emptyBattery = (Texture) Resources.Load ("emptyBattery"); 
		fullHealth = (Texture) Resources.Load ("fullHealth");
		emptyHealth = (Texture) Resources.Load ("emptyHealth"); 
		
		if(!fullBattery || !emptyBattery || !fullHealth || !emptyHealth){
			Debug.LogError("Assign a GUI Texture in the inspector.");
			return;
		}
		
		
		GUI.DrawTexture (new Rect (100, 30, 100, 50), emptyBattery);
		
		GUI.BeginGroup (new Rect (100, 30, (int)LightBehaviour.batteryLife, 50));
		GUI.DrawTexture (new Rect (0, 0, 100, 50), fullBattery);
		
		GUI.EndGroup ();
		
		
		
		
		
		GUI.DrawTexture (new Rect (200, 30, 100, 50), emptyHealth);
		//GUI.DrawTexture (new Rect (200, 30, 100/3 * (int)playerMovementScript.life, 50), fullHealth);
		
		GUI.BeginGroup (new Rect (200, 30, 100/3 * (int)playerMovementScript.life, 50));
		GUI.DrawTexture (new Rect (0, 0, 100, 50), fullHealth);
		
		GUI.EndGroup ();

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
