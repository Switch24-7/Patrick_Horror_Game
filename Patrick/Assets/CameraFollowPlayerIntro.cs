using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerIntro : MonoBehaviour {
	Transform player;
	float xMin = -2.67f;
	float xMax = 2.65f;
	float yMax =2f;
	float yMin = -2f;

	void Start () {
		player = GameObject.Find ("Player").GetComponent<Transform> ();
	}
	

	void Update () {
		this.transform.position = new Vector3 (Mathf.Clamp(player.position.x,xMin,xMax), Mathf.Clamp(player.position.y,yMin,yMax), this.transform.position.z);
	}
}
