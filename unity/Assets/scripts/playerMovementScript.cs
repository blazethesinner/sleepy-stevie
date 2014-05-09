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
		if (Input.GetKey ("w"))
		{
			//rigidbody2D.AddForce(Vector2.up*10);
			transform.Translate(Vector2.up * walkSpeed * Time.deltaTime);
		}
		if (Input.GetKey ("s"))
		{
			//rigidbody2D.AddForce(-Vector2.up*10);
			transform.Translate(-Vector2.up * walkSpeed *Time.deltaTime);
		}
		if (Input.GetKey ("a"))
		{
			//rigidbody2D.AddForce(-Vector2.right*10);
			transform.Translate(-Vector2.right * walkSpeed *Time.deltaTime);
		}
		if (Input.GetKey ("d"))
		{
			//rigidbody2D.AddForce(Vector2.right*10);
			transform.Translate(Vector2.right * walkSpeed *Time.deltaTime);
		}
	}
}
