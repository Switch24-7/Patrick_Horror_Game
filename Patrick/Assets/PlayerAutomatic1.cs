using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerAutomatic1 : MonoBehaviour {
	public Transform[] Waypoints;
	Text myText;
	DialogueManager dialogue;
	CameraFollowPlayerLevel1Chase camera;
	int current;
	public string sceneName;
	Animator anim;
	bool pause = false;
	bool pause2 = true;
	public float CD;

	float changeInX;
	float originalX;
	float newX;
	float changeInY;
	float originalY;
	float newY;

	void Start()
	{
		camera = GameObject.Find ("Main Camera").GetComponent<CameraFollowPlayerLevel1Chase> ();
		anim = GetComponent<Animator> ();
		CD = 1.8f;
	}
	void FixedUpdate()
	{
		if (!pause) {
			originalX = this.transform.position.x;
			originalY = this.transform.position.y;
			if (current == 2) {
				this.transform.position = Waypoints [2].position;
				camera.Location = "secondFloor";
			} else if (current == 4) {
				this.transform.position = Waypoints [4].position;
				camera.Location = "firstFloor";
			} else if (current == 9) {
				this.transform.position = Waypoints [9].position;
				camera.Location = "exit";
			} else if (current == 16) {
				this.transform.position = Waypoints [16].position;
				camera.Location = "firstFloor";
			} else if (current == 20) {
				this.transform.position = Waypoints [20].position;
				camera.Location = "basement";
			} else if (current == 13) {
				if (pause2) {
					pause2 = false;
					pause = true;
					myText = GameObject.Find ("Speech").GetComponent<Text> ();
					dialogue = GameObject.Find ("Dialogue").GetComponent<DialogueManager> ();
					dialogue.DialogueOn ();
					myText.text = "FUUU***** THE DOOR IS LOCKED!!!";
				}
			}
			if (this.transform.position != Waypoints [current].position) {
				Vector2 move = Vector2.MoveTowards (this.transform.position, Waypoints [current].position, .075f);
				GetComponent<Rigidbody2D> ().MovePosition (move);

			} else if (current == Waypoints.Length - 1) {
				Application.LoadLevel (sceneName);
			} else
				current++;
		}
	}
	void Update () {
		changeInX = originalX - this.transform.position.x;
		changeInY = originalY - this.transform.position.y;
		anim.SetFloat ("speed", changeInX);
		anim.SetFloat ("speedy",changeInY);

		if (!pause2) 
		{
			CD -= Time.deltaTime;
		}
		if (CD <= 0f) 
		{
			pause = false;
			myText = GameObject.Find ("Speech").GetComponent<Text> ();
			dialogue = GameObject.Find ("Dialogue").GetComponent<DialogueManager> ();
			dialogue.DialogueOn ();
			myText.text = "SH** SH** SH** SH** GOTTA HIDE!!!!";
		}
	}
}
