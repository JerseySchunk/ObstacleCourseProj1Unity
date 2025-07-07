using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Transform cam;
    public float mouseSensitivity = 3f;
    public float verticalClampMin = -30f;
    public float verticalClampMax = 60f;

    [Header("Camera Offset")]
    public Vector3 cameraOffset = new Vector3(0f, 1.5f, -4f); // X: side, Y: up, Z: back

    private float rotX = 0f; // vertical rotation
    private float rotY = 0f; // horizontal rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rotY = transform.eulerAngles.y;
        rotX = transform.eulerAngles.x;
    }

    // 7/7/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

void LateUpdate()
{
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

    rotY += mouseX;
    rotX -= mouseY;
    rotX = Mathf.Clamp(rotX, verticalClampMin, verticalClampMax);

    // Apply rotation to the camera pivot
    transform.rotation = Quaternion.Euler(rotX, rotY, 0f);

    // Position the pivot at the player's position plus offset
    transform.position = target.position + transform.rotation * cameraOffset;

    // Ensure the camera looks at the player
    cam.LookAt(target.position + Vector3.up * 1.5f); // Adjust the height as needed
}
}

