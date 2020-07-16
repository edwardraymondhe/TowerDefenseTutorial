using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;

    public float panSpeed = 4f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 4f;
    public float minY = 10f;
    public float maxY = 80f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;

        if (!doMovement)
            return;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            // Vector3.forward = new Vector3(0f, 0f, 1f) * panSpeed
            transform.Translate(Vector3.forward * panSpeed, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            // Vector3.forward = new Vector3(0f, 0f, 1f) * panSpeed
            transform.Translate(Vector3.back * panSpeed, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            // Vector3.forward = new Vector3(0f, 0f, 1f) * panSpeed
            transform.Translate(Vector3.right * panSpeed, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            // Vector3.forward = new Vector3(0f, 0f, 1f) * panSpeed
            transform.Translate(Vector3.left * panSpeed, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        // Debug.Log(scroll);
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);


        transform.position = pos;

    }
}
