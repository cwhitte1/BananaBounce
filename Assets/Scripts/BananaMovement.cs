using UnityEngine;
using System.Collections;

public class BananaMovement : MonoBehaviour {

	public GameObject Banana;
	public bool setActive = false; // Banana is inactive to begin; will be set active in BananaPool
	public bool onScreen = false; // Banana is off screen to begin
	public Transform Check;

	private float Speed;
	public bool Token = false;

	// initialization
	void Start () {
		if (setActive) { //initial activity determined in BananaPool
			generateBanana ();
			onScreen = true;
		} else {
			transform.position = new Vector2 (0, -500); // places Banana in non-used space and is left inactive
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (setActive && onScreen) { // when Banana is within screen boundries and active
			if (Speed > 0) { // if the speed is positive the Banana is moving right
				if (transform.position.x > 40) { // if the Banana has left the right boundries of the screen, disable it and place back in pool
					transform.position = new Vector2 (0, -500);
					setActive = false;
					onScreen = false;
				} else { // continue rightwards movement of Banana
					transform.position = new Vector2 (transform.position.x + Speed, transform.position.y);
				}
			} else { // if the speed is negative the Banana is moving left
				if (transform.position.x < -40) { // if the Banana has left the left boundries of the screen, disable it and place back in pool
					transform.position = new Vector2 (0, -500);
					setActive = false;
					onScreen = false;
				} else { // continue leftwards movement of Banana
					transform.position = new Vector2 (transform.position.x + Speed, transform.position.y);
				}
			}
		} else if (setActive && !onScreen) { // if Banana has been activated but not put on screen, spawn Banana on screen
			generateBanana ();
			onScreen = true;
		} else { // if Banana is inactive, stay/become inactive
			transform.position = new Vector2 (0, -500);
			setActive = false;
			onScreen = false;
		}
	}

	void generateBanana () {
		float randomNumberX = Random.Range(0f, 100.0f); //random value used to determine in Banana enters from left of right
		float randomNumberY = Random.Range(0f, 14.0f); // random value that determines y pos of Banana
		print (randomNumberX);
		print (randomNumberY);
		if (randomNumberX > 50) { // 50% chance the Banana moves left
			transform.position = new Vector2 (40, randomNumberY);
			Speed = -0.1f; // speed of Banana
		} else { // 50% chance the Banana moves right
			transform.position = new Vector2 (-40, randomNumberY);
			Speed = 0.1f; // speed of Banana
		}
	}

	void OnTriggerEnter2D(Collider2D other) // if Banana intersects Monkey, it is set inactive
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Death"))
		{
			setActive = false;
			Token = true; // token is set true, and will be 'collected' in BananaPool
		}
	}	
}
