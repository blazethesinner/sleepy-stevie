using UnityEngine;
using System.Collections;

public class mapCreator : MonoBehaviour {

	public int width;
	public int height;
	public int regionHeight;
	public int regionWidth;
	public GameObject region;
	public GameObject blockingRegion;

	public int [,] matrix;
	public GameObject [,] regionMatrix;

	// Use this for initialization
	void Start () {
		matrix = new int[width, height];
		regionMatrix = new GameObject[width, height];
		fill (matrix);
		complete (matrix);
		createAreas (matrix);
		GameObject.Find ("player").transform.position = new Vector3 (20, 20, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void fill(int [,] matrix){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++){
				matrix [1, 1] = 2; // start
				matrix[1,2]=1;
				matrix[2,2]=1;
				matrix[2,1]=1;
				matrix[3,1]=1;
				matrix[3,2]=1;
				matrix[3,3]=3; //camp
			}
		}
	}

	void complete(int[,] matrix){

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++){
				if(matrix[i,j]>0){
					if (i+1<width)
						if (matrix[i+1,j]==0)
							matrix[i+1,j]=-1;
					if (i+1<width && j+1 < height)
						if (matrix[i+1,j+1]==0)
							matrix[i+1,j+1]=-1;
					if (j+1 < height)
						if (matrix[i,j+1]==0)
							matrix[i,j+1]=-1;
					if (i-1>=0 && j+1 < height)
						if (matrix[i-1,j+1]==0)
							matrix[i-1,j+1]=-1;
					if (i-1>=0 )
						if (matrix[i-1,j]==0)
							matrix[i-1,j]=-1;
					if (i-1>=0 && j-1 >=0)
						if (matrix[i-1,j-1]==0)
							matrix[i-1,j-1]=-1;
					if (j-1 >= 0)
						if (matrix[i,j-1]==0)
							matrix[i,j-1]=-1;
					if (i+1 < width && j-1 >=0)
						if (matrix[i+1,j-1]==0)
							matrix[i+1,j-1]=-1;
				}
			}
		}

	}



	void createAreas (int[,] matrix){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++){
				int tmp = matrix[i,j];
				switch (tmp){
				case 1 :
					GameObject myRegion = (GameObject)Instantiate(region, new Vector3 (i*regionWidth, j*regionHeight,0), Quaternion.identity);
					regionMatrix[i,j] = myRegion;
					myRegion.GetComponent<regionCreator>().isSpawn=false;
					myRegion.GetComponent<regionCreator>().isCamp=false;
					break;
				case -1 :
					Instantiate(blockingRegion, new Vector3 (i*regionWidth,j*regionHeight,0), Quaternion.identity);
					break;
				case 2 :
					GameObject mySpawn = (GameObject)Instantiate(region, new Vector3 (i*regionWidth, j*regionHeight,0), Quaternion.identity);
					regionMatrix[i,j] = mySpawn;
					mySpawn.GetComponent<regionCreator>().isSpawn=true;
					mySpawn.GetComponent<regionCreator>().isCamp=false;
					break;
				case 3 :
					GameObject myCamp = (GameObject)Instantiate(region, new Vector3 (i*regionWidth, j*regionHeight,0), Quaternion.identity);
					regionMatrix[i,j] = myCamp;
					myCamp.GetComponent<regionCreator>().isSpawn=false;
					myCamp.GetComponent<regionCreator>().isCamp=true;

					break;

					break;
				}
			}
		}
	}
}


