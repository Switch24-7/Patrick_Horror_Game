using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete2 : MonoBehaviour {
	bool touching;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		if (touching && Input.GetKeyDown(KeyCode.Space)) {
			delete3.bloodTrailGone = true;
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		
		touching = true;
	}
	void OnTriggerExit2D (Collider2D col)
	{
		
		touching = false;
	}
}
