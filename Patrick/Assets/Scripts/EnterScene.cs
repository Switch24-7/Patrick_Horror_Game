using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour {

	bool Activated;

	public string sceneName;
	void Update () {
		if (Activated && Input.GetKeyDown (KeyCode.Return))
			Application.LoadLevel (sceneName);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
			Activated = true;
	}
	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Player")
			Activated = false;
	}
}
