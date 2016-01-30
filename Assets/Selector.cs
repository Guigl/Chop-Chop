using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : MonoBehaviour {

	public List<GameObject> lumberjacks;
	public GameObject activeLumberjack;


	public GameObject findLumberjack (string name) {
		foreach (GameObject lj in lumberjacks) {
			Character curCharacter = lj.GetComponent<Character> ();
			if (curCharacter.charName == name) {
				return lj; 
			}
		}
		return null;
	}

	public void makeActiveLumberjack (GameObject lj) {
		if (lj.GetComponent<Character> ())
			activeLumberjack = lj;
		else
			Debug.Log ("GameObject given to makeActiveLumberjack was not a lumberjack and that's not okay");
	}

	public void addLumberjack (GameObject lj) {
		//Make sure there is a check in the character creator that checks for duplicate names
		//Add the lumberjack to the selector /after it has gone through creation
		lumberjacks.Add(lj);

	}

	// Use this for initialization
	void Start () {
		lumberjacks = new List<GameObject> ();
		activeLumberjack = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
