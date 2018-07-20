using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour {

	private GameObject[] columns;
	public int columnPoolSize = 2;
	public GameObject columnPrefab;
	public float spawnRate = 4f;
	public float columnMin = -1f;
	public float columnMax = 3.5f;

	private float spawnXPos = 10f;
	private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
	private int currentColumn = 0;

	private float timeSinceLastSpawned;

	// Use this for initialization
	// void Start () {
	// 	timeSinceLastSpawned = spawnRate;
	// 	columns = new GameObject[columnPoolSize];
	// 	for (int i = 0; i < columnPoolSize; i++) {
	// 		columns[i] = (GameObject) Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
	// 	}
	// }

	// // Update is called once per frame
	// void Update () {
	// 	timeSinceLastSpawned += Time.deltaTime;

	// 	if (GameController.instance.gameOver == false && timeSinceLastSpawned >= spawnRate) {
	// 		timeSinceLastSpawned = 0;
	// 		float spawnYPos = Random.Range(columnMin, columnMax);
	// 		columns[currentColumn].transform.position = new Vector2(spawnXPos, spawnYPos);
	// 		currentColumn = (currentColumn + 1) % columnPoolSize;
	// 	}
	// }
}
