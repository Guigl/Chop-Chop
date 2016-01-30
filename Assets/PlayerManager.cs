using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public Selector lumberjackSelector;
	public LumberUI lumberUI;

	public InputField username;
	public InputField password;
	public Text error;
	public Button loginButton;

	public string wrongUsername;
	public string wrongPassword;

	void clearFields()
	{
		username.text = "";
		password.text = "";
		error.text = "";
	}


	public void Login () {
		GameObject lumberjack = lumberjackSelector.findLumberjack (username.text);
		if (lumberjack == null) {
			Debug.Log ("WrongUsername");
			error.text = wrongUsername;
			return;
		}
		Character lumberjackCharacter = lumberjack.GetComponent<Character> ();

		if (lumberjackCharacter.password != password.text) {
			error.text = wrongPassword;
			return;
		}

		lumberjackSelector.makeActiveLumberjack (lumberjack);
		clearFields ();
		lumberUI.displayPlay ();

	}

	public void CreateCharacter() {
		clearFields ();
		lumberUI.displayCreate ();
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

		lumberUI = this.GetComponentInParent<LumberUI> ();
		lumberjackSelector = this.GetComponentInParent<LumberUI> ().lumberjackSelector.GetComponent<Selector>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
