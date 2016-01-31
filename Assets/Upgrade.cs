using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Upgrade : MonoBehaviour {

	//This need not be constant. Could just have one upgrade
	//object and change the lumberjack upgrade costs/stats in the canvas
	public Selector lumberjackSelector;
	public LumberUI lumberUI;

	private Character lumberjackCharacter;

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

	public float axePowerCostBase = 1.5f;
	public float axeSpeedCostBase = 1.5f;
	public float bootsCostBase = 1.5f;
	public float backpackCostBase = 3f;

	public void UpgradeItem (string type) {
		int level, cost;
		switch (type) 
		{
		case "axePower":
			level = lumberjackCharacter.axePowerLv;
			cost = getCost (type, level);
			if (lumberjackCharacter.money >= cost) 
			{
				lumberjackCharacter.money -= cost;
				lumberjackCharacter.axePowerLv++;
				lumberjackCharacter.axePower = Mathf.CeilToInt(getStat (type, lumberjackCharacter.axePowerLv));
			}
			break;
		case "axeSpeed":
			level = lumberjackCharacter.axeSpeedLv;
			cost = getCost (type, level);
			if (lumberjackCharacter.money >= cost) 
			{
				lumberjackCharacter.money -= cost;
				lumberjackCharacter.axeSpeedLv++;
				lumberjackCharacter.axeSpeed = Mathf.CeilToInt(getStat (type, lumberjackCharacter.axeSpeedLv));
			}
			break;
		case "boots":
			level = lumberjackCharacter.walkLv;
			cost = getCost (type, level);
			if (lumberjackCharacter.money >= cost) 
			{
				lumberjackCharacter.money -= cost;
				lumberjackCharacter.walkLv++;
				lumberjackCharacter.walkSpeed = Mathf.CeilToInt(getStat (type, lumberjackCharacter.walkLv));
			}
			break;
		case "backpack":
			level = lumberjackCharacter.backpackLv;
			cost = getCost (type, level);
			if (lumberjackCharacter.money >= cost) 
			{
				lumberjackCharacter.money -= cost;
				lumberjackCharacter.backpackLv++;
				lumberjackCharacter.backpackSize = Mathf.CeilToInt(getStat (type, lumberjackCharacter.backpackLv));
			}
			break;
		default:
			break;
		}
	}

	public void Logout ()
	{
		lumberUI.displayLogin();
	}

	int getCost(string type, int level)
	{
		switch (type) 
		{
		case "axePower":
			return Mathf.CeilToInt (Mathf.Pow(axePowerCostBase,level));
			break;
		case "axeSpeed":
			return Mathf.CeilToInt (Mathf.Pow(axeSpeedCostBase,level));
			break;
		case "boots":
			return Mathf.CeilToInt (Mathf.Pow(bootsCostBase,level)); 
			break;
		case "backpack":
			return Mathf.CeilToInt (Mathf.Pow(backpackCostBase,level));
			break;
		default:
			return -1;
		}
	}

	float getStat(string type, int level)
	{
		switch (type) 
		{
		case "axePower":
			return level;
		case "axeSpeed":
			return 100 / (99 + level);
		case "boots":
			return level;
		case "backpack":
			return level;
		default:
			return -1;
		}
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
				backpackCostText = but.gameObject.GetComponentInChildren<Text> ();
				break;
			case "Logout":
				logoutButton = but;
				break;
			default:
				break;
			}
		}

		lumberUI = this.GetComponentInParent<LumberUI> ();
		lumberjackSelector = lumberUI.lumberjackSelector.GetComponent<Selector>();

	}
	
	// Update is called once per frame
	void Update () {
        if (lumberjackSelector.activeLumberjack)
        {
            lumberjackCharacter = lumberjackSelector.activeLumberjack.GetComponent<Character>();
            axePowerCostText.text = getCost("axePower", lumberjackCharacter.axePowerLv).ToString();
            axeSpeedCostText.text = getCost("axeSpeed", lumberjackCharacter.axeSpeedLv).ToString();
            bootsCostText.text = getCost("boots", lumberjackCharacter.walkLv).ToString();
            backpackCostText.text = getCost("backpack", lumberjackCharacter.backpackLv).ToString();
        }
		//add display level to buttons
	}
}
