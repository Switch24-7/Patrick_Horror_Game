using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level1KeyScoop : MonoBehaviour {
	bool inRange = false;
	PlayerMovement player;
	DialogueManager dialogue;
	Text myText;
	bool activated = true;

	public AudioClip dialogueSound;
	public string[] theText;
	int index = 0;

	private bool isTyping = false;
	private bool cancelTyping;
	public float typeSpeed;
	[HideInInspector]
	public bool scoopFound = false;
	bool onDialogue = false;
	void Start()
	{
		dialogue = GameObject.Find ("Dialogue").GetComponent<DialogueManager> ();
		myText = GameObject.Find ("Speech").GetComponent<Text> ();
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
	}

	void Update()
	{
		if (!activated) 
		{
			Level1 script = GameObject.Find ("Game Tracker").GetComponent<Level1> ();
			script.keyFound = true;
		}
		else if (inRange && Input.GetKeyDown (KeyCode.Return) && onDialogue == true) 
		{
			if (index < theText.Length - 1 && !isTyping) {
				index++;
				StartCoroutine (TextScroll (theText [index]));
			}
			else if (index == theText.Length - 1 && !isTyping) {
				dialogue.DialogueOff ();
				activated = false;
				index = 0;
				onDialogue = false;
			} else if (isTyping && !cancelTyping) {
				cancelTyping = true;
			} 
		}
		else if (inRange && Input.GetKeyDown (KeyCode.Return) && !onDialogue) 
		{
			onDialogue = true;
			dialogue.DialogueOn ();
			StartCoroutine (TextScroll (theText [index]));
		}
	}
			

	private IEnumerator TextScroll (string lineOfText)
	{
		int letter = 0;
		myText.text = "";
		isTyping = true;
		cancelTyping = false;
		while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1)) 
		{
			AudioSource.PlayClipAtPoint (dialogueSound, player.transform.position);
			myText.text += lineOfText [letter];
			letter += 1;
			yield return new WaitForSeconds (typeSpeed);
		}
		myText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
			inRange = true;	
	}
	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Player")
			inRange = false;
	}
}
