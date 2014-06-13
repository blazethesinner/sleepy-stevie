using UnityEngine;
using System.Collections;

public class RabbitBehaviour :  MonoBehaviour {

	public enum animalType {rabbit, bear};
	public animalType myType;

	//characteristics
	public int maxHealth;
	public int walkSpeed;
	public int pushSpeed;
	public int speed;
	public int strength;
	private int health;

	//enums
	public enum state {light, dark};
	public enum behaviour {noMove, chasingTarget, wander, pushed};
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

	public string directionPushed;
	public float pushDuration;
	private float timerPushed;
	
	private AudioClip clip_smack;
	private AudioSource smackAudio;

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
		smackAudio = (AudioSource)gameObject.AddComponent ("AudioSource");
		clip_smack = (AudioClip)Resources.Load ("sfx/smack");
		smackAudio.clip = clip_smack;
	}

	void Update(){
		computeAI ();

		applyOrder ();

		//timers
		timeStaying += Time.deltaTime;
	}

	//debug
	


	public void computeAI()//rule the pushed state for now, the wander state is ruled separately
	{
		if (myBehaviour==behaviour.wander){
			//nothing
		}

		if (myBehaviour == behaviour.pushed) {
			timerPushed += Time.deltaTime;
			speed= pushSpeed;
			if (timerPushed > pushDuration)
				myBehaviour=behaviour.wander;
		}
		else {
			speed=walkSpeed;
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
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			if (myBehaviour==behaviour.wander)
				myAnimator.SetInteger("select",1);
			break;
		case order.left :
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
			if (myBehaviour==behaviour.wander)
				myAnimator.SetInteger("select",3);
			break;
		case order.up :
			transform.Translate (Vector2.up * speed * Time.deltaTime);
			if (myBehaviour==behaviour.wander)
				myAnimator.SetInteger("select",2);
			break;
		case order.down :
			transform.Translate (-Vector2.up * speed * Time.deltaTime);
			if (myBehaviour==behaviour.wander)
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
		if (myOrder == order.left&& myBehaviour==behaviour.wander)
			changeDirectionWander ();
		if (myOrder == order.left && myBehaviour==behaviour.pushed)
			myBehaviour=behaviour.wander;
	}
	void rightstay()
	{
		obstacleRight = true;

		if (myOrder == order.right && myBehaviour==behaviour.wander)
			changeDirectionWander ();
		if (myOrder == order.right && myBehaviour==behaviour.pushed)
			myBehaviour=behaviour.wander;
	}
	void topstay()
	{
		obstacleTop = true;
		if (myOrder == order.up && myBehaviour==behaviour.wander)
			changeDirectionWander ();
		if (myOrder == order.up && myBehaviour==behaviour.pushed)
			myBehaviour=behaviour.wander;
	}
	void bottomstay()
	{
		obstacleBot = true;
		if (myOrder == order.down && myBehaviour==behaviour.wander)
			changeDirectionWander ();
		if (myOrder == order.down && myBehaviour==behaviour.pushed)
			myBehaviour=behaviour.wander;
	}
	void leftexit()
	{

		if (!obstacleLeft && wallFollowed == order.left && myBehaviour==behaviour.wander)
			changeDirectionWander ();
		obstacleLeft = false;
	}
	void rightexit()
	{
		obstacleRight = false;
		if (!obstacleRight && wallFollowed == order.right && myBehaviour==behaviour.wander)
			changeDirectionWander ();
	}
	void topexit()
	{
		obstacleTop = false;
		if (!obstacleTop && wallFollowed == order.up && myBehaviour==behaviour.wander)
			changeDirectionWander ();
	}
	void bottomexit()
	{
		obstacleBot = false;
		if (!obstacleBot && wallFollowed == order.down && myBehaviour==behaviour.wander)
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

	public void getHit(string direction){
		smackAudio.Play();
		print (gameObject.name + " smashed");
		myBehaviour = behaviour.pushed;
		timerPushed = 0f;
		switch (direction){
		case "up" :
			myOrder = order.up;
			break;
		case "down" :
			myOrder=order.down;
			break;
		case "right" :
			myOrder=order.right;
			break;
		case "left" :
			myOrder = order.left;
			break;
		default :
			myOrder = order.stay;
			break;
		}

	}

}