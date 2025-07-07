using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float accelerationRate = 8f;
    public float decelerationRate = 200f;

    private Rigidbody rb;
    private Vector3 inputDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get raw WASD input
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Get camera-relative direction
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        inputDirection = (camForward * v + camRight * h).normalized;
    }

    void FixedUpdate()
{
    Vector3 currentVelocity = rb.linearVelocity;

    Vector3 horizontalCurrent = new Vector3(currentVelocity.x, 0, currentVelocity.z);
    Vector3 horizontalTarget;

    if (inputDirection != Vector3.zero)
    {
        horizontalTarget = new Vector3(inputDirection.x * moveSpeed, 0, inputDirection.z * moveSpeed);

        // Smooth acceleration
        Vector3 adjustedVelocity = Vector3.MoveTowards(horizontalCurrent, horizontalTarget, accelerationRate * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector3(adjustedVelocity.x, currentVelocity.y, adjustedVelocity.z);
    }
    else
    {
        // Smooth deceleration to zero
        Vector3 adjustedVelocity = Vector3.MoveTowards(horizontalCurrent, Vector3.zero, decelerationRate * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector3(adjustedVelocity.x, currentVelocity.y, adjustedVelocity.z);
    }

    // Rotate player to match camera forward
    Vector3 lookDirection = Camera.main.transform.forward;
    lookDirection.y = 0f;

    if (lookDirection.sqrMagnitude > 0.01f)
    {
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime);
    }
}

}


