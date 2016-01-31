using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Creation : MonoBehaviour {

	public Selector lumberjackSelector;
	public LumberUI lumberUI;

	public GameObject lumberjackBase;

    private GameObject lumberjackDummy;
    public float dummyDistance = 5f;

	public InputField username;
	public InputField password;
	public InputField passwordRepeat;
	public Button createButton;
	public Button cancelButton;
	public Text error;

	public float walkSpeed = 1.0f;
	public float axeSpeed = 1.0f; // time it takes to swing the axe in seconds
	public int axePower = 1;

	public int backpackSize = 1;
	public int money = 0;
	public int lumberCount = 0;

    public Material[] customMaterials;

	public Vector3 spawnPoint;
    private bool spawnOnClick;

	//TODO: add colour custimization

	void clearFields()
	{
		username.text = "";
		password.text = "";
		passwordRepeat.text = "";
		error.text = "";
	}

	public void Cancel()
	{
		clearFields ();
		lumberUI.displayLogin ();
        lumberjackSelector.cameraObject.GetComponent<CameraController>().controlEnabled = true;
	}

    public void CreateLumberjackDummy()
    {
        // create lumberjack dummy in front of camera
        lumberjackDummy = Instantiate(lumberjackBase);
        Vector3 newPosition = new Vector3();
        Vector3 cameraPosition = lumberjackSelector.cameraObject.transform.position;
        float theta = Mathf.Deg2Rad*(lumberjackSelector.cameraObject.transform.eulerAngles.x+5);
        newPosition.x = cameraPosition.x;
        newPosition.y = cameraPosition.y - dummyDistance * Mathf.Sin(theta);
        newPosition.z = cameraPosition.z + dummyDistance * Mathf.Cos(theta);
        lumberjackDummy.transform.position = newPosition;
        Vector3 newAngles = new Vector3(-Mathf.Rad2Deg*theta, 180, 0);
        lumberjackDummy.transform.eulerAngles = newAngles;
        lumberjackSelector.cameraObject.GetComponent<CameraController>().controlEnabled = false;
    }

	public void CreateLumberjack()
	{
		if (lumberjackSelector.findLumberjack (username.text)) 
		{
			error.text = "Name is already taken";
			return;
		}

		if (password.text != passwordRepeat.text) 
		{
			error.text = "Passwords don't match";
			return;
		}
        
        Character lumberjackCharacter = lumberjackDummy.GetComponent<Character>();
        lumberjackCharacter.loadCharacter(username.text, password.text, walkSpeed, axeSpeed, axePower, backpackSize, money, lumberCount);
        lumberjackDummy.transform.position = spawnPoint;

        clearFields();
        lumberUI.displayPlay();
        lumberjackSelector.cameraObject.GetComponent<CameraController>().controlEnabled = true;

        spawnOnClick = true;
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
			case "CreateButton":
				createButton = but;
				break;
			case "CancelButton":
				cancelButton = but;
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
			case "PasswordRepeat":
				passwordRepeat = field;
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

    private float mouseRayDistance = Mathf.Infinity;

    // Update is called once per frame
    void Update () {
        if (spawnOnClick)
        {
            // on click
            if(Input.GetMouseButtonDown(0)) {
                //create a ray cast and set it to the mouses cursor position in game
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, mouseRayDistance))
                {
                    lumberjackDummy.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                    lumberjackSelector.addLumberjack(lumberjackDummy);
                    lumberjackSelector.makeActiveLumberjack(lumberjackDummy);
                    lumberjackDummy.GetComponent<Character>().doing = Character.charStates.readyForWork;
                    spawnOnClick = false;
                }
            }
        }
	}
}
