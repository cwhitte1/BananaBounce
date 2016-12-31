using UnityEngine;
using System.Collections;

public class CoconutMovement : MonoBehaviour {

	public GameObject Coconut;
	public bool setActive = false; // Coconut is set inactive initially
	public bool onScreen = false; // Coconut is off screen initially
	public Transform Check;

	private float Speed;
	public bool Token = false;


	// Use this for initialization
	void Start () {

		float randomChance = Random.Range(0f, 100.0f); // Random number generated to help determine if Coconut is initially spawned

		if (randomChance < 3) { // 3% chance that the Coconut initially spawns
			setActive = true; // Set Coconut active
		}
	
		if (setActive) { // if the Coconut is active, spawn it on screen using generateCoconut
			generateCoconut ();
			onScreen = true;
		} else { // if the Coconut is not active, place off screen
			transform.position = new Vector2 (0, -550);
		}
	}
	
	// Update is called once per frame
	void Update () {

		float randomChance = Random.Range(0f, 100.0f); // Random number generated every update to determine if Coconut is spawned
		if ((randomChance < .5) && !setActive) { // Less than .5% chance Coconut is spawned
			setActive = true;
		}

		if (setActive && onScreen) { // if the Coconut is on the screen and active
			if (Speed > 0) { // if the spawned Coconut is moving right
				if (transform.position.x > 40) { // if the Coconut's x position is out of the right limit, move off screen and make inactive
					transform.position = new Vector2 (0, -550);
					setActive = false;
					onScreen = false;
				} else { // otherwise move Coconut right
					transform.position = new Vector2 (transform.position.x + Speed, transform.position.y);
				}
			} else { // if the spawned Coconut is moving left
				if (transform.position.x < -40) { // if the spawned Coconut's x position is out of the left limit, move off screen and make inactive
					transform.position = new Vector2 (0, -550);
					setActive = false;
					onScreen = false;
				} else { // otherwise move Coconut left
					transform.position = new Vector2 (transform.position.x + Speed, transform.position.y);
				}
			}
		} else if (setActive && !onScreen) { // if the Coconut is active but not on screen, spawn Coconut on screen
			generateCoconut ();
			onScreen = true;
		} else { // otherwise if Coconut is inactive, stay/become inactive
			transform.position = new Vector2 (0, -550);
			setActive = false;
			onScreen = false;
		}
	}

	void generateCoconut () {
		float randomNumberX = Random.Range(0f, 100.0f); // random value used to determine if Coconut enters from left or right
		float randomNumberY = Random.Range(0f, 14.0f); // random value that determines y pos of Coconut
		print (randomNumberX);
		print (randomNumberY);
		if (randomNumberX > 50) { // 50% chance the Coconut moves left
			transform.position = new Vector2 (40, randomNumberY);
			Speed = -0.19f; // speed of Coconut
		} else { // 50% chance the Coconut moves right
			transform.position = new Vector2 (-40, randomNumberY);
			Speed = 0.19f; // speed of Coconut
		}
	}

	void OnTriggerEnter2D(Collider2D other) // if Coconut intersects Monkey, it is set inactive
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Death"))
		{
			setActive = false;
			Token = true; // token is set true, and will be 'collected' in PlayerTransition
		}
	}	
}