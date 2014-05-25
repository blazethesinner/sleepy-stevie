using UnityEngine;
using System.Collections;

public class RabbitBehaviour :  MonoBehaviour {

	public int maxHealth;
	public int walkSpeed;

	private int health;
	public enum state {light, dark};
	public enum behaviour {noMove, chasingTarget, wander};
	public enum order {stay, right, left, up, down};
	
	public state myState;
	public behaviour myBehaviour;
	public order myOrder;

	public GameObject map;


	//collider boxes for detection of walls
	private BoxCollider2D leftBox;
	private BoxCollider2D rightBox;
	private BoxCollider2D topBox;
	private BoxCollider2D botBox;


	
	// Use this for initialization
	public static string toString(state sta){
		switch (sta) {
		case state.dark:
			return "dark";
		case state.light:
			return "light";
		default :
			return "";
		}
	}
	public static string toString (behaviour beh){
		switch (beh) {
		case behaviour.chasingTarget :
			return "chasing target";
		case behaviour.noMove :
			return "no move";
		case behaviour.wander :
			return "wander";
		default :
			return "";
		}
	}
	public static string toString (order ord){
		switch (ord) {
		case order.down :
			return "down";
		case order.up :
			return "up";
		case order.left :
			return "left";
		case order.right :
			return "right";
		case order.stay :
			return "stay";
		default :
			return "";
		}
	}


	void Start(){
		myOrder = order.stay;
		map = GameObject.Find ("map");
	}

	void Update(){
		updateState ();
		computeAI ();
		applyOrder ();
	}

	//debug
	
	public void updateState()// change state depending on inlight or not, and changes characteristics
	{
		myState = state.light; //for now, the state doesn't change



	} 

	public void computeAI()//change order and behaviour depending on lots of things
	{
		/*
		if (myOrder == order.left) {
			int tmp = whatsIn(transform.position);
			if (tmp==1 || tmp ==2 || tmp == 3 || tmp == 4)
				myOrder= order.right;
		}
		else{
			if (myOrder == order.right){
				int tmp = whatsIn(transform.position-Vector3.left);
				if (tmp==1 || tmp ==2 || tmp == 3 || tmp == 4)
					myOrder= order.left;
			}
		}
		*/
	} 
	
	public void applyOrder()//move sprite depending on order, play animation according to state, etc
	{
		switch (myOrder) {
		case order.right :
			transform.Translate (Vector2.right * walkSpeed * Time.deltaTime);
			break;
		case order.left :
			transform.Translate (-Vector2.right * walkSpeed * Time.deltaTime);
			break;
		case order.up :
			transform.Translate (Vector2.up * walkSpeed * Time.deltaTime);
			break;
		case order.down :
			transform.Translate (-Vector2.up * walkSpeed * Time.deltaTime);
			break;
		default :
			break;
		}
	}

	int whatsIn (Vector3 vec){
		int xR = (int)((vec.x) / 19);
		int yR = (int)((vec.y) / 13);
		int xB = (int)((vec.x) % 19);
		int yB = (int)((vec.y) % 13);
		GameObject myRegion = map.GetComponent<mapCreator> ().regionMatrix [yR, xR];
		int ans = myRegion.GetComponent<regionCreator> ().matrix [yB, xB];
		print (ans);
		return ans;
	}

}
