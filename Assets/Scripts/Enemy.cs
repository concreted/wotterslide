using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private Bird bird;
	private SpriteRenderer sr;

    private Animator anim;

	void Start () {
		sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
		sr.enabled = false;
	}

	void Reset() {
		sr.enabled = false;
		anim.ResetTrigger("Explode");
	}

	private void OnTriggerEnter2D(Collider2D other) {
		bird = other.GetComponent<Bird>();
		if (bird != null) {
			if (GameController.instance.boostTime > 0) {
				GameController.instance.BirdScored();
				anim.SetTrigger("Explode");
                anim.SetBool("Alive", false);
				// sr.enabled = false;
			} else {
				bird.Kill();
			}
		}
	}
}
