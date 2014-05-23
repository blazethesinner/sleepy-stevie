using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour {

	public float walkSpeed= 10;

	public static string hasWon;
	public static int life;

	public bool isPaused = false;
	private float lastTimePaused;

	public Sprite leftOff;
	public Sprite rightOff;
	public Sprite upOff;
	public Sprite downOff;

	public Sprite leftOn;
	public Sprite rightOn;
	public Sprite upOn;
	public Sprite downOn; 

	public Sprite flashlight;
	public Sprite nolight;
	
	private bool isOn;
	private float lastTimeOn;
	private string direction;

	private GameObject spriterenderer;
	private GameObject light;
	// Use this for initialization

	void Start () {
		spriterenderer = GameObject.Find ("playerSprite");
		light = GameObject.Find ("Light");
		isOn = true;
		light.GetComponent<SpriteRenderer>().sprite=flashlight;
		lastTimeOn = Time.time;
		hasWon = "no";
		life = 3;
		LightBehaviour.batteryLife = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
						//update rendering layer
						spriterenderer.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-2 * transform.position.y);
						if (isOn && LightBehaviour.batteryLife > 0) {
								LightBehaviour.batteryLife -= .2;
						}
						if (LightBehaviour.batteryLife <= 0) {
								isOn = false;
								LightBehaviour.batteryLife = 0;
								light.transform.localEulerAngles = new Vector3 (0, 0, 0);
								light.transform.localPosition = new Vector3 (3, -1, 0);
								light.GetComponent<SpriteRenderer> ().sprite = nolight; //added this to your code : making light turning off properly :p
						} else {
								if (Input.GetKey ("space") && ((Time.time - lastTimeOn) > 0.2)) {
										lastTimeOn = Time.time;
										isOn = !isOn;
										if (isOn) {
												light.GetComponent<SpriteRenderer> ().sprite = flashlight;
										} else {
												light.GetComponent<SpriteRenderer> ().sprite = nolight;
										}
								}
						}
						//pause game
						if (Input.GetKey ("p") && ((Time.time - lastTimeOn) > 0.2)) {
								lastTimePaused = Time.time;
								Time.timeScale = 0;	
								isPaused = true;
						}

						//changing direction according to inputs
						if (Input.GetKey ("w") && !Input.GetKey ("s") && !Input.GetKey ("a") && !Input.GetKey ("d")) {
								direction = "up";	
						}
						if (!Input.GetKey ("w") && Input.GetKey ("s") && !Input.GetKey ("a") && !Input.GetKey ("d")) {
								direction = "down";
						}
						if (!Input.GetKey ("w") && !Input.GetKey ("s") && Input.GetKey ("a") && !Input.GetKey ("d")) {
								direction = "left";
						}
						if (!Input.GetKey ("w") && !Input.GetKey ("s") && !Input.GetKey ("a") && Input.GetKey ("d")) {
								direction = "right";
						}

						//light direction
						if (isOn) {
								if (direction == "up") {
										light.transform.localEulerAngles = new Vector3 (0, 0, 270);
										light.transform.localPosition = new Vector3 (0, 2, 0);
								}
								if (direction == "down") {
										light.transform.localEulerAngles = new Vector3 (0, 0, 90);
										light.transform.localPosition = new Vector3 (0, -2, 0);
								}
								if (direction == "left") {
										light.transform.localEulerAngles = new Vector3 (0, 0, 0);
										light.transform.localPosition = new Vector3 (-2, 0, 0);
								}
								if (direction == "right") {
										light.transform.localEulerAngles = new Vector3 (0, 0, 180);
										light.transform.localPosition = new Vector3 (2, 0, 0);
								}
						} else {
								light.transform.localEulerAngles = new Vector3 (0, 0, 0);
								light.transform.localPosition = new Vector3 (0.75f, -0.25f, 0);
						}


						//dumb movement
						if (direction == "up") {
								if (Input.GetKey ("w")) 
										transform.Translate (Vector2.up * walkSpeed * Time.deltaTime);
								if (isOn)
										spriterenderer.GetComponent<SpriteRenderer> ().sprite = upOn;
								else
										spriterenderer.GetComponent<SpriteRenderer> ().sprite = upOff;
						}

						if (direction == "down") {
								if (Input.GetKey ("s")) 
										transform.Translate (-Vector2.up * walkSpeed * Time.deltaTime);
								if (isOn)
										spriterenderer.GetComponent<SpriteRenderer> ().sprite = downOn;
								else
										spriterenderer.GetComponent<SpriteRenderer> ().sprite = downOff;
						}

						if (direction == "left") {
								if (Input.GetKey ("a")) 
										transform.Translate (-Vector2.right * walkSpeed * Time.deltaTime);
								if (isOn)
										spriterenderer.GetComponent<SpriteRenderer> ().sprite = leftOn;
								else
										spriterenderer.GetComponent<SpriteRenderer> ().sprite = leftOff;
						}
						if (direction == "right") {
								if (Input.GetKey ("d")) 
										transform.Translate (Vector2.right * walkSpeed * Time.deltaTime);
								if (isOn)
										spriterenderer.GetComponent<SpriteRenderer> ().sprite = rightOn;
								else
										spriterenderer.GetComponent<SpriteRenderer> ().sprite = rightOff;
						}
				} else {
			if (Input.GetKey ("p") && ((Time.time - lastTimeOn) > 0.2)) {
					lastTimePaused = Time.time;
					Time.timeScale = 1;	
					isPaused = false;
				}
			}

	}

	void OnTriggerEnter2D(Collider2D otherCollider){
		battery bat = otherCollider.gameObject.GetComponent<battery> ();
		tent t = otherCollider.gameObject.GetComponent<tent> ();
		pit p = otherCollider.gameObject.GetComponent<pit> ();

		if (bat != null) {
			LightBehaviour.batteryLife += bat.charge - 50; //can't tell why, but it's adding 100 rather than bat.charge's value, might just hardcode
			if(LightBehaviour.batteryLife > 100){
				LightBehaviour.batteryLife = 100;
			}
			Destroy(bat.gameObject);
		}
		//hook up to win game screen
		if (t != null) {
			Application.LoadLevel ("youWon");
		}

		if (p != null) {
			if(playerMovementScript.life > 1){
				playerMovementScript.life -= 1;
			} else {
				playerMovementScript.life = 0;
				Application.LoadLevel ("youLose");
			}
		}

	}

	/* Replaced 
	void OnGUI(){
		GUI.Label (new Rect (100, 100, 10000, 20), "Flashlight " + LightBehaviour.batteryLife.ToString()); 
		GUI.Label (new Rect (100, 150, 10000, 30), "Has player won? " + playerMovementScript.hasWon); 
		GUI.Label (new Rect (100, 200, 10000, 40), "Life left " + playerMovementScript.life); 
	}*/

}
