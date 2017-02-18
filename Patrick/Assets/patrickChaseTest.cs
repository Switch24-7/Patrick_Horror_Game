using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrickChaseTest : MonoBehaviour {
	Animator anim;
	PlayerMovement player;
	Transform target;
	float originalX;
	float originalY;

	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
			Move ();
		

	}

	void Move()
	{
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		originalX = this.transform.position.x;
		originalY = this.transform.position.y;

		//this entire function ensures that patrick only moves one vector at a time.
		target = GameObject.Find ("Player").GetComponent<Transform> ();
		Vector2 myPosition = this.transform.position;
		float yDifference = Mathf.Abs (this.transform.position.y - target.position.y);
		float xDifference = Mathf.Abs (this.transform.position.x - target.position.x);
		if (yDifference > xDifference) {
			if (myPosition.y > target.position.y)
				myPosition.y -= .03f;
			else if (myPosition.y < target.position.y)
				myPosition.y += .03f;
		} else if (xDifference > yDifference) {
			if (myPosition.x > target.position.x)
				myPosition.x -= .03f;
			else if (myPosition.x < target.position.x)
				myPosition.x += .03f;
		}
		this.transform.position = myPosition;
		float changeInX = originalX - this.transform.position.x;
		float changeInY = originalY - this.transform.position.y;
		anim.SetFloat ("speed", changeInX);
		anim.SetFloat ("speedy",changeInY);
	}
}
