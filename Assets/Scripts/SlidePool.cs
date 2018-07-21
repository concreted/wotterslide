using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePool : MonoBehaviour {

	// private GameObject[] slides;
	private GameObject[][] levelSections;
	public int slidePoolSize = 6;

	public int levelSectionBufferSize = 3;
	public GameObject slidePrefab;
	public float spawnRate = 2f;
	public float ySlideMin = -5f;
	public float ySlideMax = 5f;
	public float xSlideMin = 0f;
	public float xSlideMax = 10f;

	private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
	private int currentLevelSection = 0;
	private Quaternion spawnRotation = Quaternion.Euler(0,0,270);

	private float timeSinceLastSpawned;

	//Use this for initialization
	void Start () {
		timeSinceLastSpawned = spawnRate;
		levelSections = new GameObject[levelSectionBufferSize][];
		// Instantiate the level sections.
		for (int i = 0; i < levelSectionBufferSize; i++) {
			var slides = new GameObject[slidePoolSize];
			for (int j = 0; j < slidePoolSize; j++) {
				slides[j] = (GameObject) Instantiate(slidePrefab, objectPoolPosition, spawnRotation);
			}
			levelSections[i] = slides;
		}
	}

	void FormatLevelSection(GameObject[] levelSection) {
		var positions = new Vector2[]{
			new Vector2(3f, -1f),
            new Vector2(10f, -7f),
            new Vector2(9f, 3f),
            new Vector2(13f, 1f),
            new Vector2(16f, -5f),
            new Vector2(21f, -1f),
		};
		// move it offscreen
		var xOffset = 16f + Random.Range(-4f, 4f);
		var yOffset = Random.Range(-4f, 4f);

		for (int i = 0; i < slidePoolSize; i++) {
			var position = positions[i];
			levelSection[i].transform.position = new Vector2(position.x + xOffset, position.y + yOffset);
		}
	}

	// Update is called once per frame
	void Update () {
		timeSinceLastSpawned += Time.deltaTime;


		if (GameController.instance.gameOver == false && timeSinceLastSpawned >= spawnRate) {
			timeSinceLastSpawned = 0;

            Debug.Log(levelSections[currentLevelSection].Length);
			FormatLevelSection(levelSections[currentLevelSection]);

			// float spawnYPos = Random.Range(ySlideMin, ySlideMax);
			// float spawnXPos = Random.Range(xSlideMin, xSlideMax);
			// slides[currentSlide].transform.position = new Vector2(spawnXPos, spawnYPos);
			currentLevelSection = (currentLevelSection + 1) % levelSectionBufferSize;

		}
	}
}
