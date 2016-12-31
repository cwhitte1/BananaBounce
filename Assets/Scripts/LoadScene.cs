using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public GameObject trig;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void go () { // goes to main play scene
		Application.LoadLevel ("BananaBounce");
	}

	public void goBack() { // goes to intro screen (from instruction screen)
		Application.LoadLevel ("IntroScreen");
	}

	public void goInstructions() { // goes to instruction screen (from intro screen)
		Application.LoadLevel ("Instructions");
	}
}
