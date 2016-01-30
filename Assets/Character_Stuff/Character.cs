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

	public int axePowerLv = 1;
	public int axeSpeedLv = 1;
	public int walkLv = 1;
	public int backpackLv = 1;

	public int backpackSize = 1;
	public int money = 0;
	public int lumberCount = 100;
	public float axeCooldown = 0.0f;

	public Vector3 wayPoint;
	public GameObject targetTree;
	public GameObject targetMill;

	public charStates doing = charStates.idle;

	public List<int> backpack = new List<int>();

	public void loadCharacter (string lumberJackName, string lumberJackPassword, float lumberJackwalkingSpeed,
		float lumberJackAxeSpeed, int lumberJackAxePower, int lumberjackPackSize, int lumberJackMoney, int lumberJackLumberTotal) {
		// initialize character values
		charName = lumberJackName;
		password = lumberJackPassword;
		walkSpeed = lumberJackwalkingSpeed;
		axeSpeed = lumberJackAxeSpeed;
		axePower = lumberJackAxePower;
		backpackSize = lumberjackPackSize;
		money = lumberJackMoney;
		lumberCount = lumberJackLumberTotal;


		// make the name display above their head
		transform.GetComponentInChildren<TextMesh>().text = "Lumber" + charName;	}


	void FixedUpdate () {
	
		// if we are idle, find a new tree to chop
		if (doing == charStates.idle) {
			// determine what we need to do next
			if (backpack.Count < backpackSize) {
				Debug.Log ("backpack.count: " + backpack.Count);
				// find a tree
				targetTree = GameObject.FindGameObjectWithTag ("Tree");
				foreach (GameObject t in GameObject.FindGameObjectsWithTag("Tree")) {
					if (Vector3.Distance (this.transform.position, t.transform.position) <
					    Vector3.Distance (this.transform.position, targetTree.transform.position)) {

						targetTree = t;
					}
				}
				wayPoint = targetTree.transform.position;
				wayPoint.y = 6.0f;
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
				wayPoint.y = 6.0f;
				doing = charStates.returning;
			}
		} else if (doing == charStates.walking) {
			// move towards the tree

			if (Vector3.Distance (transform.position, wayPoint) > 4.0f) {
				transform.LookAt (wayPoint);
				Vector3 rotationVector = transform.rotation.eulerAngles;
				rotationVector.z = 0;
				transform.rotation = Quaternion.Euler(rotationVector);
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
				if (axeCooldown <= 0.0f) {
					axeCooldown = axeSpeed;
				} else {
					axeCooldown -= Time.fixedDeltaTime;
					return;
				}
				int retval = targetTree.GetComponent<TreeBehaviour> ().getChopped (axePower);
				
				if (retval > 0) {
					// got some lumber!
					backpack.Add (retval);
					Debug.Log ("chopped a tree: " + backpack.Count);
					// check if we have more pack space
					if (backpack.Count < backpackSize) {
						// we can get more trees!
						doing = charStates.idle;
					} else {
						// pack is full, dump it out!
						doing = charStates.idle;
					}

				} else if (retval == -1 || retval == -2) {
					// the tree is either gone, or we are too weak to cut it
					doing = charStates.idle;
				}


			}
		} else if (doing == charStates.returning) {
			if (Vector3.Distance (transform.position, wayPoint) >= 8f) {
				
				transform.LookAt (wayPoint);
				Vector3 rotationVector = transform.rotation.eulerAngles;
				rotationVector.z = 0;
				transform.rotation = Quaternion.Euler(rotationVector);
				transform.position += transform.forward * walkSpeed * Time.deltaTime * 10;
			} else {
				// empty out the backpack and get lods e mone!
				foreach (int val in backpack) {
					lumberCount += val;
					money += val;
				}
				Debug.Log ("money!");
				backpack.Clear ();

				doing = charStates.idle;
			}
		}

	}

	void setWaypoint( Vector3 point) {
		wayPoint = point;
		wayPoint.y = 6.0f;
	}
}
