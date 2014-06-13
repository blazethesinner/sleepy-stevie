using UnityEngine;
using System.Collections;

public class pit : MonoBehaviour
{
	public bool isReady;

	public float resetTime;
	private float reset;

	public Sprite on;
	public Sprite off;

	private AudioClip clip_StevieHit;
	private AudioSource trap;

	void Start (){
		isReady = true;
		trap = (AudioSource)gameObject.AddComponent ("AudioSource");
		clip_StevieHit = (AudioClip)Resources.Load ("sfx/stevie_caught_in_trap");
		trap.clip = clip_StevieHit;
		reset = 0f;
	}

	void Update(){
		reset += Time.deltaTime;

		if (isReady)
				GetComponentInChildren<SpriteRenderer> ().sprite = on;
		else {
				GetComponentInChildren<SpriteRenderer> ().sprite = off;
				if(reset>resetTime)
					isReady=true;
			}
	}
	// collisions
	void OnTriggerStay2D(Collider2D other){
		if (isReady) {
						if (other.gameObject.tag == "Player"){
								if(other.gameObject.GetComponent<playerMovementScript>().isVulnerable){
									other.gameObject.GetComponent<playerMovementScript> ().getHit (1);
									isReady = false;
									reset=0f;
									trap.Play();
								}
						}
						if (other.gameObject.tag == "ennemies") {
								Object.Destroy (other.gameObject.transform.parent.gameObject);
								print ("rabbit killed");
								isReady = false;
						}
		}

	}
}

