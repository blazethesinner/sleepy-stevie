using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour {

	public float walkSpeed= 10;

	public static string hasWon;
	public static int life;

	public float swingDuration;
	public float swingCoolDown;
	private float swingCoolDownTimer;

	public float batteryDropRate;

	public float hitCoolDown;
	private float hitCoolDownTimer;

	//public AudioSource lightOn;

	public bool isPaused = false;
	private float lastTimePaused;

	public Animator myAnimator;
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
	public string direction;

	public GameObject spriterenderer;
	public GameObject light;
	public GameObject swing;
	public GameObject center;

	private AudioClip clip_lightOn;
	private AudioClip clip_lightOff;
	private AudioSource lightOn;
	private AudioSource lightOff;

	private AudioClip clip_leftFootstep;
	private AudioClip clip_rightFootstep;
	private AudioSource leftFoot;
	private AudioSource rightFoot;

	private AudioClip clip_batteryGet;
	private AudioSource batteryAudio;

	private AudioClip clip_tentGet;
	private AudioSource tentAudio;

	private AudioClip clip_swingFL;
	private AudioSource swingAudio;

	// Use this for initialization
	void Start () {
		//GameObject map = GameObject.Find ("map");
		//lightOn = map.GetComponent ("light on1");
		//spriterenderer = GameObject.Find ("playerSprite");
		//light = GameObject.Find ("Light");
		isOn = true;
		light.GetComponent<SpriteRenderer>().sprite=flashlight;
		lastTimeOn = Time.time;
		hasWon = "no";
		life = 3;
		LightBehaviour.batteryLife = 100;
		direction = "top";
		myAnimator.SetInteger ("direction", 0);
		myAnimator.enabled = false;

		lightOn = (AudioSource)gameObject.AddComponent ("AudioSource");
		lightOn.playOnAwake = false;
		lightOff = (AudioSource)gameObject.AddComponent ("AudioSource");

		clip_lightOn = (AudioClip)Resources.Load ("sfx/lightOn");
		clip_lightOff = (AudioClip)Resources.Load ("sfx/lightOff");

		lightOn.clip = clip_lightOn;
		lightOff.clip = clip_lightOff;
		//debugging, can be removed
		if (!lightOn || !lightOn.clip) {
			Debug.LogError ("Assign a Sound in the inspector.");
			return;
		}
		swingCoolDownTimer = swingCoolDown;

		//battery sounds
		batteryAudio = (AudioSource)gameObject.AddComponent ("AudioSource");
		clip_batteryGet = (AudioClip)Resources.Load ("sfx/battery_pickup");
		batteryAudio.clip = clip_batteryGet;

		//Feet sounds
		leftFoot = (AudioSource)gameObject.AddComponent ("AudioSource");
		rightFoot = (AudioSource)gameObject.AddComponent ("AudioSource");
		
		clip_leftFootstep = (AudioClip)Resources.Load ("sfx/left_footstep");
		clip_rightFootstep = (AudioClip)Resources.Load ("sfx/right_footstep");

		leftFoot.clip = clip_leftFootstep;
		rightFoot.clip = clip_rightFootstep;

		//debugging, can be removed
		if (!rightFoot || !rightFoot.clip) {
			Debug.LogError ("Assign a Sound in the inspector.");
			return;
		}


		//tent sounds
		tentAudio = (AudioSource)gameObject.AddComponent ("AudioSource");
		clip_tentGet = (AudioClip)Resources.Load ("sfx/win state");
		tentAudio.clip = clip_tentGet;

		//swing sounds
		swingAudio = (AudioSource)gameObject.AddComponent ("AudioSource");
		clip_swingFL = (AudioClip)Resources.Load ("sfx/FL_swing");
		swingAudio.clip = clip_swingFL;

	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
						//update rendering layer
						spriterenderer.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-2 * transform.position.y);
						if (isOn && LightBehaviour.batteryLife > 0) {
								LightBehaviour.batteryLife -= Time.deltaTime * batteryDropRate; 
						}
						if (LightBehaviour.batteryLife <= 0) {
								isOn = false;
								LightBehaviour.batteryLife = 0;
								//light.transform.localEulerAngles = new Vector3 (0, 0, 0);
								//light.transform.localPosition = new Vector3 (3, -1, 0);
								light.GetComponent<SpriteRenderer> ().sprite = nolight; //added this to your code : making light turning off properly :p
						} else {
								if (Input.GetKey ("space") && ((Time.time - lastTimeOn) > 0.2)) {
										lastTimeOn = Time.time;
										isOn = !isOn;
										if (isOn) {
												light.GetComponent<SpriteRenderer> ().sprite = flashlight;
												lightOn.Play();
										} else {
												light.GetComponent<SpriteRenderer> ().sprite = nolight;
												lightOff.Play();
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
								if (Time.time%2 == 1){
									leftFoot.Play();
								}
								else{
									rightFoot.Play();
								}
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
						//if (isOn) {
								if (direction == "up") {
										//light.transform.localEulerAngles = new Vector3 (0, 0, 270);
										//light.transform.localPosition = new Vector3 (0, 2, 0);
										center.transform.localEulerAngles = new Vector3 (0, 0, 270);
								}
								if (direction == "down") {
										//light.transform.localEulerAngles = new Vector3 (0, 0, 90);
										//light.transform.localPosition = new Vector3 (0, -2, 0);
										center.transform.localEulerAngles = new Vector3 (0, 0, 90);
								}
								if (direction == "left") {
										//light.transform.localEulerAngles = new Vector3 (0, 0, 0);
										//light.transform.localPosition = new Vector3 (-2, 0, 0);
										center.transform.localEulerAngles = new Vector3 (0, 0, 0);
								}
								if (direction == "right") {
										//light.transform.localEulerAngles = new Vector3 (0, 0, 180);
										//light.transform.localPosition = new Vector3 (2, 0, 0);
										center.transform.localEulerAngles = new Vector3 (0, 0, 180);
								}
						//} else {
						//		light.transform.localEulerAngles = new Vector3 (0, 0, 0);
						//		light.transform.localPosition = new Vector3 (0.75f, -0.25f, 0);
						//}


						//dumb movement
						if (direction == "up") {
								if (Input.GetKey ("w")) {
										transform.Translate (Vector2.up * walkSpeed * Time.deltaTime);
										if (!myAnimator.enabled)
											myAnimator.enabled=true;
										myAnimator.SetInteger("direction",0);
								}
								else
										if (myAnimator.enabled)
											myAnimator.enabled=false;
						}

						if (direction == "down") {
								if (Input.GetKey ("s")) {
										transform.Translate (-Vector2.up * walkSpeed * Time.deltaTime);
										if (!myAnimator.enabled)
											myAnimator.enabled=true;
										myAnimator.SetInteger("direction",1);
								}
								else
										if (myAnimator.enabled)
											myAnimator.enabled=false;
						}

						if (direction == "left") {
								if (Input.GetKey ("a")) {
										transform.Translate (-Vector2.right * walkSpeed * Time.deltaTime);
										if (!myAnimator.enabled)
											myAnimator.enabled=true;
										myAnimator.SetInteger("direction",2);
								}
								else
										if (myAnimator.enabled)
											myAnimator.enabled=false;
						}
						if (direction == "right") {
								if (Input.GetKey ("d")) {
										transform.Translate (Vector2.right * walkSpeed * Time.deltaTime);
										if (!myAnimator.enabled)
											myAnimator.enabled=true;
										myAnimator.SetInteger("direction",3);
								}
								else
									if (myAnimator.enabled)
										myAnimator.enabled=false;
						}

					//death
					if (life<=0){
						Application.LoadLevel ("endScreen");
					}
					

					//swing
					if (Input.GetKeyDown("k") && swingCoolDownTimer>swingCoolDown){
						swingAudio.Play ();
						swingCoolDownTimer=0f;
						swing.GetComponent<swing>().duration = swingDuration;
						swing.SetActive(true);
						swing.GetComponent<swing>().startSwing();

				
					}
					swingCoolDownTimer+=Time.deltaTime;
					hitCoolDownTimer+= Time.deltaTime;

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
		//pit p = otherCollider.gameObject.GetComponent<pit> ();

		if (bat != null) {
			LightBehaviour.batteryLife += bat.charge - 50; //can't tell why, but it's adding 100 rather than bat.charge's value, might just hardcode
			if(LightBehaviour.batteryLife > 100){
				LightBehaviour.batteryLife = 100;
			}
			batteryAudio.Play();
			Destroy(bat.gameObject);
		}
		//hook up to win game screen
		if (t != null) {
			//tentAudio.Play(); It doesn't play the full duration
			PlayerPrefs.SetInt("hasWon",1);
			Application.LoadLevel ("endScreen");
		}
		/*
		if (p != null) {
			if(playerMovementScript.life > 1){
				playerMovementScript.life -= 1;
			} else {
				playerMovementScript.life = 0;
				PlayerPrefs.SetInt("hasWon",0);
				Application.LoadLevel ("endScreen");
			}
		}
		*/

	}

	/* Replaced 
	void OnGUI(){
		GUI.Label (new Rect (100, 100, 10000, 20), "Flashlight " + LightBehaviour.batteryLife.ToString()); 
		GUI.Label (new Rect (100, 150, 10000, 30), "Has player won? " + playerMovementScript.hasWon); 
		GUI.Label (new Rect (100, 200, 10000, 40), "Life left " + playerMovementScript.life); 
	}*/

	public void getHit(int strenght){
		if (hitCoolDownTimer>hitCoolDown){
			print ("Aie ! I'm hit !");
			life --;
			hitCoolDownTimer=0;
		}
	}

}
