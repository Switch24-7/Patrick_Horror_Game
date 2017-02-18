using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrick : MonoBehaviour {
	Transform target;//player's Transform
	public float roamSpeed = .02f;
	//Waypoints ----------------------------------------------------------------
	public Transform[] bedRoom;
	public Transform[] bedRoomReturn;

	public Transform[] hallwayToSecondFloor;
	public Transform[] hallwayToBedRoom;

	public Transform[] secondFloorToFirstFloor;
	public Transform[] secondFloorToHallway;

	public Transform[] firstFloorToExit;
	public Transform[] firstFloorToDiningRoom;
	public Transform[] firstFloorToBasement;
	public Transform[] firstFloorFromDiningRoomToBasement;
	public Transform[] firstFloorFromDiningRoomToExit;
	public Transform[] firstFloorFromDiningRoomToSecondFloor;
	public Transform[] firstFloorFromExitToSecondFloor;
	public Transform[] firstFloorFromExitToDiningRoom;
	public Transform[] firstFloorFromExitToBasement;
	public Transform[] firstFloorFromBasementToExit;
	public Transform[] firstFloorFromBasementToDiningRoom;
	public Transform[] firstFloorFromBasementToSecondFloor;


	public Transform[] diningRoomFromFirstFloorToKitchen;
	public Transform[] diningRoomFromFirstFloorToLivingRoom;
	public Transform[] diningRoomFromKitchenToLivingRoom;
	public Transform[] diningRoomFromKitchenToFirstFloor;
	public Transform[] diningRoomFromLivingRoomToKitchen;
	public Transform[] diningRoomFromLivingRoomToFirstFloor;

	public Transform[] kitchenToGarden;
	public Transform[] kitchenToDiningRoom;

	public Transform[] garden;

	public Transform[] livingRoomToExit;
	public Transform[] livingRoomToDiningRoom;

	public Transform[] exitToLivingRoom;
	public Transform[] exitToFirstFloor;

	public Transform[] basementToStorage;
	public Transform[] basementToFirstFloor;

	public Transform[] storage;
	// ----------------------------------------------------------------
	int current;
	int chance;
	Animator anim;
	[HideInInspector]
	public bool chanceReset;
	float waitTime;
	public float changeRoomCD = 1f;
	bool changeRooms = true;
	int chaseChance;
	bool chaseChanceReset = true;

	public GameObject chaseArea;//chase range. when player moves out of range patrick will stop chasing.

	[HideInInspector]
	public bool playerDetect; //detection range. when player is in range, patrick will begin chasing.
	[HideInInspector]
	public string generalLocation;
	[HideInInspector]
	public string Location; //Patrick's location. Which room he's in

	//for Animation
	float changeInX;
	float changeInY;
	float originalX;
	float originalY;

	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		chanceReset = true;
		generalLocation = "bedRoom";
		Location = "bedRoom";
		this.transform.position = bedRoom [0].position;
	}

	void FixedUpdate()
	{
		
		//While patrick is not chasing, follow waypoints
		if (playerDetect == false) { 
			Roaming ();
		}

		//when player is in range, start chasing player
		if (playerDetect == true) 
		{
			Chase();
		}
	}

	void Update()
	{
		waitTime -= Time.deltaTime;
		changeInX = originalX - this.transform.position.x;
		changeInY = originalY - this.transform.position.y;
		anim.SetFloat ("speed", changeInX);
		anim.SetFloat ("speedy",changeInY);
	}
		
	void Chase()
	{
		PlayerMovement player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		originalX = this.transform.position.x;
		originalY = this.transform.position.y;
		if (generalLocation == player.Location) {
			//this entire function ensures that patrick only moves one vector at a time.
			target = GameObject.Find ("Player").GetComponent<Transform> ();
			Vector2 myPosition = this.transform.position;
			float yDifference = Mathf.Abs (this.transform.position.y - target.position.y);
			float xDifference = Mathf.Abs (this.transform.position.x - target.position.x);
			if (yDifference > xDifference) {
				if (myPosition.y > target.position.y)
					myPosition.y -= 2f * Time.deltaTime;
				else if (myPosition.y < target.position.y)
					myPosition.y += 2f * Time.deltaTime;
			} else if (xDifference > yDifference) {
				if (myPosition.x > target.position.x)
					myPosition.x -= 2f * Time.deltaTime;
				else if (myPosition.x < target.position.x)
					myPosition.x += 2f * Time.deltaTime;
			}
			this.transform.position = myPosition;
		} 

		else if (generalLocation != player.Location) {
			current = 0;
			if (chaseChanceReset) {
				chaseChanceReset = false;
				chaseChance = Random.Range (1, 5);
			}
			else if (chaseChance == 1) {
				playerDetect = false;
				chaseChanceReset = true;
				return;
			}
			else if (chaseChance == 2 || chaseChance == 3|| chaseChance == 4)
			{
			if (player.fromWhichDoor == "firstFloor")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
					chaseChanceReset = true;
					generalLocation = "firstFloor";
					Location = "firstFloor";
					this.transform.position = firstFloorToExit [0].position;
				}
			}
			else if (player.fromWhichDoor == "livingRoomToDiningRoom")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "livingRoom";
					Location = "livingRoomToDiningRoom";
					this.transform.position = livingRoomToDiningRoom [0].position;
				}
			}
			else if (player.fromWhichDoor == "livingRoomToExit")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "livingRoom";
					Location = "livingRoomToExit";
					this.transform.position = livingRoomToExit [0].position;
				}
			}
			else if (player.fromWhichDoor == "firstFloorFromExit")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "firstFloor";
					Location = "firstFloorFromExit";
					this.transform.position = firstFloorFromExitToBasement [0].position;
				}
			}
			else if (player.fromWhichDoor == "firstFloorFromDiningRoom")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "firstFloor";
					Location = "firstFloorFromDiningRoom";
					this.transform.position = firstFloorFromDiningRoomToExit [0].position;
				}
			}
			else if (player.fromWhichDoor == "firstFloorFromBasement")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "firstFloor";
					Location = "firstFloorFromBasement";
					this.transform.position = firstFloorFromBasementToExit [0].position;
				}
			}
			else if (player.fromWhichDoor == "basementToStorage")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "basement";
					Location = "basementToStorage";
					this.transform.position = basementToStorage [0].position;
				}
			}
			else if (player.fromWhichDoor == "basementToFirstFloor")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "basement";
					Location = "basementToFirstFloor";
					this.transform.position = basementToFirstFloor [0].position;
				}
			}
			else if (player.fromWhichDoor == "storage")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "storage";
					Location = "storage";
					this.transform.position = storage [0].position;
				}
			}
			else if (player.fromWhichDoor == "diningRoomFromFirstFloor")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "diningRoom";
					Location = "diningRoomFromFirstFloor";
					this.transform.position = diningRoomFromFirstFloorToKitchen [0].position;
				}
			}
			else if (player.fromWhichDoor == "diningRoomFromKitchen")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "diningRoom";
					Location = "diningRoomFromKitchen";
					this.transform.position = diningRoomFromKitchenToFirstFloor [0].position;
				}
			}
			else if (player.fromWhichDoor == "diningRoomFromLivingRoom")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "diningRoom";
					Location = "diningRoomFromLivingRoom";
					this.transform.position = diningRoomFromLivingRoomToKitchen [0].position;
				}
			}
			else if (player.fromWhichDoor == "garden")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "garden";
					Location = "garden";
					this.transform.position = garden [0].position;
				}
			}
			else if (player.fromWhichDoor == "secondFloor")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "secondFloor";
					Location = "secondFloor";
					this.transform.position = secondFloorToFirstFloor [0].position;
				}
			}
			else if (player.fromWhichDoor == "secondFloorToHallway")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "secondFloor";
					Location = "secondFloorToHallway";
					this.transform.position = secondFloorToHallway [0].position;
				}
			}
			else if (player.fromWhichDoor == "hallwayToBedroom")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "hallway";
					Location = "hallwayToBedRoom";
					this.transform.position = hallwayToBedRoom [0].position;
				}
			}else if (player.fromWhichDoor == "hallway")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "hallway";
					Location = "hallway";
					this.transform.position = hallwayToSecondFloor [0].position;
				}
			}
			else if (player.fromWhichDoor == "bedRoomReturn")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "bedRoom";
					Location = "bedRoomReturn";
					this.transform.position = bedRoomReturn [0].position;
				}
			}
			else if (player.fromWhichDoor == "exitToLivingRoom")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "exit";
					Location = "exitToLivingRoom";
					this.transform.position = exitToLivingRoom [0].position;
				}
			}
			else if (player.fromWhichDoor == "exitToFirstFloor")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "exit";
					Location = "exitToFirstFloor";
					this.transform.position = exitToFirstFloor [0].position;
				}
			}
			else if (player.fromWhichDoor == "kitchenToGarden")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "kitchen";
					Location = "kitchenToGarden";
					this.transform.position = kitchenToGarden [0].position;
				}
			}
			else if (player.fromWhichDoor == "kitchenToDiningRoom")
			{
				if (changeRooms) {
					waitTime = changeRoomCD;
					changeRooms = false;
				} else if (waitTime <= 0f) {
					changeRooms = true;
						chaseChanceReset = true;
					generalLocation = "kitchen";
					Location = "kitchenToDiningRoom";
					this.transform.position = kitchenToDiningRoom [0].position;
				}
			}
		}
	}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") {
			playerDetect = true;
			chaseArea.SetActive (true);
		}
	}

	void Roaming()
	{
		originalX = this.transform.position.x;
		originalY = this.transform.position.y;
		if (Location == "bedRoom")
			BedRoom ();
		else if (Location == "hallway")
			Hallway ();
		else if (Location == "secondFloor")
			SecondFloor ();
		else if (Location == "secondFloorToHallway")
			SecondFloorToHallway ();
		else if (Location == "firstFloor")
			FirstFloor ();
		else if (Location == "firstFloorFromBasement")
			FirstFloorFromBasement ();
		else if (Location == "firstFloorFromDiningRoom")
			FirstFloorFromDiningRoom ();
		else if (Location == "firstFloorFromExit")
			FirstFloorFromExit ();
		else if (Location == "exitToLivingRoom")
			ExitRoomToLivingRoom ();
		else if (Location == "exitToFirstFloor")
			ExitRoomToFirstFloor ();
		else if (Location == "hallwayToBedRoom")
			HallwayToBedroom ();
		else if (Location == "bedRoomReturn")
			BedroomReturn ();
		else if (Location == "diningRoomFromFirstFloor")
			DiningRoomFromFirstFloor ();
		else if (Location == "diningRoomFromKitchen")
			DiningRoomFromKitchen ();
		else if (Location == "diningRoomFromLivingRoom")
			DiningRoomFromLivingRoom ();
		else if (Location == "livingRoomToDiningRoom")
			LivingRoomToDiningRoom ();
		else if (Location == "livingRoomToExit")
			LivingRoomToExit ();
		else if (Location == "kitchenToGarden")
			KitchenToGarden ();
		else if (Location == "kitchenToDiningRoom")
			KitchenToDiningRoom ();
		else if (Location == "basementToStorage")
			BasementToStorage ();
		else if (Location == "basementToFirstFloor")
			BasementToFirstFloor ();
		else if (Location == "storage")
			Storage ();
		else if (Location == "garden")
			Garden ();


	}
	//All location functions under here--------------------------------------------------------------------
	void BedRoom()
	{
		if (this.transform.position != bedRoom [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, bedRoom [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++; //When waypoint is reached, move on to next point
		if (current == bedRoom.Length) { //When all waypoints reached, reset and move to next location
			current = 0;
			generalLocation = "hallway";
			Location = "hallway";
			this.transform.position = hallwayToSecondFloor [0].position;
		}
	}

	void Hallway()
	{
		if (this.transform.position != hallwayToSecondFloor [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, hallwayToSecondFloor [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == hallwayToSecondFloor.Length) {
			current = 0;
			generalLocation = "secondFloor";
			Location = "secondFloor";
			this.transform.position = secondFloorToFirstFloor [0].position;
		}
	}

	void SecondFloor()
	{
		if (this.transform.position != secondFloorToFirstFloor [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, secondFloorToFirstFloor [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == secondFloorToFirstFloor.Length) {
			current = 0;
			generalLocation = "firstFloor";
			Location = "firstFloor";
			this.transform.position = firstFloorToExit [0].position;
		}
	}

	void FirstFloor()
	{
		if (chanceReset) {
			chance = Random.Range (1, 4);
			chanceReset = false;
		}
		if (chance == 1) {
			if (this.transform.position != firstFloorToExit [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorToExit [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorToExit.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "exit";
				Location = "exitToLivingRoom";
				this.transform.position = exitToLivingRoom [0].position;
			}
		} else if (chance == 2) {
			if (this.transform.position != firstFloorToDiningRoom [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorToDiningRoom [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorToDiningRoom.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "diningRoom";
				Location = "diningRoomFromFirstFloor";
				this.transform.position = diningRoomFromFirstFloorToKitchen [0].position;
			}
		} else if (chance == 3) {
			if (this.transform.position != firstFloorToBasement [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorToBasement [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorToBasement.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "basement";
				Location = "basementToStorage";
				this.transform.position = basementToStorage [0].position;
			}
		}
	}

	void ExitRoomToLivingRoom()
	{
		if (this.transform.position != exitToLivingRoom [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, exitToLivingRoom [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == exitToLivingRoom.Length) {
			chanceReset = true;
			current = 0;
			generalLocation = "livingRoom";
			Location = "livingRoomToDiningRoom";
			this.transform.position = livingRoomToDiningRoom [0].position;
		}
	}

	void ExitRoomToFirstFloor()
	{
		if (this.transform.position != exitToFirstFloor [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, exitToFirstFloor [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == exitToFirstFloor.Length) {
			chanceReset = true;
			current = 0;
			generalLocation = "firstFloor";
			Location = "firstFloorFromExit";
			this.transform.position = firstFloorFromExitToBasement [0].position;
		}
	}

	void FirstFloorFromExit()
	{
		if (chanceReset) {
			chance = Random.Range (1, 4);
			chanceReset = false;
		}
		if (chance == 1) {
			if (this.transform.position != firstFloorFromExitToSecondFloor [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorFromExitToSecondFloor [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorFromExitToSecondFloor.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "secondFloor";
				Location = "secondFloorToHallway";
				this.transform.position = secondFloorToHallway [0].position;
			}
		} else if (chance == 2) {
			if (this.transform.position != firstFloorFromExitToDiningRoom [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorFromExitToDiningRoom [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorFromExitToDiningRoom.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "diningRoom";
				Location = "diningRoomFromFirstFloor";
				this.transform.position = secondFloorToHallway [0].position;
			}
		} else if (chance == 3) {
			if (this.transform.position != firstFloorFromExitToBasement [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorFromExitToBasement [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorFromExitToBasement.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "basement";
				Location = "basementFromFirstFloor";
				this.transform.position = secondFloorToHallway [0].position;
			}
		}
	}
	void SecondFloorToHallway()
	{
		if (this.transform.position != secondFloorToHallway [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, secondFloorToHallway [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == secondFloorToHallway.Length) {
			current = 0;
			generalLocation = "hallway";
			Location = "hallwayToBedRoom";
			this.transform.position = hallwayToBedRoom [0].position;
		}
	}
	void HallwayToBedroom()
	{
		if (this.transform.position != hallwayToBedRoom [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, hallwayToBedRoom [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == hallwayToBedRoom.Length) {
			current = 0;
			generalLocation = "bedRoom";
			Location = "bedRoomReturn";
			this.transform.position = bedRoomReturn [0].position;
		}
	}
	void BedroomReturn()
	{
		if (this.transform.position != bedRoomReturn [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, bedRoomReturn [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == bedRoomReturn.Length) {
			current = 0;
			generalLocation = "hallway";
			Location = "hallway";
			this.transform.position = hallwayToSecondFloor [0].position;
		}
	}
	void DiningRoomFromFirstFloor()
	{
		if (chanceReset) {
			chance = Random.Range (1, 3);
			chanceReset = false;
		}
		if (chance == 1) {
			if (this.transform.position != diningRoomFromFirstFloorToKitchen [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, diningRoomFromFirstFloorToKitchen [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == diningRoomFromFirstFloorToKitchen.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "kitchen";
				Location = "kitchenToGarden";
				this.transform.position = kitchenToGarden [0].position;
			}
		} else if (chance == 2) {
			if (this.transform.position != diningRoomFromFirstFloorToLivingRoom [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, diningRoomFromFirstFloorToLivingRoom [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == diningRoomFromFirstFloorToLivingRoom.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "livingRoom";
				Location = "livingRoomToExit";
				this.transform.position = livingRoomToExit [0].position;
			}
		}
	}
	void DiningRoomFromKitchen()
	{
		if (chanceReset) {
			chance = Random.Range (1, 3);
			chanceReset = false;
		}
		if (chance == 1) {
			if (this.transform.position != diningRoomFromKitchenToFirstFloor [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, diningRoomFromKitchenToFirstFloor [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == diningRoomFromKitchenToFirstFloor.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "firstFloor";
				Location = "firstFloorFromDiningRoom";
				this.transform.position = firstFloorFromDiningRoomToExit [0].position;
			}
		}
		else if (chance ==2)
		{
			if (this.transform.position != diningRoomFromKitchenToLivingRoom [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, diningRoomFromKitchenToLivingRoom [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == diningRoomFromKitchenToLivingRoom.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "livingRoom";
				Location = "livingRoomToExit";
				this.transform.position = livingRoomToExit [0].position;
			}
		}
	}
	void FirstFloorFromBasement()
	{
		if (chanceReset) {
			chance = Random.Range (1, 4);
			chanceReset = false;
		}
		if (chance == 1) {
			if (this.transform.position != firstFloorFromBasementToDiningRoom [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorFromBasementToDiningRoom [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorFromBasementToDiningRoom.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "diningRoom";
				Location = "diningRoomFromFirstFloor";
				this.transform.position = diningRoomFromFirstFloorToKitchen [0].position;
			}
		} else if (chance == 2) {
			if (this.transform.position != firstFloorFromBasementToExit [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorFromBasementToExit [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorFromBasementToExit.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "exit";
				Location = "exitToLivingRoom";
				this.transform.position = exitToLivingRoom [0].position;
			}
		} else if (chance == 3) {
			if (this.transform.position != firstFloorFromBasementToSecondFloor [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorFromBasementToSecondFloor [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorFromBasementToSecondFloor.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "secondFloor";
				Location = "secondFloorToHallway";
				this.transform.position = secondFloorToHallway [0].position;
			}
		}
	}
	void FirstFloorFromDiningRoom()
	{
		if (chanceReset) {
			chance = Random.Range (1, 4);
			chanceReset = false;
		}
		if (chance == 1) {
			if (this.transform.position != firstFloorFromDiningRoomToBasement [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorFromDiningRoomToBasement [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorFromDiningRoomToBasement.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "basement";
				Location = "basementToStorage";
				this.transform.position = basementToStorage [0].position;
			}
		} else if (chance == 2) {
			if (this.transform.position != firstFloorFromDiningRoomToExit [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorFromDiningRoomToExit [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorFromDiningRoomToExit.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "exit";
				Location = "exitToLivingRoom";
				this.transform.position = exitToLivingRoom [0].position;
			}
		} else if (chance == 3) 
		{
			if (this.transform.position != firstFloorFromDiningRoomToSecondFloor [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, firstFloorFromDiningRoomToSecondFloor [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == firstFloorFromDiningRoomToSecondFloor.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "secondFloor";
				Location = "secondFloorToHallway";
				this.transform.position = secondFloorToHallway [0].position;
			}
		}
	}
	void Storage()
	{
		if (this.transform.position != storage [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, storage [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == storage.Length) {
			chanceReset = true;
			current = 0;
			generalLocation = "basement";
			Location = "basementToFirstFloor";
			this.transform.position = basementToFirstFloor [0].position;
		}
	}
	void BasementToStorage()
	{
		if (this.transform.position != basementToStorage [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, basementToStorage [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == basementToStorage.Length) {
			chanceReset = true;
			current = 0;
			generalLocation = "storage";
			Location = "storage";
			this.transform.position = storage[0].position;
		}
	}
	void BasementToFirstFloor()
	{
		if (this.transform.position != basementToFirstFloor [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, basementToFirstFloor [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == basementToFirstFloor.Length) {
			chanceReset = true;
			current = 0;
			generalLocation = "firstFloor";
			Location = "firstFloorFromBasement";
			this.transform.position = firstFloorFromBasementToExit[0].position;
		}
	}
	void DiningRoomFromLivingRoom()
	{
		if (chanceReset) {
			chance = Random.Range (1, 3);
			chanceReset = false;
		}
		if (chance == 1) {
			if (this.transform.position != diningRoomFromLivingRoomToFirstFloor [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, diningRoomFromLivingRoomToFirstFloor [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == diningRoomFromLivingRoomToFirstFloor.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "firstFloor";
				Location = "firstFloorFromDiningRoom";
				this.transform.position = firstFloorFromDiningRoomToExit [0].position;
			}
		}
		else if (chance == 2) {
			if (this.transform.position != diningRoomFromLivingRoomToKitchen [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, diningRoomFromLivingRoomToKitchen [current].position, roamSpeed);
				GetComponent<Rigidbody2D> ().MovePosition (move);
			} else
				current++;
			if (current == diningRoomFromLivingRoomToKitchen.Length) {
				chanceReset = true;
				current = 0;
				generalLocation = "kitchen";
				Location = "kitchenToGarden";
				this.transform.position = kitchenToGarden [0].position;
			}
		}
	}
	void LivingRoomToDiningRoom()
	{
		if (this.transform.position != livingRoomToDiningRoom [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, livingRoomToDiningRoom [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == livingRoomToDiningRoom.Length) {
			chanceReset = true;
			current = 0;
			generalLocation = "diningRoom";
			Location = "diningRoomFromLivingRoom";
			this.transform.position = diningRoomFromLivingRoomToKitchen [0].position;
		}
	}
	void LivingRoomToExit()
	{
		if (this.transform.position != livingRoomToExit [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, livingRoomToExit [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == livingRoomToExit.Length) {
			chanceReset = true;
			current = 0;
			generalLocation = "exit";
			Location = "exitToFirstFloor";
			this.transform.position = exitToFirstFloor [0].position;
		}
	}
	void KitchenToGarden()
	{
		if (this.transform.position != kitchenToGarden [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, kitchenToGarden [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == kitchenToGarden.Length) {
			current = 0;
			chanceReset = true;
			generalLocation = "garden";
			Location = "garden";
			this.transform.position = garden [0].position;
		}
	}
	void KitchenToDiningRoom()
	{
		if (this.transform.position != kitchenToDiningRoom [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, kitchenToDiningRoom [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == kitchenToDiningRoom.Length) {
			chanceReset = true;
			current = 0;
			generalLocation = "diningRoom";
			Location = "diningRoomFromKitchen";
			this.transform.position = diningRoomFromKitchenToFirstFloor [0].position;
		}
	}
	void Garden()
	{
		if (this.transform.position != garden [current].position) {
			Vector2 move = Vector2.MoveTowards (this.transform.position, garden [current].position, roamSpeed);
			GetComponent<Rigidbody2D> ().MovePosition (move);
		} else
			current++;
		if (current == garden.Length) {
			chanceReset = true;
			current = 0;
			generalLocation = "kitchen";
			Location = "kitchenToDiningRoom";
			this.transform.position = kitchenToDiningRoom [0].position;
		}
	}
}
