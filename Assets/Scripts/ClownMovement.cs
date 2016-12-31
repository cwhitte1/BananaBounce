using UnityEngine;
using System.Collections;

public class ClownMovement : MonoBehaviour {

	public GameObject Clown;
	public bool setActive = false; // Clown is inactive to begin
	public bool onScreen = false; // Clown is off screen to begin
	public Transform Check;

	private float Speed;
	public bool Token = false;


	// Use this for initialization
	void Start () {

		float randomChance = Random.Range(0f, 100.0f); // Random number generated to help determine if Clown is initially spawned

		if (randomChance < 3) { // 3% chance that the Clown initially spawns
			setActive = true; // Set Clown active
		}

		if (setActive) { // if the Clown is active, spawn it on screen using generateClown
			generateClown ();
			onScreen = true;
		} else { // if the Clown is not active, place off screen
			transform.position = new Vector2 (0, -550);
		}
	}

	// Update is called once per frame
	void Update () {

		float randomChance = Random.Range(0f, 100.0f); // Random number generated every update to determine if Clown is spawned
		if ((randomChance < .5) && !setActive) { // Less than .5% chance the Clown is spawned
			setActive = true;
		}

		if (setActive && onScreen) { // if the Clown is on the screen and active
			if (Speed > 0) { // if the spawned Clown is moving right
				if (transform.position.x > 40) { // if the Clown's x position is out of the right limit, move off screen and make inactive
					transform.position = new Vector2 (0, -550);
					setActive = false;
					onScreen = false;
				} else { // otherwise move Clown right
					transform.position = new Vector2 (transform.position.x + Speed, transform.position.y);
				}
			} else { // if the spawned Clown is moving left
				if (transform.position.x < -40) { // if the spawbed Clown's x position is out of the right limit, move off screen and make inactive
					transform.position = new Vector2 (0, -550);
					setActive = false;
					onScreen = false;
				} else { // otherwise move Clown left
					transform.position = new Vector2 (transform.position.x + Speed, transform.position.y);
				}
			}
		} else if (setActive && !onScreen) { // if the Clown is active but not on screen, spawn Clown on screen
			generateClown ();
			onScreen = true;
		} else { // otherwise if Clown is inactive, stay/become inactive
			transform.position = new Vector2 (0, -550);
			setActive = false;
			onScreen = false;
		}
	}

	void generateClown () {
		float randomNumberX = Random.Range(0f, 100.0f); // random value used to determine if Clown enters from left or right
		float randomNumberY = Random.Range(0f, 14.0f); // random value that determines y pos of Clown
		print (randomNumberX);
		print (randomNumberY);
		if (randomNumberX > 50) { // 50% chance the Clown moves left
			transform.position = new Vector2 (40, randomNumberY);
			Speed = -0.07f; // speed of Clown
		} else { // 50% chance the Clown moves right
			transform.position = new Vector2 (-40, randomNumberY);
			Speed = 0.07f; // speed of Clown
		}
	}

	void OnTriggerEnter2D(Collider2D other) // if Clown intersects Monkey, it is set inactive
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Death"))
		{
			setActive = false;
			Token = true; // token is set true, and will be 'collected' in PlayerTransition
		}
	}	
}