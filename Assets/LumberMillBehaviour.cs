using UnityEngine;
using System.Collections;

public class LumberMillBehaviour : MonoBehaviour {
	public string millName = "LumberJack's LumberMill";

	void setName (string s) {
		name = "Lumber" + s + "'s LumberMill";
		transform.GetComponentInChildren<TextMesh>().text = millName;
	}
}
