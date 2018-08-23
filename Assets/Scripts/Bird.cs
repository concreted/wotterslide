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

	private int boostCounter = 0;

    Transform tr;
    float height;
    int layerMask;

	private PolygonCollider2D collider;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		collider = GetComponent<PolygonCollider2D>();

        tr = transform;
        // height = collider.bounds.extent.y;
        layerMask = LayerMask.NameToLayer("OneWayPlatform");
		Debug.Log(layerMask);
	}

	// Update is called once per frame
	void Update () {
		if (isDead == false) {
			RaycastHit2D[] hits = Physics2D.RaycastAll(tr.position, Vector2.down, 0.5f);
			Debug.DrawRay(tr.position, Vector2.down, Color.red, 1f);
			bool hasObstacle = false;
			// this doesn't detect instantiated prefabs for some reason.
			foreach(RaycastHit2D hit in hits) {
                if (hit && hit.normal != -Vector2.down){
                    hasObstacle = true;
                }
			}
			if (hasObstacle) {
				anim.ResetTrigger("Airborne");
				anim.SetTrigger("Land");
			}
			else {
				anim.ResetTrigger("Land");
				anim.SetTrigger("Airborne");
			}

			rb2d.velocity = new Vector2(0, rb2d.velocity.y);
			if (GameController.instance.GetInput() && jumpCounter < 2) {
				// Testing no collision on going up
				rb2d.velocity = new Vector2(0, 0);
				rb2d.AddForce(new Vector2(0, upForce));
                anim.ResetTrigger("Airborne");
				anim.SetTrigger("Jump");
				incrementJumpCounter();
                // transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			// Boosting
			if (GameController.instance.GetBoost() && boostCounter < 2) {
                // rb2d.AddForce(new Vector2(upForce * 2, 0));
				GameController.instance.boostTime = GameController.instance.boostTimeLimit;
				anim.ResetTrigger("Airborne");
				anim.ResetTrigger("Jump");
				anim.ResetTrigger("Land");
				anim.SetTrigger("Boost");
				incrementBoostCounter();
                transform.rotation = Quaternion.Euler(0, 0, 0);
			}


			if (rb2d.velocity.y >= 0) {
				rb2d.gameObject.layer = 9;
			} else {
				rb2d.gameObject.layer = 8;
			}
			// Debug.Log("Layer: " + rb2d.gameObject.layer, rb2d.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		// Debug.Log("Collided with: " + col.gameObject.tag, col.gameObject);
		if (col.gameObject.tag == "Slide") {
			// otter is landing on a slide
			// if (rb2d.velocity.y <= 0) {
			// 	anim.SetTrigger("Land");
			// }
			resetJumpCounter();
			resetBoostCounter();
		}
		// else {
		// 	rb2d.velocity = Vector2.zero;
		// 	isDead = true;
		// 	triggerDeathAnim();
		// 	GameController.instance.BirdDied();
		// }
	}

	void incrementJumpCounter() {
		jumpCounter++;
	}

	void incrementBoostCounter() {
        boostCounter++;
	}

	void resetJumpCounter() {
		jumpCounter = 0;
	}

	void resetBoostCounter() {
		boostCounter = 0;
	}

	public void Kill() {
        triggerDeathAnim();
		isDead = true;
	}
	public void triggerDeathAnim(){
		anim.SetTrigger("Die");
	}
}
