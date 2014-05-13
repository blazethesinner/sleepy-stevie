using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour {

	public float walkSpeed= 10;

	public Sprite left;
	public Sprite right;
	public Sprite up;
	public Sprite down;

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
	}
	
	// Update is called once per frame
	void Update () {
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
			if (direction=="up") {
				light.transform.localEulerAngles = new Vector3 (0, 0, 270);
				light.transform.localPosition = new Vector3 (0, 8, 0);
			}
			if (direction=="down") {
				light.transform.localEulerAngles = new Vector3 (0, 0, 90);
				light.transform.localPosition = new Vector3 (0, -8, 0);
			}
			if (direction=="left") {
				light.transform.localEulerAngles = new Vector3 (0, 0, 0);
				light.transform.localPosition = new Vector3 (-8, 0, 0);
			}
			if (direction=="right") {
				light.transform.localEulerAngles = new Vector3 (0, 0, 180);
				light.transform.localPosition = new Vector3 (8, 0, 0);
			}
		}
		else {
			light.transform.localEulerAngles = new Vector3 (0, 0, 0);
			light.transform.localPosition = new Vector3 (3,-1, 0);
		}


		//dumb movement
		if (direction == "up") {
			if (Input.GetKey ("w")) 
				transform.Translate (Vector2.up * walkSpeed * Time.deltaTime);
			spriterenderer.GetComponent<SpriteRenderer> ().sprite = up;
		}

		if (direction == "down") {
			if (Input.GetKey ("s")) 
				transform.Translate (-Vector2.up * walkSpeed * Time.deltaTime);
			spriterenderer.GetComponent<SpriteRenderer> ().sprite = down;
		}

		if (direction == "left") {
			if (Input.GetKey ("a")) 
				transform.Translate (-Vector2.right * walkSpeed * Time.deltaTime);
			spriterenderer.GetComponent<SpriteRenderer> ().sprite = left;
		}
		if (direction == "right") {
			if (Input.GetKey ("d")) 
				transform.Translate (Vector2.right * walkSpeed * Time.deltaTime);
			spriterenderer.GetComponent<SpriteRenderer> ().sprite = right;
		}
	}


}
