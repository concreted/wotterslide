using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour {
	
	private Bird bird;
	private Rigidbody2D rb2d;

	private void OnTriggerEnter2D(Collider2D other) {
		bird = other.GetComponent<Bird>();
		if (bird != null) {
			GameController.instance.BirdScored();
		}
	}

	// private void OnCollisionEnter2D(Collision2D col) {
	// 	bird = col.gameObject.GetComponent<Bird>();
	// 	if (bird != null) {
	// 		bird.setCanJump(true);
	// 	}
	// } 
}
