using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {

	PlayerMovement player;

	public GameObject keyScoop;
	public GameObject bedroomLocked;
	public GameObject bedroomUnlocking;
	public GameObject bedroomUnlocked;
	public GameObject roomDialogue;
	public GameObject hallwayDialogue;

	public GameObject Painting;
	public GameObject redChair1;
	public GameObject redChair2;
	public GameObject redChair3;
	public GameObject plant;

	public GameObject Crack;
	public GameObject RedChair1;
	public GameObject RedChair2;
	public GameObject RedChair3;
	public GameObject Plant;
	public GameObject bloodTrail;
	public GameObject thrillerMusic;
	public GameObject bloodTrail2;
	public GameObject RedDoor;
	public GameObject closedRedDoor;

	[HideInInspector]
	public bool keyFound = false;
	[HideInInspector]
	public bool bedroomDoorUnlocked = false;
	bool bedroomDialogue = false;
	bool bedroomHallwayDialogue = false;

	bool bloodyRoom = false;

	void Start()
	{
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		keyScoop.SetActive (false);
		bedroomLocked.SetActive (true);
		bedroomUnlocking.SetActive (false);
		bedroomUnlocked.SetActive (false);
		roomDialogue.SetActive (false);
		hallwayDialogue.SetActive (false);

		Painting.SetActive (true);
		Crack.SetActive (false);
		thrillerMusic.SetActive (false);
	}

	void Update () {
		string playerLocation = player.Location;
		if (keyFound) 
		{
			keyScoop.SetActive (false);
			bedroomLocked.SetActive (false);
			bedroomUnlocking.SetActive (true);
		}
		if (bedroomDoorUnlocked) 
		{
			bedroomUnlocking.SetActive (false);
			bedroomUnlocked.SetActive (true);
			bedroomDialogue = true;
			bedroomDoorUnlocked = false;
		}
		if (bedroomDialogue && playerLocation == "bedRoom") 
		{
			roomDialogue.SetActive (true);
			bedroomDialogue = false;
			bedroomHallwayDialogue = true;
		}
		if (bedroomHallwayDialogue && playerLocation == "hallway")
		{
			bedroomHallwayDialogue = false;
			hallwayDialogue.SetActive (true);
			Painting.SetActive (false);
			Crack.SetActive (true);
			bloodyRoom = true;
			thrillerMusic.SetActive (true);
		}
		if (bloodyRoom && playerLocation == "firstFloor") 
		{
			bloodyRoom = false;
			redChair1.SetActive (false);
			redChair2.SetActive (false);
			redChair3.SetActive (false);
			plant.SetActive (false);

			RedChair1.SetActive (true);
			RedChair2.SetActive (true);
			RedChair3.SetActive (true);
			Plant.SetActive (true);
			bloodTrail.SetActive (true);
			bloodTrail2.SetActive (true);
			RedDoor.SetActive (true);
			closedRedDoor.SetActive (false);
		}

	}
}
