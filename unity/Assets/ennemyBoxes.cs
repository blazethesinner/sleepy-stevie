﻿using UnityEngine;
using System.Collections;

public class ennemyBoxes : MonoBehaviour {
	public GameObject rabbit;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		//if (other.gameObject.tag == "Player")
		//	other.gameObject.GetComponent<playerMovementScript> ().getHit (rabbit.GetComponent<RabbitBehaviour>().strength);
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag=="obstacle")
			rabbit.SendMessage (this.name+"stay");
		if (other.gameObject.tag == "Player") {
			//if(this.gameObject == rabbit){
				if(other.gameObject.GetComponent<playerMovementScript>().isVulnerable)
					other.gameObject.GetComponent<playerMovementScript>().bunnyCollision.Play();
			//}
			other.gameObject.GetComponent<playerMovementScript> ().getHit (rabbit.GetComponent<RabbitBehaviour> ().strength);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag=="obstacle")
		rabbit.SendMessage (this.name+"exit");
	}
}
