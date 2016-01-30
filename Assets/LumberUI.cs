using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LumberUI : MonoBehaviour {

	//This need not be constant. Could just have one upgrade
	//object and change the lumberjack upgrade costs/stats in the canvas
	public GameObject lumberjackSelector;

	public Canvas playCanvas;
	public Canvas loginCanvas;
	public Canvas createCanvas;


	public void displayPlay() 
	{
		loginCanvas.enabled = false;
		playCanvas.enabled = true;
		createCanvas.enabled = false;
	}

	public void displayLogin()
	{
		loginCanvas.enabled = true;
		playCanvas.enabled = false;
		createCanvas.enabled = false;
	}

	public void displayCreate()
	{
		loginCanvas.enabled = false;
		playCanvas.enabled = false;
		createCanvas.enabled = true;
	}

	// Use this for initialization
	void Start () 
	{
		//Sets up the buttons
		Canvas[] canvases = this.GetComponentsInChildren<Canvas> ();
		foreach (Canvas canvas in canvases) 
		{
			string name = canvas.gameObject.name;
			switch (name)
			{
			case "Play Canvas":
				playCanvas = canvas;
				break;
			case "Login Canvas":
				loginCanvas = canvas;
				break;
			case "create Canvas":
				createCanvas = canvas;
				break;
			default:
				break;
			}
		}

		displayLogin ();
	}

	// Update is called once per frame
	void Update () {
		//change text depending on lumberjack's stats
		//axeCostText.text = ;
	}
}
