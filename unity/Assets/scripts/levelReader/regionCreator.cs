using UnityEngine;
using System.Collections;




public class regionCreator : MonoBehaviour {

	public int width;
	public int height;

	public fileReader reader;

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
	public GameObject bear;
	public int[,] matrix;

	// Use this for initialization
	void Start () {

		reader = GameObject.Find ("map").GetComponent<fileReader> ();
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
				case 4 : //bear
					GameObject thisBear = (GameObject)Object.Instantiate(bear, new Vector3(j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					break;
				default :
					break;
				}
			}
		}
	}

	void fill (int [,] matrix){ //basic fonction choosing a

		int rnd = Random.Range (0, reader.numberOfLevels);
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++){
				matrix[i,j]=reader.matrixArray[rnd,i,j];
			}
		}

		//spawn and camp
		if (isSpawn) {
			matrix [6,9] = 7;
		}
		if (isCamp) {
			matrix [6,9] = 8;
			matrix[7,10]=0;
			matrix[6,10]=0;
			matrix[7,9]=0;
		}


	}
	

}


