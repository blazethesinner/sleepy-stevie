using UnityEngine;
using System.Collections;

public class pit : MonoBehaviour
{
	public bool isReady;

	public Sprite on;
	public Sprite off;

	void Start (){
		isReady = true;
	}

	void Update(){
		if (isReady)
			GetComponentInChildren<SpriteRenderer> ().sprite = on;
		else
			GetComponentInChildren<SpriteRenderer> ().sprite = off;
	}
		
	// collisions
	void OnTriggerEnter2D(Collider2D other){
		if (isReady) {
						if (other.gameObject.tag == "Player")
								other.gameObject.GetComponent<playerMovementScript> ().getHit (1);
						if (other.gameObject.tag == "ennemies") {
								Object.Destroy (other.gameObject);
								print ("rabbit killed");
						}
				}
		isReady = false;
	}
}

