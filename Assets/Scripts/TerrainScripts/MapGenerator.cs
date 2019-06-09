// Created using the tutorials provided by Sebastian Lague on procedural landmass generation
// Videos 1 - 5 in the playlist @ https://www.youtube.com/playlist?list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3
using UnityEngine;

// Map generator
public class MapGenerator : MonoBehaviour
{
    // Map width and height
    private int size = 200;

    // Noise scale
    [Range(1.0f, 100.0f)]
    public float noiseScale = 50.0f;

    // The lacunarity of the noise
    [Range(1.0f, 8.0f)]
    public float lacunarity = 2.0f;

    // The number of octaves
    [Range(0, 10)]
    public int octaves = 8;

    // The persistance of the noise
    [Range(0.0f, 1.0f)]
    public float persistance = 0.5f;

    // Mesh height multiplier
    [Range(0.0f, 100.0f)]
    public float heightMultiplier = 15.0f;

    // Falloff variables
    [Range(0, 20)]
    public int falloffPower = 1;
    [Range(0, 20)]
    public int falloffDistance = 1;

    // Mesh Height curve
    public AnimationCurve meshHeight;

    // Random seed value
    public int seed;

    // Octave offset
    private Vector2 offset = Vector2.zero;

    // AutoUpdate
    public bool autoUpdate;

    // Use random seed
    public bool useRandomSeed;

    // The falloff map
    float[,] falloffMap;

    // The type of terrain
    public TerrainType[] regions;

    // Object generator reference
    public ObjectGenerator objectGenerator;

    // The sea game object
    public GameObject sea;

    // Mesh offset parent
    public GameObject meshOffset;

    // Call before start
    void Awake()
    {
        // Get the falloff map
        falloffMap = FalloffGenerator.GenerateFalloffMap(size, falloffPower, falloffDistance);

        // Generate the mesh
        Generate();

        // Set the offset of the mesh
        meshOffset.transform.localPosition = new Vector3(transform.position.x + size / 2, 0.0f, transform.position.z + size / 2);

        // Initialise the object generator
        objectGenerator.SetSize(size, size);
        objectGenerator.Init();

        //// Set the sea gameobject
        //sea.GetComponent<MeshCollider>().enabled = true;
        //sea.GetComponent<WaveGenerator>().enabled = true;
    }

    // Generate the map
    public void Generate()
    {
        // If using a random seed
        if (useRandomSeed)
            seed = Random.Range(0, 1000);

        // Get the noise map
        float[,] noiseMap = NoiseGenerator.GenerateNoiseMap(size, size, seed, noiseScale, lacunarity, persistance, octaves, offset);

        // Create a colour map
        Color[] colourMap = new Color[size * size];

        // Lopp through positions
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                // Add the falloff to the noise map
                noiseMap[x, z] = Mathf.Clamp01(noiseMap[x, z] - falloffMap[x, z]);

                // Record the current height at position
                float currentHeight = noiseMap[x, z];

                // Loop through regions
                for (int i = 0; i < regions.Length; i++)
                {
                    // Current hiegth less than region height
                    if (currentHeight <= regions[i].height)
                    {
                        // Set the colour value at position
                        colourMap[z * size + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        // Display tghe noise map
        MapDisplay display = FindObjectOfType<MapDisplay>();

        // Display the mesh
        display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, heightMultiplier, meshHeight), TextureGenerator.TextureFromColourMap(colourMap, size, size));
    }

    // When script variable is changed in inspector
    void OnValidate()
    {
        // Clamp values
        if (size < 1) size = 1;
        if (size < 1) size = 1;
        if (octaves < 1) octaves = 1;
        if (lacunarity < 1.0f) lacunarity = 1.0f;
        if (noiseScale < 0.0f) noiseScale = 0.0f;

        // Create the falloff map
        falloffMap = FalloffGenerator.GenerateFalloffMap(size, falloffPower, falloffDistance);
    }
}

// The terrain type (show in inspector)
[System.Serializable]
public struct TerrainType
{
    // Terrain name, height and colour
    public string name;
    public float height;
    public Color colour;
}
