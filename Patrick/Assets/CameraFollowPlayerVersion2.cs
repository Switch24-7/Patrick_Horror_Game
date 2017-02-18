using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerVersion2 : MonoBehaviour {
	Transform player;
	[HideInInspector]
	public string Location = "exit";
	Camera camera;
	public float smoothTime = .3f;
	float yVelocity = 5f;
	float xVelocity = 5f;
	void Start () {
		player = GameObject.Find ("Player").GetComponent<Transform> ();
		camera = GetComponent<Camera> ();
	}

	void FixedUpdate()
	{
		float yPosition = Mathf.SmoothDamp (transform.position.y, player.position.y, ref yVelocity, smoothTime);
		float xPosition = Mathf.SmoothDamp (transform.position.x, player.position.x,ref xVelocity, smoothTime);
		Vector3 newPos = new Vector3 (xPosition, yPosition, this.transform.position.z);
		transform.position = newPos;
	}
	void Update () {
		/*
		if (Location == "exit") {
			this.transform.position = new Vector3 (1.74f, 19.23f, this.transform.position.z);
		} else if (Location == "firstFloor") {
			this.transform.position = new Vector3 (Mathf.Clamp(player.position.x,1.35f,8.29f), Mathf.Clamp (player.position.y, 10f, 13.924f), this.transform.position.z);
		} else if (Location == "livingRoom") {
			this.transform.position = new Vector3 (Mathf.Clamp (player.position.x, 8.286f, 8.526f), Mathf.Clamp (player.position.y, 19.23f, 20.046f), this.transform.position.z);
		} else if (Location == "diningRoom") {
			this.transform.position = new Vector3 (Mathf.Clamp(player.position.x,1.35f,8.29f), Mathf.Clamp (player.position.y, 12.74f, 13.88f), this.transform.position.z);
		} else if (Location == "diningRoomHallway") {
			this.transform.position = new Vector3 (5.38f,12.76f, this.transform.position.z);
		} else if (Location == "kitchen") {
			this.transform.position = new Vector3 (8.92f, 6.77f, this.transform.position.z);
		} else if (Location == "basement") {
			this.transform.position = new Vector3 (Mathf.Clamp (player.position.x, -1.82f, 2.4f), Mathf.Clamp (player.position.y, -1.88f, 2.05f), this.transform.position.z);
		} else if (Location == "storage") {
			this.transform.position = new Vector3 (Mathf.Clamp (player.position.x, -19.11f, -14.29f), Mathf.Clamp (player.position.y, -1.88f, 2.05f), this.transform.position.z);
		} else if (Location == "secondFloor") {
			this.transform.position = new Vector3 (-4.84f, Mathf.Clamp (player.position.y, 11.67f, 13.91f), this.transform.position.z);
		} else if (Location == "hallway") {
			this.transform.position = new Vector3 (-12.84f, 10.16f, this.transform.position.z);
		} else if (Location == "bedRoom") {
			this.transform.position = new Vector3 (-12.84f,Mathf.Clamp(player.position.y,16.18f,16.85f),this.transform.position.z);
		}
		*/

		//this.transform.position = Vector3.SmoothDamp(this.transform.position,new Vector3(player.position.x,player.position.y,this.transform.position.z),

		//this.transform.position = new Vector3 (player.position.x, player.position.y, this.transform.position.z);


	}
	void LateUpdate()
	{
		//this.transform.position = Vector3.Lerp (this.transform.position, new Vector3(RoundToNearestPixel(player.position.x, camera),RoundToNearestPixel(player.position.y,camera), this.transform.position.z), Time.smoothDeltaTime*5f);

		//Vector3 roundPos = new Vector3(RoundToNearestPixel(player.position.x, camera),RoundToNearestPixel(player.position.y, camera),this.transform.position.z);
		//transform.position = roundPos; 


	}
	public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
	{
		float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
		valueInPixels = Mathf.Round(valueInPixels);
		float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
		return adjustedUnityUnits;
	}
}

