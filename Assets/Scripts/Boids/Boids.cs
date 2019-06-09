using UnityEngine;

// Boids class
public class Boids : MonoBehaviour
{
    public float neighborRadius;
    public float desiredSeparation;

    // Components
    public new Rigidbody rigidbody;

    // Boids manager
    private BoidsManager boidsManager;

    // Called before first frame update
    void Start()
    {
        // Components
        rigidbody = GetComponent<Rigidbody>();

        // Flock
        boidsManager = GetComponentInParent<BoidsManager>();

        // Initialise
        transform.position = new Vector3(Random.value * 200.0f, 0.5f, Random.value * 200.0f);
        rigidbody.velocity = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1);
    }

    // Called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
    }

    // Called once per frame
    void FixedUpdate()
    {
        BoundPosition();
    }

    // Keep in bounds
    private void BoundPosition()
    {
        if (transform.position.x > 200.0f)
        {
            transform.position = new Vector3(200.0f, transform.position.y, transform.position.z);
            rigidbody.velocity = new Vector3(-rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z);
        }

        if (transform.position.x < 0.0f)
        {
            transform.position = new Vector3(50.0f, transform.position.y, transform.position.z);
            rigidbody.velocity = new Vector3(-rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z);
        }

        if (transform.position.z > 200.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 200.0f);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, -rigidbody.velocity.z);
        }

        if (transform.position.z < 0.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 50.0f);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, -rigidbody.velocity.z);
        }

        if (transform.position.y > 1.5f)
        {
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -rigidbody.velocity.y, rigidbody.velocity.z);
        }

        if (transform.position.y < 0.5f)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -rigidbody.velocity.y, rigidbody.velocity.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        rigidbody.velocity = -rigidbody.velocity;
    }
}