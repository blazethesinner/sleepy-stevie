using UnityEngine;
using System.Collections;

public class pit : MonoBehaviour
{
	public bool isReady;

	public Sprite on;
	public Sprite off;

	private AudioClip clip_StevieHit;
	private AudioSource trap;

	void Start (){
		isReady = true;
		trap = (AudioSource)gameObject.AddComponent ("AudioSource");
		clip_StevieHit = (AudioClip)Resources.Load ("sfx/stevie_caught_in_trap");
		trap.clip = clip_StevieHit;
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
						if (other.gameObject.tag == "Player"){
								other.gameObject.GetComponent<playerMovementScript> ().getHit (1);
								isReady = false;
				trap.Play();
						}
						if (other.gameObject.tag == "ennemies") {
								Object.Destroy (other.gameObject.transform.parent.gameObject);
								print ("rabbit killed");
								isReady = false;
						}
				}

	}
}

