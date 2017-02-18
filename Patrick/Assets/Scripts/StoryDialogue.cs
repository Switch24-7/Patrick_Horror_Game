using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryDialogue : MonoBehaviour {
	PlayerMovement player;
	DialogueManager dialogue;
	Text myText;
	bool Activated = true;

	public AudioClip dialogueSound;
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
	public string[] theText;
	int index;

	private bool isTyping = false;
	private bool cancelTyping;
	public float typeSpeed;
	public bool isPlayerHere = true;
	public bool loadScene = false;
	public string sceneName;

	void Start()
	{
		index = 0;
		dialogue = GameObject.Find ("Dialogue").GetComponent<DialogueManager> ();
		myText = GameObject.Find ("Speech").GetComponent<Text> ();
		if (isPlayerHere)
			player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		dialogue.DialogueOn ();
		if (PlayerUI)
			Player.SetActive (true);
		if (FredUI)
			Fred.SetActive (true);
		if (PatrickFine)
			PatrickFineUI.SetActive (true);
		if (Patrick)
			PatrickUI.SetActive (true);
		StartCoroutine (TextScroll(theText [index]));
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Return) && Activated == true) 
		{
			if(!isTyping)
			index++;
			if (index > theText.Length - 1 && !isTyping) {
				dialogue.DialogueOff ();
				Activated = false;
				index = 0;
				if (PlayerUI)
					Player.SetActive (false);
				if (FredUI)
					Fred.SetActive (false);
				if (PatrickFine)
					PatrickFineUI.SetActive (false);
				if (Patrick)
					PatrickUI.SetActive (false);
				if (activateNewDialogue) {
					newDialogue.SetActive (true);
				}
				if (loadScene) 
				{
					Application.LoadLevel (sceneName);
				}
			} 
			else if (isTyping && !cancelTyping) 
			{
				cancelTyping = true;
			}
			else 
				StartCoroutine (TextScroll(theText [index]));
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
		
}
