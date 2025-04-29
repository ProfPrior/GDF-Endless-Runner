using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    public float speed = 5.0f;
    public float jumpForce = 50.0f;
    private float fallMultiplier = 2.5f;
    private Rigidbody rb;

    private bool isGrounded = true;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update() {
        // Automatically move the player forward
        // transform.Translate(Vector3.forward * Time.deltaTime * speed);
        // rb.linearVelocity = transform.forward * speed;

        Vector3 currentVelocity = rb.linearVelocity;
        currentVelocity.z = speed;
        rb.linearVelocity = currentVelocity;

        // Falle ich gerade?
        if(rb.linearVelocity.y < 0) {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}
