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

	public bool isPaused;// = false;
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
	public bool isVulnerable;

	public GameObject spriterenderer;
	public GameObject light;
	public GameObject swing;
	public GameObject center;

	public Texture imagePause;

	private AudioClip clip_lightOn;
	private AudioClip clip_lightOff;
	private AudioSource lightOn;
	private AudioSource lightOff;

	private float lastTimeStep;

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

	private AudioClip clip_bunnyNoise;
	public AudioSource bunnyCollision;

	// Use this for initialization
	void Start () {

		//GameObject map = GameObject.Find ("map");
		//lightOn = map.GetComponent ("light on1");
		//spriterenderer = GameObject.Find ("playerSprite");
		//light = GameObject.Find ("Light");
		isPaused = false;
		Screen.showCursor = false;
		PlayerPrefs.SetInt("hasWon",0);

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

		clip_lightOn = (AudioClip)Resources.Load ("sfx/light On");
		clip_lightOff = (AudioClip)Resources.Load ("sfx/light Off");

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
		lastTimeStep = Time.time;
		leftFoot = (AudioSource)gameObject.AddComponent ("AudioSource");
		rightFoot = (AudioSource)gameObject.AddComponent ("AudioSource");
		
		clip_leftFootstep = (AudioClip)Resources.Load ("sfx/left_footstep");
		clip_rightFootstep = (AudioClip)Resources.Load ("sfx/right_footstep");

		leftFoot.clip = clip_leftFootstep;
		leftFoot.time = 1;
		rightFoot.clip = clip_rightFootstep;
		rightFoot.time = 1;

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

		//bunny sounds
		bunnyCollision = (AudioSource)gameObject.AddComponent ("AudioSource");
		clip_bunnyNoise = (AudioClip)Resources.Load ("sfx/rabbit");
		bunnyCollision.clip = clip_bunnyNoise;
		bunnyCollision.time = 1;
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
							lightOn.Play ();
					} else {
							light.GetComponent<SpriteRenderer> ().sprite = nolight;
							lightOff.Play ();
					}
				}
			}
			//pause game
			if (Input.GetKeyDown ("p") && ((Time.time - lastTimePaused) > 0.2)) {
				lastTimePaused = Time.time;
				Time.timeScale = 0;	
				isPaused = true;
			}

			//changing direction according to inputs
			if (Input.GetKey ("w") && !Input.GetKey ("s") && !Input.GetKey ("a") && !Input.GetKey ("d")) {
				if (Time.time % 2 == 1 && (Time.time - lastTimeStep) > 0.2) {
						leftFoot.Play ();
					Debug.LogError ("Left foot played");
				} else if(Time.time % 2 == 0 && (Time.time - lastTimeStep) > 0.2) {
						rightFoot.Play ();
					Debug.LogError ("Right foot played");
				}
				lastTimeStep = Time.time;
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

			//light position
			if (isOn)
				light.transform.localPosition = new Vector2 (-3, 0.5f);
			else
				light.transform.localPosition = new Vector2 (0.3f, 0.2f);

			//light direction
			if (direction == "up") {
				center.transform.localEulerAngles = new Vector3 (0, 0, 270);
			}
			if (direction == "down") {
				center.transform.localEulerAngles = new Vector3 (0, 0, 90);
			}
			if (direction == "left") {
				center.transform.localEulerAngles = new Vector3 (0, 0, 0);
			}
			if (direction == "right") {
				center.transform.localEulerAngles = new Vector3 (0, 0, 180);
			}


			//dumb movement
			if (direction == "up") {
					if (Input.GetKey ("w")) {
						transform.Translate (Vector2.up * walkSpeed * Time.deltaTime);
						if (!myAnimator.enabled)
							myAnimator.enabled = true;
						myAnimator.SetInteger ("direction", 0);
					} else
						if (myAnimator.enabled)
							myAnimator.enabled = false;
			}

			if (direction == "down") {
				if (Input.GetKey ("s")) {
					transform.Translate (-Vector2.up * walkSpeed * Time.deltaTime);
					if (!myAnimator.enabled)
						myAnimator.enabled = true;
					myAnimator.SetInteger ("direction", 1);
				} else
					if (myAnimator.enabled)
						myAnimator.enabled = false;
			}

			if (direction == "left") {
				if (Input.GetKey ("a")) {
					transform.Translate (-Vector2.right * walkSpeed * Time.deltaTime);
					if (!myAnimator.enabled)
						myAnimator.enabled = true;
					myAnimator.SetInteger ("direction", 2);
				} else
					if (myAnimator.enabled)
						myAnimator.enabled = false;
			}
			if (direction == "right") {
				if (Input.GetKey ("d")) {
					transform.Translate (Vector2.right * walkSpeed * Time.deltaTime);
					if (!myAnimator.enabled)
						myAnimator.enabled = true;
					myAnimator.SetInteger ("direction", 3);
				} else
					if (myAnimator.enabled)
						myAnimator.enabled = false;
			}

			//death
			if (life <= 0) {
				PlayerPrefs.SetInt ("timer", (int)GameObject.Find ("GUI").GetComponent<GUIBarScript> ().timer);
				Application.LoadLevel ("endScreen");
			}
		

			//swing
			if (Input.GetKeyDown ("k") && swingCoolDownTimer > swingCoolDown) {
				swingAudio.Play ();
				swingCoolDownTimer = 0f;
				swing.GetComponent<swing> ().duration = swingDuration;
				swing.SetActive (true);
				swing.GetComponent<swing> ().startSwing ();
			}
		
			//timers...
		
			swingCoolDownTimer += Time.deltaTime;
			hitCoolDownTimer += Time.deltaTime;
			if (hitCoolDownTimer > hitCoolDown)
				isVulnerable = true;

		} else {
			if (Input.GetKeyDown ("p") && ((Time.time - lastTimeOn) > 0.2)) {
				lastTimePaused = Time.time;
				Time.timeScale = 1;	
				isPaused = false;
			}
			if (Input.GetKeyDown ("m")) {
				Screen.showCursor = true;
				//lastTimePaused = 0;
				//isPaused = false;
				Application.LoadLevel("mainMenu");
			}
		}
	}
	

	void OnGUI(){
		if(isPaused)
			GUI.Label(new Rect(Screen.width/2-200,Screen.height/2-100,400,200),imagePause);
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
			PlayerPrefs.SetInt("timer",(int)GameObject.Find("GUI").GetComponent<GUIBarScript>().timer);
			PlayerPrefs.SetInt("hasWon",1);
			Application.LoadLevel ("endScreen");
		}

	}

	public void getHit(int strenght){
		if (isVulnerable){
			life --;
			hitCoolDownTimer=0;
			isVulnerable=false;
			//bunnyCollision.time = 1;
			//bunnyCollision.Play();
		}
	}

}
