using UnityEngine;
using System.Collections;

public class TreeBehaviour : MonoBehaviour {
	public int maxHealth = 100;
	public int curHealth = 100;
	public int armor = 0;

	public int lumberVal = 10;
	public int type; // used for saving the game

	public GameObject stump;

	void Start () {
		curHealth = maxHealth;
	}

	// called by the lumberjacks when they chop the tree
	// returns a value to the lumberjack based on how the tree reacts to the chop
	public int getChopped( int axe ) {
		if (curHealth <= 0) {
			// tree is already dead, no wood for you!
			return -1;

		} else {
			//chop chop!
			int oldHealth = curHealth;
			int damage = Mathf.Max(axe - armor, 0);
			curHealth -= damage;

			if (curHealth == oldHealth) {
				// its no use!
				return -2;
			} else {
				// successful chop! check if the tree is dead
				if (curHealth <= 0) {
					// rip tree
					Instantiate(stump, transform.position, Quaternion.identity);
					Destroy (this.gameObject);
					return lumberVal;
				} else {
					// tree is not dead yet
					return -3;
				}
			}
		}
	}
}
