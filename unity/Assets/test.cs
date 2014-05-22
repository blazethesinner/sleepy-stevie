using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		int xR = (int)((transform.position.x) / 19);
		int yR = (int)((transform.position.y) / 13);
		int xB = (int)((transform.position.x) % 19);
		int yB = (int)((transform.position.y) % 13);
		GUI.Label (new Rect (0, 0, 100, 100), "in : "+ "\nregion : " + xR +", " +yR + "\nblock : " + xB + ", " + yB );
	}
}
