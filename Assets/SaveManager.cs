using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SaveManager {

	public static void save() {
		// save all lumberjacks, lumbermills, and trees

		// start with the lumberjacks
		List<string> jackNames = new List<string>();
		List<string> jackPass = new List<string>();

		List<int> jackLumber = new List<int>();
		List<int> jackMoney = new List<int>();

		List<int> jackWalkLevel = new List<int>();
		List<int> jackBackpackLevel = new List<int>();
		List<int> jackAxeSpeedLevel = new List<int>();
		List<int> jackAxePowerLevel = new List<int>();

		List<Vector3> jackPos = new List<Vector3>();
		List<Vector3> jackRotate = new List<Vector3>();

		foreach(GameObject jack in GameObject.FindGameObjectsWithTag("LumberJack")) {
			jackNames.Add (jack.GetComponent<Character> ().charName);
			jackPass.Add (jack.GetComponent<Character> ().password);

			jackLumber.Add(jack.GetComponent<Character> ().lumberCount);
			jackMoney.Add(jack.GetComponent<Character> ().money);


			jackWalkLevel.Add(jack.GetComponent<Character> ().lumberCount);
			jackBackpackLevel.Add(jack.GetComponent<Character> ().backpackLv);
			jackAxeSpeedLevel.Add(jack.GetComponent<Character> ().axeSpeedLv);
			jackAxePowerLevel.Add(jack.GetComponent<Character> ().axePowerLv);

			jackPos.Add (jack.transform.position);
			jackRotate.Add (Quaternion.ToEulerAngles(jack.transform.rotation));
		}

		PlayerPrefsX.SetStringArray ("JackNames", jackNames.ToArray ());
		PlayerPrefsX.SetStringArray ("JackPass", jackPass.ToArray ());

		PlayerPrefsX.SetIntArray ("JackLumber", jackLumber.ToArray ());
		PlayerPrefsX.SetIntArray ("JackMoney", jackMoney.ToArray ());


		PlayerPrefsX.SetIntArray ("JackWalkLevel", jackWalkLevel.ToArray ());
		PlayerPrefsX.SetIntArray ("JackBackpackLevel", jackBackpackLevel.ToArray ());
		PlayerPrefsX.SetIntArray ("JackSpeedLevel", jackAxeSpeedLevel.ToArray ());
		PlayerPrefsX.SetIntArray ("JackPowerLevel", jackAxePowerLevel.ToArray ());

		// next we do the lumberMills

		List<string> millNames = new List<string>();
		List<Vector3> millPos = new List<Vector3>();

		foreach (GameObject mill in GameObject.FindGameObjectsWithTag("LumberMill")) {
			millNames.Add (mill.GetComponent<LumberMillBehaviour> ().millName);
			millPos.Add (mill.transform.position);

		}

		PlayerPrefsX.SetStringArray ("MillNames", millNames.ToArray ());
		PlayerPrefsX.SetVector3Array ("MillPos", millPos.ToArray ());

		// save the trees!

		List<Vector3> treePos = new List<Vector3>();
		List<int> treeType = new List<int>();

		foreach (GameObject tree in GameObject.FindGameObjectsWithTag("Tree")) {
			treePos.Add (tree.transform.position);
			treeType.Add (tree.GetComponent<TreeBehaviour> ().type);
		}

		PlayerPrefsX.SetVector3Array ("TreePos", treePos.ToArray ());
		PlayerPrefsX.SetIntArray ("TreeType", treeType.ToArray ());
	}


}
