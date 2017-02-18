using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager: MonoBehaviour {
	PlayerMovement player;
	public GameObject panel;
	public bool isPlayerHere = true;
	Text speech;
	[HideInInspector]
	public bool activated;

	public void DialogueOn()
	{
		activated = true;
		if (isPlayerHere) {
			player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
			player.dialogueOn = true;
		}
		panel.SetActive (true);
	}

	public void DialogueOff()
	{
		speech = GameObject.Find ("Speech").GetComponent<Text> ();
		speech.text = "";
		if (isPlayerHere) {
			player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
			player.dialogueOn = false;
		}
		panel.SetActive (false);
	}
		
}
