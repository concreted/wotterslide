using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePool : MonoBehaviour {

	private GameObject[] slides;
	public int slidePoolSize = 6;
	public GameObject slidePrefab;
	public float spawnRate = 0.3777f;
	public float ySlideMin = -5f;
	public float ySlideMax = 5f;
	public float xSlideMin = 0f;
	public float xSlideMax = 10f;

	private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
	private int currentSlide = 0;
	private Quaternion spawnRotation = Quaternion.Euler(0,0,270);

	private float timeSinceLastSpawned;

	//Use this for initialization
	void Start () {
		timeSinceLastSpawned = spawnRate;
		slides = new GameObject[slidePoolSize];
		for (int i = 0; i < slidePoolSize; i++) {
			slides[i] = (GameObject) Instantiate(slidePrefab, objectPoolPosition, spawnRotation);
		}
	}

	// Update is called once per frame
	void Update () {
		timeSinceLastSpawned += Time.deltaTime;

		if (GameController.instance.gameOver == false && timeSinceLastSpawned >= spawnRate) {
			timeSinceLastSpawned = 0;
			float spawnYPos = Random.Range(ySlideMin, ySlideMax);
			float spawnXPos = Random.Range(xSlideMin, xSlideMax);
			slides[currentSlide].transform.position = new Vector2(spawnXPos, spawnYPos);
			currentSlide = (currentSlide + 1) % slidePoolSize;
		}
	}
}
