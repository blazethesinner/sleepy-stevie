using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {
	
	public GUIStyle styleButtons;
	public GUIStyle titleStyle;

	enum state{main, options, intro};
	private state currentState;

	private bool isSoundOn;
	private bool isMusicOn;

	public Texture textureShadow;
	
	public float introTimePerSlide;
	private float introTimer;
	
	public Texture2D[] imagesIntro;

	private AudioClip clip_hammerTent;
	private AudioSource hammerAudio;

	private AudioClip clip_rustling;
	private AudioSource rustlingAudio;

	private int delay = 0;



	void Start(){
		currentState = state.main;
		isSoundOn = true;
		isMusicOn = true;

		hammerAudio = (AudioSource)gameObject.AddComponent ("AudioSource");
		clip_hammerTent = (AudioClip)Resources.Load ("sfx/intro");
		hammerAudio.clip = clip_hammerTent;
		//hammerAudio.time = 0.01F;

		rustlingAudio = (AudioSource)gameObject.AddComponent ("AudioSource");
		clip_rustling = (AudioClip)Resources.Load ("sfx/intro");
		rustlingAudio.clip = clip_rustling;
		//rustlingAudio.time = 2;
	}

	void Update(){
		introTimer += Time.deltaTime;
	}


	void OnGUI () {

		if (currentState == state.main) {
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2, 300, 75), "New Game", styleButtons)) {
				//Application.LoadLevel ("game");
				currentState=state.intro;
				introTimer=0;
			}
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 100, 300, 75), "Options", styleButtons)) {
				currentState = state.options;
			}
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 200, 300, 75), "Leave Game", styleButtons)) {
				Application.Quit ();
			}
			GameObject.Find("SleepyStevie").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("Options").GetComponent<SpriteRenderer>().enabled = false;
			moveShadow();
			//GUI.Label (new Rect (100, 100, 760, 100), "Sleepy Stevie", titleStyle);
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
			GameObject.Find("SleepyStevie").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("Options").GetComponent<SpriteRenderer>().enabled = true;
			moveShadow();

		}

		if(currentState==state.intro) {
			if(introTimer == 0){
				hammerAudio.Play ();
			}
			Screen.showCursor = false;
			int numberSlide = (int)introTimer;
			if (numberSlide>=imagesIntro.Length){
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),imagesIntro[imagesIntro.Length-1]);
			}
			else {
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),imagesIntro[numberSlide]);
			}
			if (Input.anyKeyDown){
				Application.LoadLevel ("game");
			}
		}



	}

	void moveShadow(){
		Vector3 pos = new Vector3 (Input.mousePosition.x,Input.mousePosition.y,0);
		GUI.DrawTexture (new Rect (pos.x-Screen.width*3,-Screen.height-pos.y, 6*Screen.width, 4*Screen.height), textureShadow);
	}

	string onOff(bool condition){
		if (condition)
			return "On";
		else
			return "Off";
	}
}
