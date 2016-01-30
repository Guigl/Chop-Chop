using UnityEngine;
using System.Collections;



public class Character : MonoBehaviour {
	public enum charStates {idle, walking, chopping};
	public string charName = "Jack";

	public float walkSpeed = 1.0f;
	public float axePower = 1.0f;

	public int backpackSize = 1;
	public int lumberCount = 100;

	public Vector3 wayPoint;
	public charStates doing = charStates.idle;

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


	void FixedUpdate () {
	
		// if we are idle, find a new tree to chop
		if (doing == charStates.idle) {
			// find a tree

		} else if (doing == charStates.walking) {
			// move towards the tree

			if (Vector3.Distance (transform.position, wayPoint) > 2.0f) {
				transform.LookAt (wayPoint);
				transform.position += transform.forward * walkSpeed * Time.deltaTime * 10;
			} else {
				// we are close to the tree, start chopping
				doing = charStates.chopping;
			}
		}

	}

	void setWaypoint( Vector3 point) {

	}
}
