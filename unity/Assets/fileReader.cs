using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class fileReader : MonoBehaviour {
	public FileInfo sourceFile;
	public TextAsset textFile;
	public int numberOfLevels;
	public int width;
	public int height;
	public int[,,] matrixArray;

	void OnEnable () {
		//FileInfo theSourceFile = new FileInfo ("Test.txt");
		//StreamReader reader = sourceFile.OpenText();
		
		string completeText = textFile.text;

		MemoryStream stream = new MemoryStream();
		StreamWriter writer = new StreamWriter(stream);
		writer.Write(completeText);
		writer.Flush();
		stream.Position = 0;
		StreamReader reader = new StreamReader (stream);
		

		matrixArray = new int[numberOfLevels, height, width];

		int currentMatrix = 0;
		int currentLine = 0;
		bool newMatrixReady = true;
		string text;
		do
		{
			text = reader.ReadLine();

			if (text!=""){


				if (currentMatrix<numberOfLevels){
					for (int i=0; i <width; i ++){
						matrixArray[currentMatrix,12-currentLine,i] = int.Parse(text[i].ToString());
					}
					//print (text);
					currentLine++;
				}
				if (currentLine>=height){
					currentLine=0;
					currentMatrix++;
					//print ("new matrix : " + currentMatrix);
				}
			}
		}while (currentMatrix < numberOfLevels);
	}
	

	void Update () {
	
	}
}
