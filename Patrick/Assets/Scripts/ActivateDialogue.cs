using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActivateDialogue : MonoBehaviour {
	PlayerMovement player;
	DialogueManager dialogue;
	Text myText;
	bool Activated;
	bool onDialogue;
	public string[] theText;
	int index = 0;
	public bool loadScene;
	public string sceneName;

	private bool isTyping = false;
	private bool cancelTyping;
	public float typeSpeed;
	public AudioClip dialogueSound;
	public bool soundOn = true;

	void Start()
	{
		dialogue = GameObject.Find ("Dialogue").GetComponent<DialogueManager> ();
		myText = GameObject.Find ("Speech").GetComponent<Text> ();
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
	}

	void Update()
	{
		if (onDialogue == true && Input.GetKeyDown (KeyCode.Return)) 
		{
			if (index < theText.Length - 1 && !isTyping) {
				index++;
				StartCoroutine (TextScroll(theText [index]));
			}
			else if (index == theText.Length - 1 && !isTyping) 
			{
				if (loadScene == true) 
				{
					Application.LoadLevel (sceneName);
				}
				index = 0;
				onDialogue = false;
				dialogue.DialogueOff ();
			}
			else if (isTyping && !cancelTyping) 
			{
				cancelTyping = true;
			}
		}
		else if (Activated == true && Input.GetKeyDown (KeyCode.Return)) 
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
			if (soundOn)
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
		{
			Activated = true;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Player")
			Activated = false;
	}
}
