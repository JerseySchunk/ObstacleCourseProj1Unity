using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 3f;
    public Transform player;

    private float rotationY = 0f;

    void Start()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;

        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);

        if (player != null)
        {
            transform.position = player.position + Vector3.up * 1.5f;
        }
    }
}
