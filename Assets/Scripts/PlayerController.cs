using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

	public float upForce = 200f;
	public float boostTimeout = 2;
	public float boostDuration = 0.25f;
	public float boostTimer = 0;

	private float maxVelocity = 6f;
	private float baseVelocity = 6f;
	private float boostVelocity = 20f;
	private float speedUpVelocity = 12f;
	private float minVelocity = 3f;

	private bool isDead = false;
	private bool canJump = false;
	private bool onWaterfall = false;

	private Rigidbody2D rb2d;
	private PolygonCollider2D pc2d;
	private Animator anim;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		pc2d = GetComponent<PolygonCollider2D>();
		anim = GetComponent<Animator>();
	}

	bool SlowDown() {
		return Input.GetKey(KeyCode.LeftArrow);
	}

	bool SpeedUp() {
		return Input.GetKey(KeyCode.RightArrow);
	}

	bool Boost() {
		if (boostTimer >= boostTimeout && Input.GetKeyDown(KeyCode.RightArrow)) {
			boostTimer = 0;
			return true;
		}
		return false;
	}

	bool Jump() {
		var jumpPressed = (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow));
		if (jumpPressed && canJump) {
			canJump = false;
			return true;
		}
		return false;
	}

	// Update is called once per frame
	void Update () {
		// Set boost timer
		boostTimer += Time.deltaTime;
		if (boostTimer < boostDuration) {
			maxVelocity = boostVelocity;
		} else if (SpeedUp()) {
			maxVelocity = Math.Max(speedUpVelocity, Math.Min(rb2d.velocity.x, maxVelocity - (5 * Time.deltaTime)));
		} else {
            // https://answers.unity.com/questions/1242672/how-to-increase-and-decrease-speed-gradually.html
            maxVelocity = Math.Max(baseVelocity, Math.Min(rb2d.velocity.x, maxVelocity - (5 * Time.deltaTime)));
		}

		// Cap x velocity
		var newVel = rb2d.velocity.x;
		if (rb2d.velocity.x > maxVelocity) {
			newVel = maxVelocity;
        } else if (rb2d.velocity.x < minVelocity && !onWaterfall) {
            newVel = minVelocity;
        }
		rb2d.velocity = new Vector2(newVel, rb2d.velocity.y);

		// Testing no collision on going up 
		Physics2D.IgnoreLayerCollision (0, 0, rb2d.velocity.y > 0);

		if (SlowDown()) {
			anim.SetTrigger("Slowdown");
			pc2d.sharedMaterial.friction = 1.5f;
		} else {
			pc2d.sharedMaterial.friction = 0f;
		}
		// stupid hack because of unity bug
		pc2d.enabled = false;
		pc2d.enabled = true;

		if (isDead == false) {
			if (Jump()) {
				rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
				rb2d.AddForce(new Vector2(0, upForce));
				anim.SetTrigger("Jump");
			}

            if (Boost()) {
                rb2d.AddForce(new Vector2(200, 30));
				anim.SetTrigger("Boost");
            }
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
	
		if (col.gameObject.tag == "Waterfall") {
			Debug.Log("object collision enter: " + col.gameObject.tag, col.gameObject);
			rb2d.velocity = new Vector2(6f, -2f);
			onWaterfall = true;
		}
		canJump = true;
		// rb2d.velocity = Vector2.zero;
		// isDead = true;
		// anim.SetTrigger("Die");
		// BirdDied();
	}

	// Never seems to be in the stay state
	// void OnCollisionStay2D(Collision2D col) {
	
	// 	if (col.gameObject.tag == "Waterfall") {
	// 		Debug.Log("object collision stay: " + col.gameObject.tag, col.gameObject);
	// 		rb2d.velocity = new Vector2(6f, -2f);
	// 		onWaterfall = true;
	// 	}
	// 	canJump = true;
	// }

	void OnCollisionExit2D(Collision2D col) {
	
		if (col.gameObject.tag == "Waterfall") {
			Debug.Log("object exiting collision: " + col.gameObject.tag, col.gameObject);
			rb2d.velocity = new Vector2(0.0f, -2f);
			onWaterfall = true;
		}
	}
}
