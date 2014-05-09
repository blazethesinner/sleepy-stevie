using UnityEngine;
using System.Collections;




public class regionCreator : MonoBehaviour {

	public int width;
	public int height;


	//public string filename;
	public GameObject grass;
	public GameObject rock;
	public GameObject bush;
	private int[,] matrix;

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
				case 0: //grass
					//Object.Instantiate(grass, new Vector3 (j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					break;	
				case 1: //rock
					Object.Instantiate(rock, new Vector3(j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					break;
				case 2: //bush
					Object.Instantiate(bush, new Vector3(j+transform.position.x,i+transform.position.y,0), Quaternion.identity);
					break;
				}
			}
		}
	}

	void fill (int [,] matrix){ //basic fonction choosing a
		int middlex = width / 2;
		int middley = height / 2;
		int randomNumber = Random.Range (0, 4);

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
			break;
		case 3 :
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

			matrix[6,6]=2;
			matrix[7,7]=2;

			break;
		}

		//making doors
		matrix [0, middley] = 0;
		matrix [width-1, middley] = 0;
		matrix [middlex, 0] = 0;
		matrix [middlex, height-1] = 0;

	}

}


