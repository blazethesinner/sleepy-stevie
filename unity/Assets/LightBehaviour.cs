using UnityEngine;
using System.Collections;

public class LightBehaviour : MonoBehaviour {


	public Sprite flashlight;
	public Sprite nolight;

	private bool isOn;
	private float lastTimeOn;
	private string direction;

	// Use this for initialization
	void Start () {
		isOn = true;
		GetComponent<SpriteRenderer>().sprite=flashlight;
		lastTimeOn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		//turning on/off the light
		if (Input.GetKey ("space") && ((Time.time-lastTimeOn)>0.1))
		{
			lastTimeOn=Time.time;
			isOn=!isOn;
			if(isOn){
				GetComponent<SpriteRenderer>().sprite=flashlight;
			}
			else{
				GetComponent<SpriteRenderer>().sprite=nolight;
			}
		}

		//changing direction accordingly to inputs
		if (Input.GetKey ("w")) {
			direction = "up";	
		}
		if (Input.GetKey ("s")) {
			direction = "down";
		}
		if (Input.GetKey ("a")) {
			direction = "left";
		}
		if (Input.GetKey ("d")) {
			direction = "right";
		}

		//changing transform accordingly to direction
		if (isOn) {
			if (direction=="up") {
				transform.localEulerAngles = new Vector3 (0, 0, 270);
				transform.localPosition = new Vector3 (0, 8, 0);
			}
			if (direction=="down") {
				transform.localEulerAngles = new Vector3 (0, 0, 90);
				transform.localPosition = new Vector3 (0, -8, 0);
			}
			if (direction=="left") {
				transform.localEulerAngles = new Vector3 (0, 0, 0);
				transform.localPosition = new Vector3 (-8, 0, 0);
			}
			if (direction=="right") {
				transform.localEulerAngles = new Vector3 (0, 0, 180);
				transform.localPosition = new Vector3 (8, 0, 0);
			}
		}
		else {
			transform.localEulerAngles = new Vector3 (0, 0, 0);
			transform.localPosition = new Vector3 (3,-1, 0);
		}


	}
}
