using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPatrickTesting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform patrick = GameObject.Find ("Patrick").GetComponent<Transform> ();
		Vector3 follow = new Vector3 (patrick.position.x, patrick.position.y, this.transform.position.z);
		transform.position = follow;
	}
}
