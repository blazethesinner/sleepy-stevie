using UnityEngine;
using System.Collections;

public class ennemyBoxes : MonoBehaviour {
	public GameObject animal;

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
			animal.SendMessage (this.name+"stay");
		if (other.gameObject.tag == "Player") {
			//if(this.gameObject == rabbit){
				if(other.gameObject.GetComponent<playerMovementScript>().isVulnerable){
					if (animal.GetComponent<RabbitBehaviour>().myType==RabbitBehaviour.animalType.rabbit)
				    	other.gameObject.GetComponent<playerMovementScript>().bunnyCollision.Play();
					if (animal.GetComponent<RabbitBehaviour>().myType==RabbitBehaviour.animalType.bear)
						other.gameObject.GetComponent<playerMovementScript>().bearCollision.Play();
				}
			//}
			other.gameObject.GetComponent<playerMovementScript> ().getHit (animal.GetComponent<RabbitBehaviour> ().strength);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag=="obstacle")
		animal.SendMessage (this.name+"exit");
	}
}
