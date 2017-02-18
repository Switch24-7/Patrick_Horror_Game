using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public bool DontMove = false;
	public float speed; //how fast the character is
	public string Location;
	Animator anim; 

	bool notMoving;
	float changeInY;
	float originalY;
	float originalX;
	float changeInX;

	[HideInInspector]
	public bool dialogueOn;
	[HideInInspector]
	public string fromWhichDoor;

	void FixedUpdate()
	{
		if (!DontMove)
		Movement ();
	}
	void Update()
	{
		if (!DontMove) {
			anim.SetBool ("notMoving", notMoving);
			anim.SetFloat ("speed", changeInX);
			anim.SetFloat ("speedy", changeInY);
		}
	}
	void Movement()
	{
		changeInY = 0;
		changeInX = 0;

		anim = this.gameObject.GetComponent<Animator> ();
		Vector2 currentPosition = this.transform.position;

		if (Input.GetKey (KeyCode.UpArrow)) { //Move the character up
			changeInY = 0;
			originalY = currentPosition.y;
			currentPosition.y += speed;
			changeInY = originalY - currentPosition.y;

		} else if (Input.GetKey (KeyCode.DownArrow)) {// Move the character down
			changeInY = 0;
			originalY = currentPosition.y;
			currentPosition.y -= speed;
			changeInY = originalY - currentPosition.y;

		} else if (Input.GetKey (KeyCode.LeftArrow)) {// Move the character left
			changeInX = 0;
			originalX = currentPosition.x;
			currentPosition.x -= speed;
			changeInX = originalX - currentPosition.x;

		} else if (Input.GetKey (KeyCode.RightArrow)) {// Move the character right
			changeInX = 0;
			originalX = currentPosition.x;
			currentPosition.x += speed;
			changeInX = originalX - currentPosition.x;

		}

		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Vertical");
		if (moveX == 0 && moveY == 0) {
			notMoving = true;
		}
		else if (moveX != 0 || moveY != 0) {
			notMoving = false;
		} 


		if (dialogueOn)
			return;
		this.transform.position = currentPosition;

	}
}
