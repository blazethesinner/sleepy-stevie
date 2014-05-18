using UnityEngine;
using System.Collections;

public class LightBehaviour : MonoBehaviour {


	public Sprite flashlight;
	public Sprite nolight;

	//how much power the flashlight has left
	public int batteryLife;

	private bool isOn;
	private float lastTimeOn;
	// Use this for initialization
	void Start () {
		batteryLife = 50;
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
				if (batteryLife == 0) {
					GetComponent<SpriteRenderer>().sprite=nolight;		
				}
				else{
					GetComponent<SpriteRenderer>().sprite=flashlight;
				}
			}
			else{
				GetComponent<SpriteRenderer>().sprite=nolight;
			}
		}
		if (isOn && ((Time.time - lastTimeOn) > 0.1)) 
		{
			batteryLife = batteryLife - 10;
		}

	}
}
