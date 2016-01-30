using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Upgrade : MonoBehaviour {

	//This need not be constant. Could just have one upgrade
	//object and change the lumberjack upgrade costs/stats in the canvas
	public GameObject lumberjackSelector;

	//Need some sort of interface for lumberjack information.
	//Linear increase in stats for now

	public float axeIncrement;
	public float bootsIncrement;
	public int backpackIncrement;

	private Button axeButton;
	private Button bootsButton;
	private Button backpackButton;


	void UpgradeItem () {
		//use lumberjackSelector to get lumberjack/required stats

	}

	// Use this for initialization
	void Start () 
	{
		//Sets up the buttons
		Button[] buttons = this.GetComponentsInChildren<Button> ();
		foreach (Button but in buttons) 
		{
			string name = but.gameObject.name;
			switch (name)
			{
			case "Axe":
				axeButton = but;
				break;
			case "Boots":
				bootsButton = but;
				break;
			case "Backpack":
				backpackButton = but;
				break;
			default:
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
