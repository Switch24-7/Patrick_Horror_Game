using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
	
	public float newPositionX; //X position where player will be teleported
	public float newPositionY; //Y position where player will be teleported
	public float CameraPositionX; //X position where camera will be teleported
	public float CameraPositionY; //Y position where camera will be teleported
	public string newLocation;

	bool Activated;

	public bool level1 = false;
	public bool levelChase = false;
	public bool level2 = false;

	public string fromWhichDoor;

	PlayerMovement player;
	void Start()
	{
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
	}

	void Update()
	{
		if (Activated == true && Input.GetKeyDown (KeyCode.Return))
		{
			player.Location = newLocation;
			player.fromWhichDoor = fromWhichDoor;
			Transform playerTransform = GameObject.Find ("Player").GetComponent<Transform> ();
			Vector2 newPosition = playerTransform.position;
			newPosition.x = newPositionX;
			newPosition.y = newPositionY;
			playerTransform.position = newPosition;  //Teleports player to new location

			if (level1) {
				CameraFollowPlayerLevel1 camera = GameObject.Find ("Main Camera").GetComponent<CameraFollowPlayerLevel1> ();
				camera.Location = newLocation;
			} else if (levelChase) {
				CameraFollowPlayerLevel1Chase camera = GameObject.Find ("Main Camera").GetComponent<CameraFollowPlayerLevel1Chase> ();
				camera.Location = newLocation;
			}else if (level2) {
				CameraFollowPlayerLevel2 camera = GameObject.Find ("Main Camera").GetComponent<CameraFollowPlayerLevel2> ();
				camera.Location = newLocation;
			}
		//Vector3 newCameraPosition = cameraTransform.position;
		//newCameraPosition.x = CameraPositionX;
		//newCameraPosition.y = CameraPositionY;
		//newCameraPosition.z = cameraTransform.position.z;
		//cameraTransform.position = newCameraPosition; //Camera changes to new location
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") 
		{
			Activated = true;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Player") 
		{
			Activated = false;
		}
	}

}
