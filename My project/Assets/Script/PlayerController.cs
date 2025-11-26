using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float FlySpeed = 20f;
    public float YawAmount = 120f;
    private float Yaw;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent physics from messing rotation
    }

    void FixedUpdate()
    {
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate direction and rotation
        Yaw += horizontalInput * YawAmount * Time.fixedDeltaTime;
        float pitch = Mathf.Lerp(0, 45, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll = Mathf.Lerp(0, 20, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        Quaternion targetRotation = Quaternion.Euler(Vector3.up * Yaw + Vector3.right * pitch + Vector3.forward * roll);

        // Apply rotation
        rb.MoveRotation(targetRotation);

        // Move forward using physics
        Vector3 move = transform.forward * FlySpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("Crashed into terrain!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
