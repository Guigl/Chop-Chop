using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public string charName = "Jack";

	public float walkSpeed = 1.0f;
	public float axePower = 1.0f;

	public int backpackSize = 1;
	public int lumberCount = 100;

	public void loadCharacter (string n, float w, float a, int b, int l) {
		// initialize character values
		charName = n;
		walkSpeed = w;
		axePower = a;
		backpackSize = b;
		lumberCount = l;

		// make the name display above their head
		transform.GetComponentInChildren<TextMesh>().text = "Lumber" + charName;

	}


	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {
	
	}
}
