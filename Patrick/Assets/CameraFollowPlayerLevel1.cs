using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerLevel1 : MonoBehaviour {
	Transform player;
	[HideInInspector]
	public string Location = "exit";

	void Start () {
		player = GameObject.Find ("Player").GetComponent<Transform> ();
	}
	

	void Update () {
		if (Location == "exit") {
			this.transform.position = new Vector3 (.3f, 23.924f, this.transform.position.z);
		} else if (Location == "firstFloor") {
			this.transform.position = new Vector3 (.39f, Mathf.Clamp (player.position.y, 10f, 14.05f), this.transform.position.z);
		} else if (Location == "livingRoom") {
			this.transform.position = new Vector3 (Mathf.Clamp (player.position.x, 16.51f, 17.09f), Mathf.Clamp (player.position.y,23.75f , 24.09f), this.transform.position.z);
		} else if (Location == "diningRoom") {
			this.transform.position = new Vector3 (16.81f, Mathf.Clamp (player.position.y, 11.28f, 12.76f), this.transform.position.z);
		} else if (Location == "kitchen") {
			this.transform.position = new Vector3 (17.04f, .2f, this.transform.position.z);
		} else if (Location == "basement") {
			this.transform.position = new Vector3 (Mathf.Clamp (player.position.x, -1.82f, 2.4f), Mathf.Clamp (player.position.y, -1.88f, 2.05f), this.transform.position.z);
		} else if (Location == "storage") {
			this.transform.position = new Vector3 (Mathf.Clamp (player.position.x, -19.11f, -14.29f), Mathf.Clamp (player.position.y, -1.88f, 2.05f), this.transform.position.z);
		} else if (Location == "secondFloor") {
			this.transform.position = new Vector3 (-16.66f, Mathf.Clamp (player.position.y, 10.94f, 13.45f), this.transform.position.z);
		} else if (Location == "hallway") {
			this.transform.position = new Vector3 (-38.07f, 11.92f, this.transform.position.z);
		} else if (Location == "bedRoom") {
			this.transform.position = new Vector3 (-37.9f,Mathf.Clamp(player.position.y,24.55f,25.33f),this.transform.position.z);
		}
	}
}
