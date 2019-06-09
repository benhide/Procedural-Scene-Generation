using UnityEngine;

// Wave generator
public class WaveGenerator : MonoBehaviour
{
    // Wave scale
    public float waveScale = 10.0f;

    // Wave speed
    public float waveSpeed = 1.0f;

    // Wave resolution
    public float waveResolution = 1.0f;

    // The mesh
    Mesh mesh;

    // The mesh collider
    MeshCollider meshCollider;

    // The vertices and new vertices
    Vector3[] vertices;
    Vector3[] updatedVertices;

    // Random value
    public float random = 1.0f;

    // Use for initialisation
    void Start()
    {
        // Get the mesh and collider
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();

        // Update the mesh
        meshCollider.sharedMesh = mesh;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Random value
        random = -Time.time / 2;

        // Get the vertices
        vertices = mesh.vertices;

        // Create a new set of vertices to update
        updatedVertices = new Vector3[vertices.Length];

        // Clamp wave resolution
        if (waveResolution <= 0)
            waveResolution = 0;

        // Loop through vertices
        for (int i = 0; i < vertices.Length; i++)
        {
            // Get the vertice
            Vector3 vertex = vertices[i];

            // Generate a sample for perlin noise on the x axis
            float sampleX = (waveSpeed * random) + (vertices[i].x + transform.position.x) / waveResolution;

            // Generate a sample for perlin noise on the z axis
            float sampleZ = -(waveSpeed * random) + (vertices[i].z + transform.position.z) / waveResolution;

            // Set the vertice y value using perlin noise
            vertex.y = Mathf.PerlinNoise(sampleX, sampleZ) * waveScale;

            // Assign to the updated vertices array
            updatedVertices[i] = vertex;
        }

        // Copy the updated vertices over to the mesh vertices
        mesh.vertices = updatedVertices;

        // Update the mesh
        meshCollider.sharedMesh = mesh;

        // Recalculate the bounds and normals
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
