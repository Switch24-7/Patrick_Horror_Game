using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScareCountdown : MonoBehaviour {
	public GameObject Patrick;
	public GameObject ScareSound;
	bool playOnce = false;
	bool waitAgain = false;

	float waitTime;
	float waitTimeAgain;

	public string sceneName;

	void Start () {
		Patrick.SetActive (false);
		waitTime = 2f;
		playOnce = false;
	}
	

	void Update () {
		waitTime -= Time.deltaTime;
		waitTimeAgain -= Time.deltaTime;
		if (waitTime <= 0f && !playOnce) {
			playOnce = true;
			waitAgain = true;
			waitTimeAgain = 1.5f;
			Patrick.SetActive (true);
			ScareSound.SetActive (true);
		} else if (waitTimeAgain <= 0f && waitAgain) {
			Application.LoadLevel (sceneName);
		}
	}
}
