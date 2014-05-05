using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour {

	public float walkSpeed= 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//dumb movement
		if (Input.GetKey ("up"))
		{
			transform.Translate(Vector2.up * walkSpeed * Time.deltaTime);
		}
		if (Input.GetKey ("down"))
		{
			transform.Translate(-Vector2.up * walkSpeed *Time.deltaTime);
		}
		if (Input.GetKey ("left"))
		{
			transform.Translate(-Vector2.right * walkSpeed *Time.deltaTime);
		}
		if (Input.GetKey ("right"))
		{
			transform.Translate(Vector2.right * walkSpeed *Time.deltaTime);
		}
	}
}
