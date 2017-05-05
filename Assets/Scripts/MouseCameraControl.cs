using UnityEngine;

public class MouseCameraControl : MonoBehaviour
{

    public float horizontalLookSens = 5.0f;
    public float verticalLookSens = 5.0f;

    public float sidewaysPanSens = 1.0f;
    public float forwardsPanSens = 1.0f;

    public float zoomSens = 5.0f;

	Vector3 mousePos;
	Plane backPlane;

    private void Update()
    {
        if (UIManager.instance.windowOpen == false)
        {
			// Left Click : selection 
			if (Input.GetMouseButton(0))
			{
				
			}

			// Right Click : Free look
            if (Input.GetMouseButton(1))
            {
                float lookH = transform.localEulerAngles.y + horizontalLookSens * Input.GetAxis("Mouse X");
                float lookV = transform.localEulerAngles.x - verticalLookSens * Input.GetAxis("Mouse Y");
                transform.localEulerAngles = new Vector3(lookV, lookH);
            }


			// middle click : startPan 
			if (Input.GetMouseButtonDown(2)) {
				mousePos = Input.mousePosition;
				backPlane = new Plane(-1 * Camera.main.transform.forward, getPlanePos(mousePos, new Plane(Vector3.up,Vector3.zero)));
			}

			// middle held : Pan 
			if (Input.GetMouseButton(2))
			{
				Vector3 newMousePos = Input.mousePosition;
				if (mousePos == newMousePos)
					return;
				Camera.main.transform.parent.Translate (getPlanePos (mousePos, backPlane) - getPlanePos (newMousePos, backPlane));
				mousePos = newMousePos;

				//float currentY = transform.position.y;
				//transform.Translate(new Vector3(-1 * sidewaysPanSens * Input.GetAxis("Mouse X"), 0, -1 * forwardsPanSens * Input.GetAxis("Mouse Y")), Space.Self);
				//transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
			}

            // Scrollwheel : Zoom
            transform.position += transform.forward * zoomSens * Input.GetAxis("Mouse ScrollWheel");
            if (transform.position.y < 0)
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
	}

	Vector3 getPlanePos(Vector3 mousepos, Plane plane){
		Ray ray = Camera.main.ScreenPointToRay(mousepos);
		float distance = 0; 
		if (plane.Raycast(ray, out distance)){
			return ray.GetPoint(distance);
		}
		return Vector3.zero;
	}
}