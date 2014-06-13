using UnityEngine;
using System.Collections;

public class endScreen : MonoBehaviour {

	public GUIStyle styleButtons;
	public GUIStyle styleTimer;
	private bool hasWon;

	private AudioClip clip_playerWon;
	public AudioSource winningAudio;
	private AudioClip clip_playerLost;
	public AudioSource losingAudio;


	// Use this for initialization
	void Start () {
		Screen.showCursor = true;
		int hasWonStr = PlayerPrefs.GetInt ("hasWon");
		hasWon = (hasWonStr == 1);

		winningAudio = (AudioSource)gameObject.AddComponent ("AudioSource");
		clip_playerLost = (AudioClip)Resources.Load ("sfx/MattWin");
		winningAudio.clip = clip_playerLost;

		losingAudio = (AudioSource)gameObject.AddComponent ("AudioSource");
		clip_playerLost = (AudioClip)Resources.Load ("sfx/scream");
		losingAudio.clip = clip_playerLost;
		if(hasWon)
			winningAudio.Play ();
		else
			losingAudio.Play();
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
			GUI.Label (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 75), "TIME : " + PlayerPrefs.GetInt ("timer") + " s", styleTimer);

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
