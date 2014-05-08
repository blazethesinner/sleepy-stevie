using UnityEngine;
using System.Collections;

public class mapCreator : MonoBehaviour {

	public int width;
	public int height;
	public int regionHeight;
	public int regionWidth;
	public GameObject region;
	public GameObject blockingRegion;

	private int [,] matrix;


	// Use this for initialization
	void Start () {
		matrix = new int[width, height];
		fill (matrix);
		createAreas (matrix);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void fill(int [,] matrix){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++){
				matrix [0, 0] = 1; // start
				matrix[0,1]=1; // empty
				matrix[0,2]=1; // camp
			}
		}
	}

	void createAreas (int[,] matrix){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++){
				int tmp = matrix[i,j];
				switch (tmp){
				case 1 :
					Instantiate(region, new Vector3 (i*regionWidth,j*regionHeight,0), Quaternion.identity);
					break;
				case 2 :

					break;
				case 3 :

					break;
				}
			}
		}
	}
}


