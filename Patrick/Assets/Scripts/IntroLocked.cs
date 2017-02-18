using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroLocked : MonoBehaviour {

	bool inRange = false;
	PlayerMovement player;
	DialogueManager dialogue;
	Text myText;
	bool Activated = true;

	public AudioClip dialogueSound;
	public string[] theText;
	int index = 0;

	public GameObject Player;
	public GameObject Fred;
	public GameObject PatrickFineUI;
	public GameObject PatrickUI;
	public GameObject newDialogue;
	public bool PlayerUI;
	public bool FredUI;
	public bool PatrickFine;
	public bool Patrick;
	public bool activateNewDialogue;

	private bool isTyping = false;
	private bool cancelTyping;
	public float typeSpeed;

	bool onDialogue = false;
	void Start()
	{
		dialogue = GameObject.Find ("Dialogue").GetComponent<DialogueManager> ();
		myText = GameObject.Find ("Speech").GetComponent<Text> ();
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();

	}

	void Update()
	{
		if (!Activated) 
		{
			Intro script = GameObject.Find ("IntroManager").GetComponent<Intro> ();
			script.doorOpen = true;
		}
		else if (inRange && Input.GetKeyDown (KeyCode.Return) && onDialogue == true) 
		{
			if (index < theText.Length - 1 && !isTyping) {
				index++;
				StartCoroutine (TextScroll (theText [index]));
			}
			else if (index == theText.Length - 1 && !isTyping) {
				dialogue.DialogueOff ();
				Activated = false;
				index = 0;
				onDialogue = false;
				if (PlayerUI)
					Player.SetActive (false);
				if (FredUI)
					Fred.SetActive (false);
				if (PatrickFine)
					PatrickFineUI.SetActive (false);
				if (Patrick)
					PatrickUI.SetActive (false);
				if (activateNewDialogue) 
					newDialogue.SetActive (true);
			} else if (isTyping && !cancelTyping) {
				cancelTyping = true;
			} 
		}
		else if (inRange && Input.GetKeyDown (KeyCode.Return) && !onDialogue) 
		{
			onDialogue = true;
			dialogue.DialogueOn ();
			if (PlayerUI)
				Player.SetActive (true);
			if (FredUI)
				Fred.SetActive (true);
			if (PatrickFine)
				PatrickFineUI.SetActive (true);
			if (Patrick)
				PatrickUI.SetActive (true);
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
			AudioSource.PlayClipAtPoint (dialogueSound, GameObject.Find("Main Camera").GetComponent<Transform>().position);
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
