using UnityEngine;
using System.Collections;




public class regionCreator : MonoBehaviour {

	public int width;
	public int height;

	public bool isSpawn;
	public bool isCamp;
	//public string filename;

	public GameObject rock;
	public GameObject bush;
	public GameObject tree;
	public GameObject battery;
	public GameObject rabbit;
	public GameObject player;
	public GameObject camp;
	public GameObject pit;

	public int[,] matrix;

	// Use this for initialization
	void Start () {

		matrix = new int[width,height];
		fill(matrix);
		create (matrix);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void create (int [,] matrix){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++){
				int code = matrix[i,j];
				switch (code){
				case 0: //grass : do nothing
					break;	
				case 1: //rock
					GameObject thisRock = (GameObject)Object.Instantiate(rock, new Vector3(j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					thisRock.GetComponentInChildren<SpriteRenderer>().sortingOrder =(int)( - 2*(i+transform.position.y));
					break;
				case 2: //bush
					GameObject thisBush = (GameObject)Object.Instantiate(bush, new Vector3(j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					thisBush.GetComponentInChildren<SpriteRenderer>().sortingOrder =(int)( - 2*(i+transform.position.y));
					break;
				case 3 : //tree
					GameObject thisTree = (GameObject)Object.Instantiate(tree, new Vector3(j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					thisTree.GetComponentInChildren<SpriteRenderer>().sortingOrder =(int)( - 2*(i+transform.position.y));
					break;
				case 5 : //battery
					GameObject thisBattery = (GameObject)Object.Instantiate(battery, new Vector3(j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					thisBattery.GetComponentInChildren<SpriteRenderer>().sortingOrder =(int)( - 2*(i+transform.position.y));
					break;
				case 6 : //rabbit
					GameObject thisRabbit = (GameObject)Object.Instantiate(rabbit, new Vector3(j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					break;
				case 7 : //spawn
					GameObject thisPlayer = (GameObject)Object.Instantiate(player, new Vector3(j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					thisPlayer.GetComponentInChildren<SpriteRenderer>().sortingOrder =(int)( - 2*(i+transform.position.y));
					break;
				case 8 : // camp
					GameObject thisCamp = (GameObject)Object.Instantiate(camp, new Vector3(j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					thisCamp.GetComponentInChildren<SpriteRenderer>().sortingOrder =(int)( - 2*(i+transform.position.y));
					break;
				case 9 : // pit
					GameObject thisPit = (GameObject)Object.Instantiate(pit, new Vector3(j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					thisPit.GetComponentInChildren<SpriteRenderer>().sortingOrder =(int)( - 2*(i+transform.position.y));
					break;
				default :
					break;
				}
			}
		}
	}

	void fill (int [,] matrix){ //basic fonction choosing a
		int middlex = width / 2;
		int middley = height / 2;
		int randomNumber = Random.Range (2, 5);

		switch (randomNumber) {
		case 0 :
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++){
					matrix[i,j]=0;
				}
			}
			matrix [2, 2] = 1;
			matrix [3, 4] = 1;
			break;
		case 1 :
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++){
					matrix[i,j]=0;
				}
			}
			matrix [2, 2] = 1;
			matrix [2, 3] = 1;
			matrix [2, 4] = 1;
			matrix [2, 5] = 1;
			break;
		case 2 :
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++){
					matrix[i,j]=0;
				}
			}
			for (int i = 0; i < width; i++) {
				matrix[i,0]=1;
				matrix[i,height-1]=1;
			}
			for (int j = 0; j < height; j++) {
				matrix[0,j]=1;
				matrix[width-1,j]=1;
			}
			matrix [4,4] = 6; //rabbit !

			matrix [6,6] = 5; //battery !
			break;

		case 3 :
			 
			int[,] table3 = new int[,] { 	
				{ 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				{ 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
				{ 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1 }, 
				{ 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1 }, 
				{ 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1 }, 
				{ 0, 0, 0, 1, 0, 3, 4, 3, 4, 3, 4, 0, 1, 0, 0, 1, 0, 0, 0 }, 
				{ 0, 0, 0, 1, 0, 4, 4, 4, 4, 4, 4, 0, 1, 0, 0, 1, 0, 6, 0 }, 
				{ 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0 }, 
				{ 1, 0, 0, 1, 0, 9, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 1 }, 
				{ 1, 0, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 1 }, 
				{ 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1 }, 
				{ 1, 0, 0, 0, 0, 1, 0, 1, 0, 9, 0, 1, 1, 0, 0, 0, 5, 0, 1 }, 
				{ 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 }
								};
			
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++){
					matrix[i,j]=table3[i,j];
				}
			}

			break;

		case 4 :
			int[,] table4 = new int[,] { 	
				{ 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				{ 0, 0, 0, 2, 2, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, 
				{ 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 5, 0, 0, 0, 0, 1 }, 
				{ 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 }, 
				{ 1, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, 3, 4, 0, 3, 0, 0, 0, 0 }, 
				{ 0, 0, 0, 0, 0, 3, 4, 3, 4, 3, 4, 4, 4, 0, 0, 1, 0, 0, 0 }, 
				{ 0, 0, 0, 1, 0, 4, 4, 4, 4, 4, 4, 3, 4, 0, 0, 1, 0, 0, 0 }, 
				{ 0, 0, 6, 2, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 6, 0, 0, 0 }, 
				{ 1, 0, 0, 1, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0 }, 
				{ 1, 0, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 0, 2, 1, 3, 4, 0, 0 }, 
				{ 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 4, 4, 0, 1 }, 
				{ 2, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 1 }, 
				{ 1, 2, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 }
			};
			
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++){
					matrix[i,j]=table4[i,j];
				}
			}
			break;
		}

		//making doors
		matrix [0, middley] = 0;
		matrix [width-1, middley] = 0;
		matrix [middlex, 0] = 0;
		matrix [middlex, height-1] = 0;

		//spawn and camp
		if (isSpawn) {
			matrix [3,3] = 7;
		}
		if (isCamp) {
			matrix [3, 3] = 8;
		}
		//making batteries ! Everywhere !
		//matrix [2, 2] = 5;

	}
	

}


