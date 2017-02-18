using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScriptVersion2 : MonoBehaviour {

	public string newLocation;

	public string fromWhichDoor;

	PlayerMovement player;
	void Start()
	{
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
	}
		
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") 
		{
			CameraFollowPlayerVersion2 camera = GameObject.Find ("Main Camera").GetComponent<CameraFollowPlayerVersion2> ();
			camera.Location = newLocation;
			player.Location = newLocation;
		}
	}
}

