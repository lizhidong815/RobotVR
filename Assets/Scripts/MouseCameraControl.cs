using UnityEngine;

public class MouseCameraControl : MonoBehaviour
{

    public float horizontalLookSens = 5.0f;
    public float verticalLookSens = 5.0f;

    public float sidewaysPanSens = 5.0f;
    public float forwardsPanSens = 5.0f;

    public float zoomSens = 5.0f;

    private void Update()
    {
        // Left Click : Free look
        if (Input.GetMouseButton(0))
        {
            float lookH = transform.localEulerAngles.y + horizontalLookSens * Input.GetAxis("Mouse X");
            float lookV = transform.localEulerAngles.x - verticalLookSens * Input.GetAxis("Mouse Y");
            transform.localEulerAngles = new Vector3(lookV, lookH);
        }

        // Right Click : Pan 
        if (Input.GetMouseButton(1))
        {
            float currentY = transform.position.y;
            transform.Translate(new Vector3(sidewaysPanSens * Input.GetAxis("Mouse X"), 0, forwardsPanSens * Input.GetAxis("Mouse Y")), Space.Self);
            transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
        }

        // Scrollwheel : Zoom
        transform.position += transform.forward * zoomSens * Input.GetAxis("Mouse ScrollWheel");
        if (transform.position.y < 0)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}