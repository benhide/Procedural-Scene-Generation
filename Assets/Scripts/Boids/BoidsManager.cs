using UnityEngine;

// THe boids manager
public class BoidsManager : MonoBehaviour
{
    // The boids
    public Boids boid;

    // NUmber of boids
    public int numberOfBoids;

    // Boid control vars
    public float alignmentFactor;
    public float cohesionFactor;
    public float separationFactor;

    // Limit updates
    private int updateCount = 0;

    // Array of boids
    private Boids[] boids;

    // Called before first frame update
    void Start()
    {
        // Initialise
        boids = new Boids[numberOfBoids];

        // Set eth boids
        for (int i = 0; i < numberOfBoids; i++)
        {
            boids[i] = Instantiate(boid, transform.position, Quaternion.identity) as Boids;
            boids[i].transform.parent = transform;
        }
    }

    // Called once per frame
    void FixedUpdate()
    {
        // Limit updates
        updateCount++;
        if (updateCount > 20)
        {
            // Loop through boids
            for (int i = 0; i < numberOfBoids; i++)
            {
                // Get teh boid
                Boids boid = boids[i];

                // If it exsists and has a rigidbody
                if (boid != null && boid.rigidbody != null)
                {
                    Vector3 alignment = Alignment(boid) * alignmentFactor * Time.deltaTime;
                    Vector3 cohesion = Cohesion(boid) * cohesionFactor * Time.deltaTime;
                    Vector3 separation = Separation(boid) * separationFactor * Time.deltaTime;

                    boid.rigidbody.velocity += (alignment + cohesion + separation);
                }
            }

            // Reset the count
            updateCount = 0;
        }
    }

    // Alignment
    private Vector3 Alignment(Boids boid)
    {
        Vector3 velocity = Vector3.zero;
        int count = 0;
        for (int i = 0; i < numberOfBoids; i++)
        {
            float distance = Vector3.Distance(boids[i].transform.localPosition, boid.transform.localPosition);
            if (distance > 0 && distance < boid.neighborRadius)
            {
                velocity += boids[i].rigidbody.velocity;
                count++;
            }
        }
        if (count > 0) return (velocity / (numberOfBoids - 1)).normalized;
        else return Vector3.zero;
    }

    // Cohesion
    private Vector3 Cohesion(Boids boid)
    {
        Vector3 centerOfMass = Vector3.zero;
        int count = 0;
        for (int i = 0; i < numberOfBoids; i++)
        {
            float distance = Vector3.Distance(boids[i].transform.localPosition, boid.transform.localPosition);
            if (distance > 0 && distance < boid.neighborRadius)
            {
                centerOfMass += boids[i].transform.localPosition;
                count++;
            }
        }
        if (count > 0) return ((centerOfMass / (numberOfBoids - 1)) - boid.transform.localPosition).normalized;
        else return Vector3.zero;
    }

    // Seperation
    private Vector3 Separation(Boids boid)
    {
        Vector3 velocity = Vector3.zero;
        int count = 0;
        for (int i = 0; i < numberOfBoids; i++)
        {
            float distance = Vector3.Distance(boids[i].transform.localPosition, boid.transform.localPosition);
            if (distance > 0 && distance < boid.desiredSeparation)
            {
                velocity -= (boids[i].transform.localPosition - boid.transform.localPosition).normalized / distance;
                count++;
            }
        }
        if (count > 0) return (velocity / (numberOfBoids - 1)).normalized;
        else return Vector3.zero;
    }
}