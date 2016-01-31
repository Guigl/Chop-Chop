using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	GameObject cam;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");

	}
	
	void Update () {
		transform.LookAt(cam.transform.position);
		Vector3 rotationVector = transform.rotation.eulerAngles;
		rotationVector.z = 0;
		rotationVector.y = 180;
		transform.rotation = Quaternion.Euler (rotationVector);
	}
}
