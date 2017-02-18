using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

	public GameObject doorLocked;
	public GameObject doorUnlocked;

	[HideInInspector]
	public bool doorOpen = false;

	void Start()
	{
		doorLocked.SetActive (true);
		doorUnlocked.SetActive (false);
	}

	void Update () {
		if (doorOpen) 
		{
			doorLocked.SetActive (false);
			doorUnlocked.SetActive (true);
		}
	}
}
