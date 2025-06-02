using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    public float speed = 5.0f;
    public float jumpForce = 50.0f;
    private float fallMultiplier = 2.5f;
    private Rigidbody rb;

    private bool isGrounded = true;

    float[] lanes = {-2.0f, 0.0f, 2.0f}; // Positionen der Bahnen
    int currentLane = 1; // Aktuelle Bahn
    float laneSwitchSpeed = 100.0f; // Geschwindigkeit des Bahnwechsels

    public GameObject deathParticlesPrefab;

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

        // Bahnwechsel LINKS
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0) {
            currentLane--;
        }

        // Bahnwechsel RECHTS
        if (Input.GetKeyDown(KeyCode.D) && currentLane < lanes.Length - 1) {
            currentLane++;
        }

        Vector3 targetPosition = new Vector3(lanes[currentLane], transform.position.y, transform.position.z);
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, laneSwitchSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Obstacle")) {
            Debug.Log("Game Over");
            Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}
