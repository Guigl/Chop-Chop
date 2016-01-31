using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    
    public int LevelArea = 100; // distance up/down/left/right at which scrolling stops

    public int ScrollArea = 25; // distance in margins to start scrolling
    public int ScrollSpeed = 25; // speed of margin scrolling
    public int DragSpeed = 100; // speed of middle-mouse dragging

    public int ZoomSpeed = 25; // speed of scroll-wheel zooming
    public int ZoomMin = 20; // min zoomable distance
    public int ZoomMax = 100; // max zoomable distance

    public int PanSpeed = 50; // speed of angle chance on close zoom
    public int PanAngleMin = 25; // min angle
    public int PanAngleMax = 80; // max angle

    public GameObject selectorObject;
    private Selector selector;

    private Camera _camera;

    public bool controlEnabled = true;


    void Start()
    {
        _camera = GetComponent<Camera>();
        selector = selectorObject.GetComponent<Selector>();
    }


    public float mouseRayDistance = 200f;

    // Update is called once per frame
    void Update()
    {
        if (controlEnabled)
        {
            // Init camera translation for this frame
            var translation = Vector3.zero;

            // Zoom in or out
            float zoomDelta = Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed * Time.deltaTime;
            if (zoomDelta != 0)
            {
                translation -= Vector3.up * ZoomSpeed * zoomDelta;
            }

            // Start panning camera if zooming in close to the ground or if just zooming out
            float pan = _camera.transform.eulerAngles.x - zoomDelta * PanSpeed;
            pan = Mathf.Clamp(pan, PanAngleMin, PanAngleMax);
            if (zoomDelta < 0 || _camera.transform.position.y < (ZoomMin + 30))
            {
                _camera.transform.eulerAngles = new Vector3(pan, 0, 0);
            }

            float ScaledScrollSpeed = ScrollSpeed + ScrollSpeed * (_camera.transform.position.y / (ZoomMax - ZoomMin));

            // Move camera with arrow keys
            translation += new Vector3(Input.GetAxis("Horizontal") * ScaledScrollSpeed / ScrollSpeed, 0, Input.GetAxis("Vertical") * ScaledScrollSpeed / ScrollSpeed);

            // Move camera with mouse
            if (Input.GetMouseButton(2)) // MMB
            {
                // Hold button and drag camera around
                translation -= new Vector3(Input.GetAxis("Mouse X") * DragSpeed * Time.deltaTime, 0,
                                   Input.GetAxis("Mouse Y") * DragSpeed * Time.deltaTime);
            }
            // move active lumberjack on rightclick
            else if (Input.GetMouseButtonDown(1))
            {
                //create a ray cast and set it to the mouses cursor position in game
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, mouseRayDistance))
                {
                    selector.activeLumberjack.GetComponent<Character>().wayPoint = new Vector3(hit.point.x, 0, hit.point.z);
                    selector.activeLumberjack.GetComponent<Character>().doing = Character.charStates.walking;
                }
            }
            else
            {
                // Move camera if mouse pointer reaches screen borders
                if (Input.mousePosition.x < ScrollArea)
                {
                    translation += Vector3.right * -1 * ScaledScrollSpeed * Time.deltaTime;
                }

                if (Input.mousePosition.x >= Screen.width - ScrollArea)
                {
                    translation += Vector3.right * ScaledScrollSpeed * Time.deltaTime;
                }

                if (Input.mousePosition.y < ScrollArea)
                {
                    translation += Vector3.forward * -1 * ScaledScrollSpeed * Time.deltaTime;
                }

                if (Input.mousePosition.y > Screen.height - ScrollArea)
                {
                    translation += Vector3.forward * ScaledScrollSpeed * Time.deltaTime;
                }
            }

            // Keep camera within level and zoom area
            Vector3 desiredPosition = _camera.transform.position + translation;
            if (desiredPosition.x < -LevelArea || LevelArea < desiredPosition.x)
            {
                translation.x = 0;
            }
            if (desiredPosition.y < ZoomMin || ZoomMax < desiredPosition.y)
            {
                translation.y = 0;
            }
            if (desiredPosition.z < -LevelArea || LevelArea < desiredPosition.z)
            {
                translation.z = 0;
            }

            // Finally move camera parallel to world axis
            _camera.transform.position += translation;
        }
    }
}
