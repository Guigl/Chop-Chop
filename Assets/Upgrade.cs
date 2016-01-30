using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Upgrade : MonoBehaviour {

	//This need not be constant. Could just have one upgrade
	//object and change the lumberjack upgrade costs/stats in the canvas
	public Selector lumberjackSelector;
	public LumberUI lumberUI;

	//Need some sort of interface for lumberjack information.
	//Linear increase in stats for now

	public float axePowerIncrement;
	public float bootsIncrement;
	public int backpackIncrement;

	private Button axePowerButton;
	private Button axeSpeedButton;
	private Button bootsButton;
	private Button backpackButton;
	private Button logoutButton;

	private Text axePowerCostText;
	private Text axeSpeedCostText;
	private Text bootsCostText;
	private Text backpackCostText;

	public float axePowerCostBase;
	public float axeSpeedCostBase;
	public float bootsCostBase;
	public float backpackCostBase;

	public void UpgradeItem () {
		
	}

	public void Logout ()
	{
		lumberUI.displayLogin();
	}

	int getCost(float currentStat, string type)
	{
		switch (type) 
		{
		case "axePower":
			break;
		default:
			break;
		}
		return 1;
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
			case "AxePower":
				axePowerButton = but;
				axePowerCostText = but.gameObject.GetComponentInChildren<Text> ();
				break;
			case "AxeSpeed":
				axeSpeedButton = but;
				axeSpeedCostText = but.gameObject.GetComponentInChildren<Text> ();
				break;
			case "Boots":
				bootsButton = but;
				bootsCostText = but.gameObject.GetComponentInChildren<Text> ();
				break;
			case "Backpack":
				backpackButton = but;
				bootsCostText = but.gameObject.GetComponentInChildren<Text> ();
				break;
			case "Logout":
				logoutButton = but;
				break;
			default:
				break;
			}
		}

		lumberUI = this.GetComponentInParent<LumberUI> ();
		lumberjackSelector = this.GetComponentInParent<LumberUI> ().lumberjackSelector.GetComponent<Selector>();

	}
	
	// Update is called once per frame
	void Update () {
		//change text depending on lumberjack's stats
		//axeCostText.text = ;
	}
}
