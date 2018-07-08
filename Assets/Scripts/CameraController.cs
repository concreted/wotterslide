using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object
	public Rigidbody2D playerRb2d;
	public Text debugText;

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
	private Camera c;

    // Use this for initialization
    void Start()
    {
		playerRb2d = player.GetComponent<Rigidbody2D>();
        c = GetComponent<Camera>();
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame

	void Update() {
        // Change camera zoom based on player velocity
        c.orthographicSize = getCameraSize();
        debugText.text = c.orthographicSize.ToString() + " " + playerRb2d.velocity.ToString();
	}

    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }

	private float getCameraSize() {
		var playerVel = Math.Abs(playerRb2d.velocity.x);
		var percentVel = playerVel / 20f;
		return 5 + 7 * percentVel;
	}
}
