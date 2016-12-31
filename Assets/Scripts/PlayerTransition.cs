using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerTransition : MonoBehaviour {

	public GameObject[] Monkey; // Stores the two Monkeys

	public Text leftActive; // Text that shows left Monkey is in use
	public Text rightActive; // Text that shows right Monkey is in use
	public Text timeText; 
	public Text leftWarning; // Text that warns player game will end during the last 6 seconds
	public Text rightWarning; // Text that warns player game will end during the last 6 seconds
	public float targetTime = 30.0f;

	public GameObject godTree; // main center Tree
	public GameObject timeCoconut; // intersectable Coconut object that adds time
	public GameObject pointClown; // intersectable Clown object that takes away Bananas

    // Use this for initialization
    void Start () { // make all texts initially empty
		leftActive.text = "";
		rightActive.text = "";
		leftWarning.text = "";
		rightWarning.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		float leftMonkeyY = Monkey[0].GetComponent<PlayerMovement> ().yPos; // y pos of left Monkey
		float rightMonkeyY = Monkey[1].GetComponent<PlayerMovement> ().yPos; // y pos of right Monkey

		if (leftMonkeyY > rightMonkeyY) { // if left Monkey is higher up than right Monkey
			Monkey [0].GetComponent<PlayerMovement> ().active = true; // set left Monkey active
			Monkey [1].GetComponent<PlayerMovement> ().active = false; // set right Monkey inactive
			leftActive.text = "Active"; // display active text for left Monkey
			rightActive.text = ""; // hide active text for right Monkey
		} else { // if right Monkey is higher up than left Monkey
			Monkey [1].GetComponent<PlayerMovement> ().active = true; // set right Monkey active
			Monkey [0].GetComponent<PlayerMovement> ().active = false; // set left Monkey inactive
			rightActive.text = "Active"; // display active text for right Monkey
			leftActive.text = ""; // hide active text for left Monkey
		}

		if (targetTime < 6 && targetTime > 5) { // in 6th second left
			leftWarning.text = "Dance Monkey,";
		} else if (targetTime < 5 && targetTime > 4) { // in 5th second left
			rightWarning.text = "Dance!";
			leftWarning.text = "";
		} else if (targetTime < 4 && targetTime > 3) { // in 4th second left
			leftWarning.text = "Dance Monkey,";
			rightWarning.text = "";
		} else if (targetTime < 3 && targetTime > 2) { // in 3rd second left
			rightWarning.text = "Dance!";
			leftWarning.text = "";
		} else if (targetTime < 2 && targetTime > 1) { // in 2nd second left
			leftWarning.text = "Dance Monkey,";
			rightWarning.text = "";
		} else if (targetTime < 1) { // in last second left
			leftWarning.text = "";
			rightWarning.text = "Dance!";
		} else { // if higher than 6, do not display
			leftWarning.text = "";
			rightWarning.text = "";
		}

		if (timeCoconut.GetComponent<CoconutMovement> ().Token == true) { // if Coconut token is avaliable, take and add 15 sec to gameplay
			targetTime = targetTime + 15;
			timeCoconut.GetComponent<CoconutMovement> ().Token = false;
		}

		if (pointClown.GetComponent<ClownMovement> ().Token == true) { // if Clown token is avaliable, take and remove 10 Bananas (Points)
			int oldPoints = godTree.GetComponent<BananaPool> ().pointTally;
			if (oldPoints <= 10) {
				godTree.GetComponent<BananaPool> ().pointTally = 0;
			} else {
				godTree.GetComponent<BananaPool> ().pointTally = oldPoints - 10;
			}
			pointClown.GetComponent<ClownMovement> ().Token = false;
		}
			
		if (targetTime > 0) // display text countdown if greater than 0
		{
			timeText.text = targetTime.ToString("N4");
		}else{ // end game if timer has hit 0
			timeText.text = "0.0000";
			if (godTree.GetComponent<BananaPool> ().pointTally < 10) { // if less than 10 Bananas were collected, go to sad ending
				Application.LoadLevel ("DeathSad");
			} else { // if more than 10 Bananas were collected, go to happy ending
				Application.LoadLevel ("DeathHappy");
			}
		}

		targetTime -= Time.deltaTime; // subtract time every update
		Debug.Log(targetTime);
	}
}
