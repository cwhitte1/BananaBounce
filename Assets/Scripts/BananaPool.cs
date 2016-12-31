using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BananaPool : MonoBehaviour {

	public GameObject[] Bananas; // array for object pool of Bananas 
	public int size; // number of Bananas in object pool
	public int pointTally; // number of Bananas collected by Monkeys

	public Text points;

	// Use this for initialization
	void Start () {
		points.text = "0";
		for (int a = 0; a < size; a++) {
			float prob = Random.Range (0f, 100.0f);
			if((Bananas[a].GetComponent<BananaMovement>().setActive == false) && (prob < 25)) { // starts with 25% chance Banana will spawn
				Bananas [a].GetComponent<BananaMovement> ().setActive = true; // activate Banana so it can be generated
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int a = 0; a < size; a++) {
			float prob = Random.Range (0f, 100.0f); // random number that helps decide if new Banana will spawn
			if ((Bananas [a].GetComponent<BananaMovement> ().setActive == false) && (prob < 1)) { // less than 1% chance Banana will spawn
				Bananas [a].GetComponent<BananaMovement> ().setActive = true;
			}
		}
		for (int a = 0; a < size; a++) { // collect avaliable tokens from Bananas, representing a Banana being caught by a Monkey
			if (Bananas [a].GetComponent<BananaMovement> ().Token == true) {
				pointTally += 1;
				Bananas [a].GetComponent<BananaMovement> ().Token = false;
			}
		}

		points.text = pointTally.ToString ("####");
	}
}