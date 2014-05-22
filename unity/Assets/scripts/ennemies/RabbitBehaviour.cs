using UnityEngine;
using System.Collections;

public class RabbitBehaviour :  MonoBehaviour {

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
	
	public void updateState(){} // change state depending on inlight or not, and changes characteristics
	
	public void computeAI(){} //change order and behaviour depending on lots of things
	
	public void applyOrder(){} //move sprite depending on order
}
