using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : MonoBehaviour {

	public List<GameObject> lumberjacks;
	public GameObject activeLumberjack;
    public GameObject cameraObject;

	public GameObject findLumberjack (string name) {
		foreach (GameObject lj in lumberjacks) {
			Character curCharacter = lj.GetComponent<Character> ();
			if (curCharacter.charName == name) {
				return lj; 
			}
		}
		return null;
	}

    private bool zoomToLumberjack = false;
    public float zoomThreshold = 200f;

    public void makeActiveLumberjack (GameObject lj) {
        if (lj.GetComponent<Character>())
        {
            activeLumberjack = lj;
            zoomToActiveLumberjack();
            /*float distanceFromCamera = Vector3.Distance(activeLumberjack.transform.position, cameraObject.transform.position);
            if (distanceFromCamera > zoomThreshold)
            {
                zoomToLumberjack = true;
            }
            else
            {
                zoomToLumberjack = false;
            }*/
        }
        else
        {
            Debug.Log("GameObject given to makeActiveLumberjack was not a lumberjack and that's not okay");
        }
	}

    public void zoomToActiveLumberjack()
    {
        zoomToLumberjack = true;
        /*float new_x = activeLumberjack.transform.position.x;
        float new_z = activeLumberjack.transform.position.z - cameraObject.transform.position.y / Mathf.Tan(Mathf.Deg2Rad*cameraObject.transform.eulerAngles.x);
        Vector3 newPosition = new Vector3(new_x, cameraObject.transform.position.y, new_z);
        cameraObject.transform.position = newPosition;*/
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

    private float velocity_x;
    private float velocity_z;

    // Update is called once per frame
    void Update () {
        if (zoomToLumberjack)
        {
            float new_x = Mathf.SmoothDamp(cameraObject.transform.position.x, activeLumberjack.transform.position.x, ref velocity_x, 1f);
            float new_z = Mathf.SmoothDamp(cameraObject.transform.position.z, activeLumberjack.transform.position.z - cameraObject.transform.position.y / Mathf.Tan(Mathf.Deg2Rad*cameraObject.transform.eulerAngles.x), ref velocity_z, 1f);
            cameraObject.transform.position = new Vector3(new_x, cameraObject.transform.position.y, new_z);
            if (Mathf.Abs(cameraObject.transform.position.z - (activeLumberjack.transform.position.z - cameraObject.transform.position.y / Mathf.Tan(Mathf.Deg2Rad*cameraObject.transform.eulerAngles.x))) < 5)
            {
                zoomToLumberjack = false;
            }
        }
    }
}
