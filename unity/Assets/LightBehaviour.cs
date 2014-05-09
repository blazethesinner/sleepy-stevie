using UnityEngine;
using System.Collections;

public class LightBehaviour : MonoBehaviour {


	public Sprite flashlight;
	public Sprite nolight;

	private bool isOn;
	private float lastTimeOn;
	// Use this for initialization
	void Start () {
		isOn = false;
		GetComponent<SpriteRenderer>().sprite=nolight;
		lastTimeOn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
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
	}
}
