using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Character : MonoBehaviour {
	public enum charStates {idle, walking, chopping, returning};
	public string charName = "Jack";
	public string password = "123456";
	public float walkSpeed = 1.0f;
	public float axeSpeed = 1.0f; // time it takes to swing the axe in seconds
	public int axePower = 1;

	public int backpackSize = 1;
	public int lumberCount = 100;
	public float axeCooldown = 0.0f;

	public Vector3 wayPoint;
	public GameObject targetTree;
	public GameObject targetMill;

	public charStates doing = charStates.idle;

	public List<int> backpack = new List<int>();

	public void loadCharacter (string n, float w, float s, int a, int b, int l) {
		// initialize character values
		charName = n;
		walkSpeed = w;
		axeSpeed = s;
		axePower = a;
		backpackSize = b;
		lumberCount = l;

		// make the name display above their head
		transform.GetComponentInChildren<TextMesh>().text = "Lumber" + charName;	}


	void FixedUpdate () {
	
		// if we are idle, find a new tree to chop
		if (doing == charStates.idle) {
			// determine what we need to do next
			if (backpack.Count < backpackSize) {
				// find a tree
				targetTree = GameObject.FindGameObjectWithTag ("Tree");
				foreach (GameObject t in GameObject.FindGameObjectsWithTag("Tree")) {
					if (Vector3.Distance (this.transform.position, t.transform.position) <
					    Vector3.Distance (this.transform.position, targetTree.transform.position)) {

						targetTree = t;
					}
				}
				wayPoint = targetTree.transform.position;
				doing = charStates.walking;

			} else {
				// find a lumbermill to deposit wood at
				targetMill = GameObject.FindGameObjectWithTag ("LumberMill");
				foreach (GameObject m in GameObject.FindGameObjectsWithTag("LumberMill")) {
					if (Vector3.Distance (this.transform.position, m.transform.position) <
					    Vector3.Distance (this.transform.position, targetMill.transform.position)) {

						targetMill = m;
					}
				}
				wayPoint = targetMill.transform.position;
				doing = charStates.returning;
			}
		} else if (doing == charStates.walking) {
			// move towards the tree

			if (Vector3.Distance (transform.position, wayPoint) > 2.0f) {
				transform.LookAt (wayPoint);
				transform.position += transform.forward * walkSpeed * Time.deltaTime * 10;
			} else {
				// we are close to the tree, start chopping
				doing = charStates.chopping;

				// select the closest tree to start chopping
				targetTree = GameObject.FindGameObjectWithTag ("Tree");
				foreach (GameObject t in GameObject.FindGameObjectsWithTag("Tree")) {
					if (Vector3.Distance (this.transform.position, t.transform.position) <
					    Vector3.Distance (this.transform.position, targetTree.transform.position)) {

						targetTree = t;
					}
				}

			}
		} else if (doing == charStates.chopping) {
			// make sure our target tree still exists
			if (targetTree) {
				int retval = targetTree.GetComponent<TreeBehaviour> ().getChopped (axePower);
				if (retval > 0) {
					// got some lumber!
					backpack.Add (retval);
					// check if we have more pack space
					if (backpack.Count < backpackSize) {
						// we can get more trees!
						doing = charStates.idle;
					} else {
						// pack is full, dump it out!
						doing = charStates.returning;
					}

				} else if (retval == -1 || retval == -2) {
					// the tree is either gone, or we are too weak to cut it
					doing = charStates.idle;
				}


			}
		} else if (doing == charStates.returning) {
			if (Vector3.Distance (transform.position, wayPoint) > 5.0f) {
				transform.LookAt (wayPoint);
				transform.position += transform.forward * walkSpeed * Time.deltaTime * 10;
			} else {
				// empty out the backpack and get lods e mone!
				foreach (int val in backpack) {
					lumberCount += val;
				}
				backpack.Clear ();

				doing = charStates.idle;
			}
		}

	}

	void setWaypoint( Vector3 point) {
		wayPoint = point;
	}
}
