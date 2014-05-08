using UnityEngine;
using System.Collections;




public class mapInterpretator : MonoBehaviour {

	public int width;
	public int height;
	public string filename;
	public GameObject grass;
	public GameObject rock;
	public GameObject bush;
	private int[,] matrix;

	// Use this for initialization
	void Start () {

		matrix = new int[width,height];

		fill(matrix);

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++){
				int code = matrix[i,j];
				switch (code){
				case 0: //grass
					Object.Instantiate(grass, new Vector3 (i,j,0), Quaternion.identity);
					break;	
				case 1: //rock
					Object.Instantiate(rock, new Vector3(i,j,0), Quaternion.identity);
					break;
				case 2: //bush
					Object.Instantiate(bush, new Vector3(i,j,0), Quaternion.identity);
					break;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void fill (int [,] matrix){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++){
				matrix[i,j]=0;
			}
		}
		matrix [2, 2] = 1;
		matrix [3, 4] = 2;
	}

}


