using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float walkSpeed = 3f;
    public float jumpPower = 600f;

    public Transform Check;
    public LayerMask groundMask;

	public float leftLimit = -20f; // left limit of Monkey movement on screen; makes where Monkey can not leave see-saw
	public float rightLimit = 20f; // right limit of Monkey movement on screen; makes where Monkey can not leave see-saw
	public float upperLimit = 20f; // top limit of Monkey movement on screen; makes where Monkey can't fly through top of screen

	public bool active = false; // Monkey initially inactive; activity determined in PlayerTransition
	public bool grounded = false; // Use Check to determine if Monkey has made contact with the see-saw

    private Animator animationController;

    private Rigidbody2D theRigidBody;

	public float yPos = 0f;

	// Use this for initialization
	void Start () {
        theRigidBody = GetComponent<Rigidbody2D>();
        animationController = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		grounded = Physics2D.OverlapCircle (Check.position, 0.5f, groundMask); // checks to see if Monkey is in contact with see-saw

		if (active) {
			float inputX = Input.GetAxis ("Horizontal");
			theRigidBody.velocity = new Vector2 (inputX * walkSpeed, theRigidBody.velocity.y);


			bool jumping = Input.GetButtonDown ("Jump");

			animationController.SetFloat ("speed", theRigidBody.velocity.x);
			animationController.SetBool ("grounded", grounded);

			yPos = transform.position.y; // remembers the y-position of Monkey in event Monkey needs to be shifted back into boundries

			if (transform.position.x < leftLimit) { // if Monkey is too far left
				transform.position = new Vector2 (leftLimit, transform.position.y);
			}

			if (transform.position.x > rightLimit) { // if Monkey is too far right
				transform.position = new Vector2 (rightLimit, transform.position.y);
			}

			if (transform.position.y > upperLimit) { // if Monkey is too high off see-saw
				transform.position = new Vector2 (transform.position.x, upperLimit);
			}
	        
			if (jumping && grounded) { // if the Monkey is in contact with the see-saw, it can jump
				theRigidBody.AddForce (new Vector2 (0f, jumpPower));
			}
		}
	}
}
