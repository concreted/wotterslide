using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private Bird bird;
	private SpriteRenderer sr;

	void Start () {
		sr = GetComponent<SpriteRenderer>();
		sr.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		bird = other.GetComponent<Bird>();
		if (bird != null) {
			if (GameController.instance.boostTime > 0) {
				GameController.instance.BirdScored();
				sr.enabled = false;
			} else {
				bird.triggerDeathAnim();
				GameController.instance.BirdDied();
			}
		}
	}
}
