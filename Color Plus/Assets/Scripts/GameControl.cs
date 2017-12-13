using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code was written in a group effort with Granger where we helped each other but wrote our code seperately.

public class GameControl : MonoBehaviour {
	//float GameLength = 60;
	float TurnLength = 2;
	public GameObject cubePrefab;
	Vector3 cubePosition; 
	public GameObject [,] grid;
	Color [] NextCubeArray; 
	GameObject nextCube;
	int gridY = 5;
	int gridX = 8;
	int turn = 0; 
	//int activeCube;
	Vector3 nextCubeStart = new Vector3 (7, 10, 0); 
	int activeNum = -1; 
	int playerScore = 0;
	bool win = true;
	bool activeCube = false;



	GameObject PickWhite (List<GameObject> whiteCubes) {

		//with no white cubes give an error
		if (whiteCubes.Count == 0) {
			return null;
		}
		//Pick a random white cube
		return whiteCubes[Random.Range(0, whiteCubes.Count)];

	}

	//White cube list for placement of nextCube
	GameObject LocateAvailableCube (int y) {
		List<GameObject> whiteCubes = new List<GameObject> ();

		for (int x = 0; x < gridX; x++) {
			if (grid[x, y].GetComponent<Renderer>().material.color == Color.white) {
				whiteCubes.Add(grid [x, y]);

			}
		}
		return PickWhite (whiteCubes);
	}

	//white cube list for placement of blackCube
	GameObject LocateAvailableCube () {
		List<GameObject> whiteCubes = new List<GameObject> ();

		for (int y = 0; y < gridY; y++) {
			for (int x = 0; x < gridX; x++) {
				if (grid [x, y].GetComponent<Renderer> ().material.color == Color.white) {
					whiteCubes.Add (grid [x, y]);
				}
			}
		}

		return PickWhite (whiteCubes);

	}




	void NextCubePlacement (int y) {
		GameObject cube = LocateAvailableCube (y);

		if (cube == null) {
			GameEnd (false); 
		} 
		else {
			//Grid x and y to place the NextCube
			cube.GetComponent<Renderer> ().material.color = nextCube.GetComponent<Renderer> ().material.color;
			Destroy (nextCube);
			nextCube = null;
			activeNum = -1;
		} 
	}

	public void ProcessKeyboard () {
		//Each number from 1 through 5 inputs nextCube into one of the rows on the grid while there is still a NextCube
		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
			activeNum = 4; 
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
			activeNum = 3; 
		}
		if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) {
			activeNum = 2; 
		}
		if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) {
			activeNum = 1; 
		}
		if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) {
			activeNum = 0; 
		}

		//Make sure the line is not full (end if it is)
		if (nextCube != null && activeNum != -1) {
			//put in specified row
			NextCubePlacement (activeNum);
		} 
	}
		
//		void PlusScoring (int x, int y){
			
	void AddBlackCube () {
		GameObject cube = LocateAvailableCube ();

		if (cube == null) {
			GameEnd (false); 
		} 
		else {
			//Assign black color to chosen cube
			cube.GetComponent<Renderer> ().material.color = Color.black;
		} 
		//find random whtie cube and turn black

		//if unable end game
		print ("Black cube created");
	}


	// Use this for initialization
	void Start () {

		NextCubeArray = new Color[5];
		NextCubeArray [0] = Color.blue; 
		NextCubeArray [1] = Color.green;
		NextCubeArray [2] = Color.red;
		NextCubeArray [3] = Color.yellow;
		NextCubeArray [4] = Color.magenta;

	}
		
	void GridCreate () {
		//create a 8 by 5 grid of cubes
		grid = new GameObject[gridX, gridY];
		for (int y = 0; y < gridY; y++) {
			for (int x = 0; x < gridX; x++) {
				cubePosition = new Vector3 (x * 2, y * 2, 0);
				grid [x, y] = Instantiate (cubePrefab, cubePosition, Quaternion.identity);
				}
			}
		}

	
	// Update is called once per frame
	void Update () {

		GridCreate (); 
		ProcessKeyboard ();
		ProcessClick (); 


		//Bool: Check if timer has run out
		if (Time.time < TurnLength) {
			//activate GameEnd
		}
		//keep track of what happens in a turn
		if (Time.time > TurnLength * turn) { 
			turn++;
			print ("This is a turn! ... " + turn); 
			//If no cube distribution, penalize player with black cube
			if (nextCube != null) {
				playerScore -= -1; 
				Destroy (nextCube);
				AddBlackCube ();
			}

			//Create the new colored cube
			if (nextCube == null) {
			nextCube = Instantiate (cubePrefab, nextCubeStart, Quaternion.identity);
			}

			nextCube.GetComponent<Renderer>().material.color = NextCubeArray[Random.Range (0, NextCubeArray.Length)];
		}
			//Was a cube clicked and moved during the turn?
			//Was PlusScoring fulfilled?
			//If turn is over, start a new turn
	}
		
	public void ProcessClick (GameObject clickedCube, int x, int y, bool activeCube) {
		print ("you have clicked" +x+ " , " +y);
		//was a colored cube clicked
		if (clickedCube.GetComponent<Renderer> ().material.color != Color.white && 
			clickedCube.GetComponent<Renderer> ().material.color != Color.black) {
			//if active cube
			if (activeCube) {
				//deactive
				clickedCube.transform.localScale /= 1f;
				activeCube = false;
			} 
			//if no active cube
			else {
				//activate
				clickedCube.transform.localScale *= 1.2f;
				activeCube = true;
			}


		}

	}
	void GameEnd (bool win){
		//Bool: does player meet win requirements?
		if (win) {
			print ("Ya a winner");
		}
		if (false) {
			//print ("ya lost plz play again");
			}
	}
}