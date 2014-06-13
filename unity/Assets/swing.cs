using UnityEngine;
using System.Collections;

public class swing : MonoBehaviour {

	public float duration;
	public float timer;
	public playerMovementScript player;
	public string direction;

	// Use this for initialization
	void OnEnable () {

	}

	public void startSwing(){
		timer = 0;
		direction = player.direction;

	}

	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "ennemies") {
						print (other.gameObject.transform.parent.gameObject.name);
						other.gameObject.transform.parent.gameObject.GetComponent<RabbitBehaviour> ().getHit (direction);
				} 
	}


	// Update is called once per frame
	void Update () {
		//duration control
		timer += Time.deltaTime;
		if (timer > duration)
			gameObject.SetActive (false);
	}
}
