using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController instance;
	public float scrollSpeed = -1.5f;

	private float baseScrollSpeed;
	public float speedUpRate = 0.5f;
	public int speedUpScore = 2;

	public float boostTime = 0f;

	public float boostTimeLimit = 0.3f;
	public Text scoreText;
	public GameObject gameOverText;
	public bool gameOver = false;

	public bool debugMode = false;

	private int score = 0;

	// Use this for initialization
	void Awake () {
		// Singleton pattern
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		baseScrollSpeed = scrollSpeed;
	}

	// Update is called once per frame
	void Update () {
		if (gameOver && GetInput()) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		scrollSpeed = baseScrollSpeed  - (50 * (boostTime / boostTimeLimit));
		boostTime = Math.Max(boostTime - Time.deltaTime, 0);
	}

	public void BirdScored() {
		if (gameOver) {
			return;
		}

		score++;
		scoreText.text = "Score: " + score.ToString();
		if (score % speedUpScore == 0) {
			SpeedUp();
			if (debugMode) {
				scoreText.text = "Speeding up! Speed: " + (-scrollSpeed).ToString() + " " + scoreText.text;
			}
		}
	}

	private void SpeedUp() {
		scrollSpeed -= speedUpRate;
	}

	public void BirdDied() {
		gameOverText.SetActive(true);
		gameOver = true;
	}

	public bool GetInput() {
		return (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space));
	}

    public bool GetBoost()
    {
        return (Input.GetKeyDown(KeyCode.RightArrow));
    }
}
