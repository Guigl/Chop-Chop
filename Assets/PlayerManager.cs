using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public Selector lumberjackSelector;

	public InputField username;
	public InputField password;
	public Text error;
	public Button loginButton;

	public string wrongUsername;
	public string wrongPassword;

	public void Login () {
		GameObject lumberjack = lumberjackSelector.findLumberjack (username.text);
		if (lumberjack == null) {
			Debug.Log ("WrongUsername");
			error.text = wrongUsername;
		}

		//Add password checking


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
			case "Login":
				loginButton = but;
				break;
			default:
				break;
			}
		}

		InputField[] fields = this.GetComponentsInChildren<InputField> ();
		foreach (InputField field in fields) 
		{
			string name = field.gameObject.name;
			switch (name) 
			{
			case "Username":
				username = field;
				break;
			case "Password":
				password = field;
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
			case "Error":
				error = text;
				break;
			default:
				break;
			}
		}

		lumberjackSelector = this.GetComponentInParent<LumberUI> ().lumberjackSelector.GetComponent<Selector>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
