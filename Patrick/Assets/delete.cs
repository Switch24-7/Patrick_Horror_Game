using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class delete : MonoBehaviour {
	bool touching;
	public string level;
	void Start () {
		
	}
	

	void Update () {
		if (touching && Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel (level);
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
