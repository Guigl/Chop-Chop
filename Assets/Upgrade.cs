﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Upgrade : MonoBehaviour {

	//This need not be constant. Could just have one upgrade
	//object and change the lumberjack upgrade costs/stats in the canvas
	public Selector lumberjackSelector;
	public LumberUI lumberUI;

	//active lumberjackCharacter
	private Character lumberjackCharacter;

	public int leaderBoardSize = 10;

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
	private Button lumberMillButton;

	private Text axePowerCostText;
	private Text axeSpeedCostText;
	private Text bootsCostText;
	private Text backpackCostText;
	private Text lumberMillCostText;

	private Text moneyText;
	private Text axePowerText;
	private Text axeSpeedText;
	private Text bootsText;
	private Text backpackText;
	private Text backpackLoadText;
	private Text backpackCapacityText;
	private Text leaderboardText;
	private Text leaderboardLumberText;

	public float axePowerCostBase = 1.5f;
	public float axeSpeedCostBase = 1.5f;
	public float bootsCostBase = 1.5f;
	public float backpackCostBase = 5f;
	public int lumberMillCost = 300;

    public bool spawnMillOnClick = false;
    public GameObject lumberMillPrefab;

	public void BuyLumberMill()
	{
		if (lumberjackCharacter.money >= lumberMillCost) 
		{
			lumberjackCharacter.money -= lumberMillCost;
			spawnMillOnClick = true;
		}
	}

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
		lumberjackSelector.activeLumberjack = null;
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
			return 100f / (99f + 10*level);
		case "boots":
			return level*0.5f +0.5f;
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
			case "LumberMillButton":
				lumberMillButton = but;
				lumberMillCostText = but.gameObject.GetComponentInChildren<Text> ();
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
			case "LeaderBoard":
				leaderboardText = text;
				break;
			case "LeaderBoardLumber":
				leaderboardLumberText = text;
				break;
			default:
				break;
			}
		}

		leaderboardText.text = "";
		leaderboardLumberText.text = "";


		lumberUI = this.GetComponentInParent<LumberUI> ();
		lumberjackSelector = this.GetComponentInParent<LumberUI> ().lumberjackSelector.GetComponent<Selector>();

	}
	
	// Update is called once per frame
	void Update () {
        if (lumberjackSelector.activeLumberjack)
        {
	
            lumberjackCharacter = lumberjackSelector.activeLumberjack.GetComponent<Character>();

			//upgrade costs
			axePowerCostText.text = "$ "+getCost("axePower", lumberjackCharacter.axePowerLv).ToString();
			axeSpeedCostText.text = "$ "+getCost("axeSpeed", lumberjackCharacter.axeSpeedLv).ToString();
			bootsCostText.text = "$ "+getCost("boots", lumberjackCharacter.walkLv).ToString();
			backpackCostText.text = "$ "+getCost("backpack", lumberjackCharacter.backpackLv).ToString();

			lumberMillCostText.text = "$ " + lumberMillCost;

			//stats
            moneyText.text = "$ "+lumberjackCharacter.money.ToString();
			axePowerText.text = "LV. "+lumberjackCharacter.axePowerLv.ToString();
			axeSpeedText.text = "LV. "+lumberjackCharacter.axeSpeedLv.ToString();
			bootsText.text = "LV. "+lumberjackCharacter.walkLv.ToString();
			backpackText.text = "LV. "+lumberjackCharacter.backpackSize.ToString();
			backpackLoadText.text = lumberjackCharacter.backpack.Count.ToString() + " / " + 
									lumberjackCharacter.backpackSize.ToString();

			//leaderboard
			if (lumberjackSelector.lumberjacks.Count > 0) 
			{
				/*
				lumberjackSelector.lumberjacks.Sort (delegate(GameObject a, GameObject b) 
				{
						int val = (b.GetComponent<Character>().lumberCount).CompareTo(a.GetComponent<Character>().lumberCount);
						if (val==0) val=1;
						return val;
				});
				*/

				lumberjackSelector.lumberjacks = lumberjackSelector.lumberjacks.OrderByDescending(x=>x.GetComponent<Character>().lumberCount).ToList();
			}

			leaderboardText.text = "";
			leaderboardLumberText.text = "";
			bool activeInLeaderBoard = false;
			for (int i=0; i<leaderBoardSize && i<lumberjackSelector.lumberjacks.Count; i++) 
			{
				GameObject curLumberjack = lumberjackSelector.lumberjacks [i];
				Character curCharacter = curLumberjack.GetComponent<Character> ();
				if (curLumberjack == lumberjackSelector.activeLumberjack) activeInLeaderBoard = true; 
				string lumberjackName = curCharacter.charName;
				int lumberAmount = curCharacter.lumberCount;

				leaderboardText.text += (i+1).ToString () + ". " + lumberjackName + '\n';
				leaderboardLumberText.text += lumberAmount.ToString() + '\n';


			} 

			if (!activeInLeaderBoard) 
			{
				GameObject curLumberjack = lumberjackSelector.activeLumberjack;
				Character curCharacter = curLumberjack.GetComponent<Character> ();
				string lumberjackName = curCharacter.charName;
				int lumberAmount = curCharacter.lumberCount;
				int rank = lumberjackSelector.lumberjacks.IndexOf (curLumberjack) + 1;

				leaderboardText.text += rank.ToString () + ". " + lumberjackName + '\n';
				leaderboardLumberText.text += lumberAmount.ToString() + '\n';
			}

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
					Debug.Log (lumberjackCharacter.charName + " built a lumbermill!");
					lumberMill.GetComponent<LumberMillBehaviour> ().setName (lumberjackCharacter.charName);
                    spawnMillOnClick = false;
                }
            }
        }
    }
}
