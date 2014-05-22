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
		myOrder = order.left;
	}

	void Update(){
		updateState ();
		computeAI ();
		applyOrder ();
	}
	
	public void updateState()// change state depending on inlight or not, and changes characteristics
	{
		myState = state.light; //for now, the state doesn't change



	} 

	void OnCollisionEnter(Collision collide)
	{
		if(myOrder==order.right)
			myOrder=order.left;
		else
			myOrder=order.right;
	}

	public void computeAI()//change order and behaviour depending on lots of things
	{
		//ruled by oncollisionenter
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

}
