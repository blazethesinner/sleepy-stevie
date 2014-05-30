using UnityEngine;
using System.Collections;

public class RabbitBehaviour :  MonoBehaviour {

	//characteristics
	public int maxHealth;
	public int walkSpeed;
	public int strength;
	private int health;

	//enums
	public enum state {light, dark};
	public enum behaviour {noMove, chasingTarget, wander};
	public enum order {stay, right, left, up, down};

	//public state myState;
	public behaviour myBehaviour;
	public order myOrder;
	public order lastOrder;
	//references to game objects

	public GameObject map;
	//collider boxes for detection of walls
	private BoxCollider2D leftBox;
	private BoxCollider2D rightBox;
	private BoxCollider2D topBox;
	private BoxCollider2D botBox;

	public Animator myAnimator;

	//timers
	private float timeStaying;

	//others

	public order wallFollowed;
	public bool obstacleLeft;
	public bool obstacleRight;
	public bool obstacleTop;
	public bool obstacleBot;

	
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
		myBehaviour = behaviour.wander;
		myOrder = order.stay;
		timeStaying = 0;
		map = GameObject.Find ("map");
		myAnimator.SetInteger ("select", 0);
		int rand = Random.Range (0, 4);
		switch (rand) {
		case 0 :
			myOrder=order.left;
			break;
		case 1 :
			myOrder=order.right;
			break;
		case 2 :
			myOrder=order.up;
			break;
		case 3 :
			myOrder=order.down;
			break;
		}
	}

	void Update(){
		//computeAI ();
		applyOrder ();

		//timers
		timeStaying += Time.deltaTime;
	}

	//debug
	


	public void computeAI()//change order and behaviour depending on lots of things
	{
		if (myBehaviour==behaviour.wander){

		}
	
	} 

	public void changeDirectionWander(){
		//print ("change");
		bool trapped = obstacleBot && obstacleTop && obstacleLeft && obstacleRight;
		if (trapped)
			myOrder = order.stay;
		else
		{
			order testdirection=order.stay;
			while (isObstacle(testdirection)){
				int randomNumber = Random.Range (0, 4);
				switch(randomNumber){
				case 0 :
					testdirection=order.down;
					break;
				case 1 :
					testdirection=order.up;
					break;
				case 2 :
					testdirection=order.left;
					break;
				case 3 :
					testdirection=order.right;
					break;
				}
			}

			myOrder=testdirection;
			wallFollowed=findWallFollowed();
			//print ("new dir : " + toString(testdirection)+ " , new wall : " + toString(wallFollowed));

		}
	}

	order findWallFollowed(){
		if (!obstacleBot && !obstacleTop && !obstacleLeft && !obstacleRight) //don't follow a wall : keep not following a wall
			return order.stay;

		switch (myOrder) {
		case order.down:
			if (obstacleRight){
				if (obstacleLeft) {
					if (Random.Range (0, 1) == 0)
						return order.right;
					else
						return order.left;
					}
					return order.right;
				}
			else
				return order.left;
			break;
		case order.up :
			if (obstacleRight){
				if (obstacleLeft) {
					if (Random.Range (0, 1) == 0)
						return order.right;
					else
						return order.left;
				}
				return order.right;
			}
			else
				return order.left;
			break;

		case order.right :
			if (obstacleTop){
				if (obstacleBot) {
					if (Random.Range (0, 1) == 0)
						return order.up;
					else
						return order.down;
				}
				return order.up;
			}
			else
				return order.down;
			break;

		case order.left :
			if (obstacleTop){
				if (obstacleBot) {
					if (Random.Range (0, 1) == 0)
						return order.up;
					else
						return order.down;
				}
				return order.up;
			}
			else
				return order.down;
			break;
			break;

		default :
			return order.stay;
			break;
		}

	}

	
	public void applyOrder()//move sprite depending on order, play animation according to state, etc
	{
		switch (myOrder) {
		case order.right :
			transform.Translate (Vector2.right * walkSpeed * Time.deltaTime);
			myAnimator.SetInteger("select",1);
			break;
		case order.left :
			transform.Translate (-Vector2.right * walkSpeed * Time.deltaTime);
			myAnimator.SetInteger("select",3);
			break;
		case order.up :
			transform.Translate (Vector2.up * walkSpeed * Time.deltaTime);
			myAnimator.SetInteger("select",2);
			break;
		case order.down :
			transform.Translate (-Vector2.up * walkSpeed * Time.deltaTime);
			myAnimator.SetInteger("select",0);
			break;
		default :
			break;
		}
		myAnimator.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-2 * transform.position.y);
	}


	void leftstay()
	{
		obstacleLeft = true;
		if (myOrder == order.left)
			changeDirectionWander ();
	}
	void rightstay()
	{
		obstacleRight = true;
		if (myOrder == order.right)
			changeDirectionWander ();
	}
	void topstay()
	{
		obstacleTop = true;
		if (myOrder == order.up)
			changeDirectionWander ();
	}
	void bottomstay()
	{
		obstacleBot = true;
		if (myOrder == order.down)
			changeDirectionWander ();
	}
	void leftexit()
	{

		if (!obstacleLeft && wallFollowed == order.left)
			changeDirectionWander ();
		obstacleLeft = false;
	}
	void rightexit()
	{
		obstacleRight = false;
		if (!obstacleRight && wallFollowed == order.right)
			changeDirectionWander ();
	}
	void topexit()
	{
		obstacleTop = false;
		if (!obstacleTop && wallFollowed == order.up)
			changeDirectionWander ();
	}
	void bottomexit()
	{
		obstacleBot = false;
		if (!obstacleBot && wallFollowed == order.down)
			changeDirectionWander ();
	}

	bool isObstacle (order thisorder){
		switch(thisorder){
		case order.down :
			return obstacleBot;
		case order.left :
			return obstacleLeft;
		case order.right :
			return obstacleRight;
		case order.up :
			return obstacleTop;
		default :
			return true;
		}
	}

}