using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float speed = 50f;
    public float zoomSpeed = 20f;
    public float minHeight = 0f;
    public float maxHeight = 1000f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        float scaledZoomFactor = 100 * transform.position.y / (maxHeight - minHeight);
        transform.Translate(0, -scroll * zoomSpeed * scaledZoomFactor, scroll * zoomSpeed * scaledZoomFactor, Space.World);
    }
}
