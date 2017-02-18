using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseArea : MonoBehaviour {
	public GameObject patrick;

	void Update () {
		if (patrick.GetComponent<Patrick> ().playerDetect == false)
			gameObject.SetActive (false);
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Player") 
		{
			patrick.GetComponent<Patrick> ().playerDetect = false;
			gameObject.SetActive (false);
		}
	}
}
