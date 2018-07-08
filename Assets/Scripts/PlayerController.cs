using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

	public float upForce = 200f;

	public float maxVelocity = 10f;

	private bool isDead = false;
	private bool canJump = false;

	private Rigidbody2D rb2d;
	private Animator anim;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	bool GetInput() {
        return (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space));
	}

	bool Jump() {
		if (GetInput() && canJump) {
			canJump = false;
			return true;
		}
		return false;
	}

	// Update is called once per frame
	void Update () {
		// Cap x velocity
		if (Math.Abs(rb2d.velocity.x) > maxVelocity) {
			var newVel = maxVelocity;
			if (rb2d.velocity.x < 0) {
				newVel =  -maxVelocity;
			}
			rb2d.velocity = new Vector2(newVel, rb2d.velocity.y);
		}
        // if (Math.Abs(rb2d.velocity.y) > maxVelocity) {
        //     var newVel = maxVelocity;
        //     if (rb2d.velocity.y < 0) {
        //         newVel = -maxVelocity;
        //     }
        //     rb2d.velocity = new Vector2(rb2d.velocity.x, newVel);

        // }

		if (isDead == false) {
			if (Jump()) {
				rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
				rb2d.AddForce(new Vector2(0, upForce));
				anim.SetTrigger("Flap");
			}
		}
	}

	void OnCollisionEnter2D() {
		canJump = true;
		// rb2d.velocity = Vector2.zero;
		// isDead = true;
		// anim.SetTrigger("Die");
		// BirdDied();
	}
}
