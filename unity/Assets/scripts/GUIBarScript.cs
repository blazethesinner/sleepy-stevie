using UnityEngine;
using System.Collections;

public class GUIBarScript : MonoBehaviour {

	public GUIStyle styleButtons;
	public GUIStyle flashlightBar;
	public Texture emptyBattery;
	public Texture fullBattery;
	//public Texture emptyHealth;
	//public Texture fullHealth;
	public Texture heart;
	public Texture pearl;
	public Texture back;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnGUI () {
			//GUI.Label (new Rect (100, 30, 100, 50), "Battery :" + ((int)LightBehaviour.batteryLife).ToString() + "%", styleButtons);
			//GUI.Label (new Rect (200, 30, 100, 50), "Life :" + playerMovementScript.life, styleButtons);
			GUI.Label (new Rect (260, 30, 100, 300), "Time :" + (int)Time.time, styleButtons);
			fullBattery = (Texture)Resources.Load ("newfullBattery2");
			emptyBattery = (Texture)Resources.Load ("newemptyBattery2"); 
			heart = (Texture)Resources.Load ("heart");
			pearl = (Texture)Resources.Load ("pearl");
			back = (Texture)Resources.Load ("guiBackdrop");
	
			//fullHealth = (Texture) Resources.Load ("fullHealth");
			//emptyHealth = (Texture) Resources.Load ("emptyHealth"); 
	
			if (!fullBattery || !emptyBattery || !heart || !pearl || !back) {
					Debug.LogError ("Assign a GUI Texture in the inspector.");
					return;
			}
			//Background of gui
			GUI.DrawTexture (new Rect (00, 15, 1250, 80), back);

			//Timer
			GUI.Label (new Rect (1020, 30, 100, 300), "Time :" + (int)Time.time, styleButtons);

			//Battery
			GUI.DrawTexture (new Rect (50, 40, 100, 30), emptyBattery);
	
			GUI.BeginGroup (new Rect (50, 40, (int)LightBehaviour.batteryLife, 30));
			GUI.DrawTexture (new Rect (0, 0, 100, 30), fullBattery);
	
			GUI.EndGroup ();
	
			//Health
			for (int i=1; i<=(int)playerMovementScript.life; i++) {
					GUI.BeginGroup (new Rect (147 + i * 33, 30, 33, 50));
					GUI.DrawTexture (new Rect (0, 0, 33, 50), heart);
		
					GUI.EndGroup ();
			}
			for (int i=3; i>(int)playerMovementScript.life; i--) {
					GUI.BeginGroup (new Rect (147 + i * 33, 30, 33, 50));
					GUI.DrawTexture (new Rect (0, 0, 33, 50), pearl);
		
					GUI.EndGroup ();
			}
		}
}
