using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

	private Text moneyText;
	private Text axePowerText;
	private Text axeSpeedText;
	private Text bootsText;
	private Text backpackText;
	private Text backpackLoadText;
	private Text backpackCapacityText;

	public float axePowerCostBase = 1.5f;
	public float axeSpeedCostBase = 1.5f;
	public float bootsCostBase = 1.5f;
	public float backpackCostBase = 5f;

    public bool spawnMillOnClick = false;
    public GameObject lumberMillPrefab;

	//Super hacky zoom to lumberjack
	public void zoomToLumberjack()
	{
		lumberjackSelector.makeActiveLumberjack (lumberjackSelector.activeLumberjack);
	}

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
				lumberjackCharacter.axeSpeed = getStat (type, lumberjackCharacter.axeSpeedLv);
			}
			break;
		case "boots":
			level = lumberjackCharacter.walkLv;
			cost = getCost (type, level);
			if (lumberjackCharacter.money >= cost) 
			{
				lumberjackCharacter.money -= cost;
				lumberjackCharacter.walkLv++;
				lumberjackCharacter.walkSpeed = getStat (type, lumberjackCharacter.walkLv);
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
			return level*2f;
		case "axeSpeed":
			return 100f / (99f + level);
		case "boots":
			return level*1.5f;
		case "backpack":
			return level;
		default:
			return -1;
		}
	}


    private float mouseRayDistance = Mathf.Infinity;

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
			case "AxePowerButton":
				axePowerButton = but;
				axePowerCostText = but.gameObject.GetComponentInChildren<Text> ();
				break;
			case "AxeSpeedButton":
				axeSpeedButton = but;
				axeSpeedCostText = but.gameObject.GetComponentInChildren<Text> ();
				break;
			case "BootsButton":
				bootsButton = but;
				bootsCostText = but.gameObject.GetComponentInChildren<Text> ();
				break;
			case "BackpackButton":
				backpackButton = but;
				backpackCostText = but.gameObject.GetComponentInChildren<Text> ();
				break;
			case "LogoutButton":
				logoutButton = but;
				break;
			default:
				break;
			}
		}


		Text[] texts = this.GetComponentsInChildren<Text> ();
		foreach (Text text in texts) 
		{
			string name = text.gameObject.name;
			switch (name)
			{
			case "Money":
				moneyText = text;
				break;
			case "AxePowerLv":
				axePowerText = text;
				break;
			case "AxeSpeedLv":
				axeSpeedText = text;
				break;
			case "BootsLv":
				bootsText = text;
				break;
			case "BackpackLv":
				backpackText = text;
				break;
			case "BackpackLoad":
				backpackLoadText = text;
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
        if (lumberjackSelector.activeLumberjack)
        {
            lumberjackCharacter = lumberjackSelector.activeLumberjack.GetComponent<Character>();
			axePowerCostText.text = "$ "+getCost("axePower", lumberjackCharacter.axePowerLv).ToString();
			axeSpeedCostText.text = "$ "+getCost("axeSpeed", lumberjackCharacter.axeSpeedLv).ToString();
			bootsCostText.text = "$ "+getCost("boots", lumberjackCharacter.walkLv).ToString();
			backpackCostText.text = "$ "+getCost("backpack", lumberjackCharacter.backpackLv).ToString();

            moneyText.text = "$ "+lumberjackCharacter.money.ToString();
			axePowerText.text = "LV. "+lumberjackCharacter.axePowerLv.ToString();
			axeSpeedText.text = "LV. "+lumberjackCharacter.axeSpeedLv.ToString();
			bootsText.text = "LV. "+lumberjackCharacter.walkLv.ToString();
			backpackText.text = "LV. "+lumberjackCharacter.backpackSize.ToString();
			backpackLoadText.text = lumberjackCharacter.backpack.Count.ToString() + " / " + 
									lumberjackCharacter.backpackSize.ToString();
        }


        //add display level to buttons

        //spawn lumber mill
        if (spawnMillOnClick)
        {
            // on click
            if (Input.GetMouseButtonDown(0))
            {
                //create a ray cast and set it to the mouses cursor position in game
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, mouseRayDistance))
                {
                    GameObject lumberMill = Instantiate(lumberMillPrefab);
                    LumberMillBehaviour millBehaviour = lumberMill.GetComponent<LumberMillBehaviour>();
                    millBehaviour.setName(lumberjackSelector.activeLumberjack.name);
                    lumberMill.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                    spawnMillOnClick = false;
                }
            }
        }
    }
}
