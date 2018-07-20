using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

	public float upForce = 2000f;
	private bool isDead = false;
	private Rigidbody2D rb2d;
	private Animator anim;
	private bool onWaterfall = false;
	// private bool canJump = true;
	private int jumpCounter = 0;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (isDead == false) {
			rb2d.velocity = new Vector2(0, rb2d.velocity.y);
			if (GameController.instance.GetInput() && jumpCounter < 2) {
				// Testing no collision on going up 
				rb2d.velocity = new Vector2(0, 0);
				rb2d.AddForce(new Vector2(0, upForce));
				anim.SetTrigger("Flap");
				incrementJumpCounter();				
			}
			if (rb2d.velocity.y > 0) {
				rb2d.gameObject.layer = 9;
			} else {
				rb2d.gameObject.layer = 8;
			}
			Debug.Log("Layer: " + rb2d.gameObject.layer, rb2d.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag == "Slide") {
			isDead = false;
			resetJumpCounter();
		} else {
			rb2d.velocity = Vector2.zero;
			isDead = true;
			anim.SetTrigger("Die");
			GameController.instance.BirdDied();
		}
	}

	// public void setCanJump(bool val) {
	// 	canJump = val;
	// }

	void incrementJumpCounter() {
		jumpCounter++;
	}

	void resetJumpCounter() {
		jumpCounter = 0;
	}

}
