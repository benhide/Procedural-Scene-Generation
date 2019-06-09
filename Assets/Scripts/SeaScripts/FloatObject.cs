using UnityEngine;

// Float an object class
public class FloatObject : MonoBehaviour
{
    // The floating points
    public Transform[] floatPoints;

    // The floating strength
    public float floatStrength = 1.0f;

    // The floating dampening
    public float floatDampening = 0.5f;

    // The ridigbody
    private new Rigidbody rigidbody;

    // The water level
    public float waterLevel;

    // Called before the first update frame
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(new Vector3(Random.value * 20.0f - 10.0f, 0.0f, Random.value * 20.0f - 10.0f));
    }

    // Called once per frame
    void FixedUpdate()
    {
        // Go through th floating points
        foreach (Transform floatPoint in floatPoints)
        {
            // Calculate the float force
            float floatForceFactor = 1.0f - (floatPoint.position.y - waterLevel) * floatStrength;

            // If the floating point needs to float
            if (floatForceFactor > 0.0f)
            {
                // Get the bouyancy effect
                Vector3 floatLift = -Physics.gravity * (floatForceFactor - rigidbody.velocity.y) / floatPoints.Length;

                // Add bouyancy to the rigidbody
                rigidbody.AddForceAtPosition(floatLift, floatPoint.position);
            }
        }

        // Set the angular drag
        rigidbody.angularDrag = floatDampening;
    }
}