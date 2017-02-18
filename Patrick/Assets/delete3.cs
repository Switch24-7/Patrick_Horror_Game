using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete3 : MonoBehaviour {
	public static bool bloodTrailGone;
	public GameObject blood;

	// Use this for initialization
	void Start () {
		if (!bloodTrailGone) {
			blood.SetActive (true);
		} else if (bloodTrailGone) {
			blood.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
