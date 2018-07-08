using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{

	private float scrollSpeedX;
	private float scrollSpeedY;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
		scrollSpeedX = 10;
		scrollSpeedY = 20;
        rb2d = GetComponent<Rigidbody2D>();
        // rb2d.velocity = new Vector2(-scrollSpeedX, scrollSpeedY);
    }

    // Update is called once per frame
    void Update()
    {
        // rb2d.velocity = new Vector2(scrollSpeedX, scrollSpeedY);
        // rb2d.velocity.Set(scrollSpeed, 0);
        // if (GameController.instance.gameOver == true)
        // {
        //     rb2d.velocity = Vector2.zero;
        // }
    }
}