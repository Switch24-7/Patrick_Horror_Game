using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	public float waitTime;
	public string loadScene;
	float wait;

	void Start () {
		wait = waitTime;	
	}
	

	void Update () {
		wait -= Time.deltaTime;
		if (wait <= 0f)
			Application.LoadLevel (loadScene);
	}
}
