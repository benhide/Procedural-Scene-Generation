using System.Collections.Generic;
using UnityEngine;

// Grid details
public enum GRID_DETAILS { empty, tree, rock, bush, island };

// Grid squares
public class GridSqaure
{
    public Vector3 position;
    public GRID_DETAILS gridDetails;
}

// Grid data
public class ObjectGenerator : MonoBehaviour
{
    // The map wdth and zSize
    private int xSize;
    private int zSize;

    // Distance from spawn point
    public int minDistX;
    public int maxDistX;
    public int minDistZ;
    public int maxDistZ;

    // The grid squares
    public GridData[,] gridSqaures;

    // The trees
    [Range(0.0f, 1.0f)]
    public float treeProbability;
    [Range(0.1f, 10.0f)]
    public float treeSize;

    // Number of trees
    int numberOfTrees;

    // List of trees
    public List<GameObject> trees;

    // Tree gameobjects
    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;
    public GameObject tree4;

    // The rocks
    [Range(0.0f, 1.0f)]
    public float rockProbability;
    [Range(0.1f, 10.0f)]
    public float rockSize;

    // Number of rocks
    int numberOfTRocks;

    // List of rocks
    public List<GameObject> rocks;

    // Rock gameobjects
    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;

    // The bushes
    [Range(0.0f, 1.0f)]
    public float bushProbability;
    [Range(0.1f, 10.0f)]
    public float bushSize;

    // Number of bushes
    int numberOfBushes;

    // List of bushes
    public List<GameObject> bushes;

    // Bush gameobjects
    public GameObject bush1;
    public GameObject bush2;
    public GameObject bush3;

    // The islands
    [Range(5, 10)]
    public int maxIslandCount;

    // Number of islands
    public int numberOfIslands;

    // List of islands
    public List<GameObject> islands;

    // islands gameobjects
    public GameObject island1;
    public GameObject island2;
    public GameObject island3;
    public GameObject island4;
    public GameObject island5;

    // The fish
    public GameObject fishes;

    // Start is called before the first frame update
    public void Init()
    {
        // Initialise the grid data
        gridSqaures = new GridData[xSize, zSize];
        for (int x = 0; x < xSize; x++)
            for (int z = 0; z < zSize; z++)
                gridSqaures[x, z] = new GridData(new Vector3(x, 0.0f, z), GRID_DETAILS.empty);

        // Initialise trees
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                // Probability of item
                float treeProb = Random.Range(0.0f, 1.0f);

                // If within range
                if (treeProb < treeProbability)
                {
                    // Raycast from position downwards
                    if (Physics.Raycast(new Vector3(x, 50.0f, z), -Vector3.up, out RaycastHit hit, 1000.0f))
                    {
                        // If the hit point is within range
                        if (hit.point.y > 3.0f && hit.point.y < 6.5f)
                        {
                            // Select the tree
                            GameObject tree = SelectTree();

                            // Set the rotation
                            Quaternion rotation = Quaternion.identity;
                            rotation.eulerAngles = new Vector3(Random.value * 10.0f - 5.0f, Random.value * 360.0f, Random.value * 10.0f - 5.0f);

                            // Add to the list
                            trees.Add(Instantiate(tree, new Vector3(x, hit.point.y - 0.15f, z), rotation, transform) as GameObject);

                            // Increase count and block off grid square
                            numberOfTrees++;
                            gridSqaures[x, z].gridDetails = GRID_DETAILS.tree;
                        }
                    }
                }
            }
        }

        // Generate the trees / forests
        GenerateTrees();

        // Initialise rocks
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                // Probability of item
                float rockProb = Random.Range(0.0f, 1.0f);

                // If within range
                if (rockProb < rockProbability)
                {
                    // Check if valid grid square
                    if (gridSqaures[x, z].gridDetails == GRID_DETAILS.empty)
                    {
                        // Raycast from position downwards
                        if (Physics.Raycast(new Vector3(x, 50.0f, z), -Vector3.up, out RaycastHit hit, 1000.0f))
                        {
                            // If the hit point is within range
                            if (hit.point.y > 3.0f && hit.point.y < 11.0f)
                            {
                                // Select the rock
                                GameObject rock = SelectRock();

                                // Set the rotation
                                Quaternion rotation = Quaternion.identity;
                                rotation.eulerAngles = new Vector3(0.0f, Random.value * 360.0f, 0.0f);
                                rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                                // Add to the list
                                rocks.Add(Instantiate(rock, new Vector3(x, hit.point.y, z), rotation, transform) as GameObject);

                                // Increase count and block off grid square
                                numberOfTRocks++;
                                gridSqaures[x, z].gridDetails = GRID_DETAILS.rock;
                            }
                        }
                    }
                }
            }
        }

        // Generate the rocks
        GenerateRocks();

        // Initialise bushes
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                // Probability of item
                float bushProb = Random.Range(0.0f, 1.0f);

                // If within range
                if (bushProb < bushProbability)
                {
                    // Check if valid grid square
                    if (gridSqaures[x, z].gridDetails == GRID_DETAILS.empty)
                    {
                        // Raycast from position downwards
                        if (Physics.Raycast(new Vector3(x, 50.0f, z), -Vector3.up, out RaycastHit hit, 1000.0f))
                        {
                            // If the hit point is within range
                            if (hit.point.y > 2.0f && hit.point.y < 6.5f)
                            {
                                // Select the bush
                                GameObject bush = SelectBush();
                                Quaternion rotation = Quaternion.identity;

                                // Set the rotation
                                rotation.eulerAngles = new Vector3(Random.value * 10.0f - 5.0f, Random.value * 360.0f, Random.value * 10.0f - 5.0f);

                                // Add to the list
                                bushes.Add(Instantiate(bush, new Vector3(x, hit.point.y, z), rotation, transform) as GameObject);

                                // Increase count and block off grid square
                                numberOfBushes++;
                                gridSqaures[x, z].gridDetails = GRID_DETAILS.bush;
                            }
                        }
                    }
                }
            }
        }

        // Initialise islands
        int count = 0;

        // Set the size of the grid to check
        int gridTocheck = 12;
        int gridToCheckSqrd = gridTocheck * gridTocheck;

        // ICounter to stop endless loop
        int iterations = 0;

        // While there are island to spawn
        while (numberOfIslands < maxIslandCount)
        {
            // RAndom x and z position
            int xPos = Random.Range(20, xSize - 20);
            int zPos = Random.Range(20, zSize - 20);

            // Go through the ajoining grid sqaures
            for (int x = 0; x < gridTocheck; x++)
            {
                for (int z = 0; z < gridTocheck; z++)
                {
                    // Check to set if valid position
                    if (gridSqaures[xPos + x, zPos + z].gridDetails == GRID_DETAILS.empty && gridSqaures[xPos - x, zPos - z].gridDetails == GRID_DETAILS.empty)
                    {
                        // Raycast from position downwards
                        if (Physics.Raycast(new Vector3(xPos + x, 50.0f, zPos + z), -Vector3.up, out RaycastHit hit1, 1000.0f) && Physics.Raycast(new Vector3(xPos - x, 50.0f, zPos - z), -Vector3.up, out RaycastHit hit2, 1000.0f))
                        {
                            // If the hit point is within range
                            if (hit1.point.y < 0.5f && hit2.point.y < 0.5f)
                            {
                                // Increase count of valid grid positions
                                count++;
                            }
                        }
                    }
                }
            }

            // If all ajoining gridsquares are valid
            if (count == gridToCheckSqrd)
            {
                // Get the island
                GameObject island = Instantiate(SelectIsland(), Vector3.zero, Quaternion.identity, transform) as GameObject;

                // Initialise the island
                island.GetComponent<IslandGenerator>().Init();

                // Set the position
                island.transform.position = new Vector3(xPos, 1.5f, zPos);

                // Add to list and increase count
                islands.Add(island);
                numberOfIslands++;

                // Close off grid squares
                for (int x = 0; x < gridTocheck; x++)
                {
                    for (int z = 0; z < gridTocheck; z++)
                    {
                        gridSqaures[xPos + x, zPos + z].gridDetails = GRID_DETAILS.island;
                        gridSqaures[xPos - x, zPos - z].gridDetails = GRID_DETAILS.island;
                    }
                }

            }

            // Reset count and increase iterations
            count = 0;
            iterations++;

            // Stop an endless loop
            if (iterations > 10000)
            {
                break;
                //if (numberOfIslands >= 5)
                //    break;
                //else
                //    iterations = 0;
            }
        }

        // Set the fishes
        fishes.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }
    
    // Select the tree to use
    GameObject SelectTree()
    {
        GameObject newTree = null;
        switch (Random.Range(0, 4))
        {
            case 0: newTree = tree1; break;
            case 1: newTree = tree2; break;
            case 2: newTree = tree3; break;
            case 3: newTree = tree4; break;
        }

        float size = Random.Range(treeSize / 2.0f, treeSize);
        newTree.transform.localScale = Vector3.one * size;
        return newTree;
    }

    // Select the rock to use
    GameObject SelectRock()
    {
        GameObject newRock = null;
        switch (Random.Range(0, 3))
        {
            case 0: newRock = rock1; break;
            case 1: newRock = rock2; break;
            case 2: newRock = rock3; break;
        }

        float size = Random.Range(rockSize / 4.0f, rockSize);
        newRock.transform.localScale = Vector3.one * size;
        return newRock;
    }

    // Select the island to use
    GameObject SelectBush()
    {
        GameObject newBush = null;
        switch (Random.Range(0, 3))
        {
            case 0: newBush = bush1; break;
            case 1: newBush = bush2; break;
            case 2: newBush = bush3; break;
        }

        float size = Random.Range(bushSize / 2.0f, bushSize);
        newBush.transform.localScale = Vector3.one * size;
        return newBush;
    }

    // Select the island to use
    GameObject SelectIsland()
    {
        GameObject newIsland = null;
        switch (Random.Range(0, 5))
        {
            case 0: newIsland = island1; break;
            case 1: newIsland = island2; break;
            case 2: newIsland = island3; break;
            case 3: newIsland = island4; break;
            case 4: newIsland = island5; break;
        }
        return newIsland;
    }

    // Generate the trees
    void GenerateTrees()
    {
        // Tree count
        int count = trees.Count;

        // set the min and max distances
        int distX = Random.Range(minDistX, maxDistX);
        int distZ = Random.Range(minDistZ, maxDistZ);

        // Get the tree count
        for (int i = 0; i < count; i++)
        {
            // Loop through forest
            for (int x = 0; x < distX; x++)
            {
                for (int z = 0; z < distZ; z++)
                {
                    // Set the position
                    Vector3 position = new Vector3(trees[i].GetComponent<Transform>().position.x + x, 0.0f, trees[i].GetComponent<Transform>().position.z + z);

                    // Probability of item
                    float treeProb = Random.Range(0.0f, 1.0f);

                    // Probability of item
                    if (treeProb < treeProbability)
                    {
                        // Raycast from position downwards
                        if (Physics.Raycast(new Vector3(position.x, 50.0f, position.z), -Vector3.up, out RaycastHit hit, 1000.0f))
                        {
                            // Inside the map
                            bool inMap = ((position.x > 0 && position.x < xSize) && (position.z > 0 && position.x < zSize)) ? true : false;

                            // Check if in range
                            if (hit.point.y > 3.0f && hit.point.y < 6.5f && inMap)
                            {
                                // Check to set if valid position
                                if (gridSqaures[(int)position.x, (int)position.z].gridDetails == GRID_DETAILS.empty)
                                {
                                    // Select the tree
                                    GameObject tree = SelectTree();

                                    // Set the rotation
                                    Quaternion rotation = Quaternion.identity;
                                    rotation.eulerAngles = new Vector3(Random.value * 10.0f - 5.0f, Random.value * 360.0f, Random.value * 10.0f - 5.0f);

                                    // Add to the list
                                    trees.Add(Instantiate(tree, new Vector3(position.x, hit.point.y - 0.15f, position.z), rotation, transform) as GameObject);

                                    // Block off grid square
                                    gridSqaures[(int)position.x, (int)position.z].gridDetails = GRID_DETAILS.tree;
                                }
                            }
                        }
                    }
                }
            }

            // Loop through forest
            for (int x = distX; x > 0; x--)
            {
                for (int z = distZ; z > 0; z--)
                {
                    // Set the position
                    Vector3 position = new Vector3(trees[i].GetComponent<Transform>().position.x + x, 0.0f, trees[i].GetComponent<Transform>().position.z + z);

                    // Probability of item
                    float treeProb = Random.Range(0.0f, 1.0f);

                    // Probability of item
                    if (treeProb < treeProbability)
                    {
                        // Raycast from position downwards
                        if (Physics.Raycast(new Vector3(position.x, 50.0f, position.z), -Vector3.up, out RaycastHit hit, 1000.0f))
                        {
                            // Inside the map
                            bool inMap = ((position.x > 0 && position.x < xSize) && (position.z > 0 && position.x < zSize)) ? true : false;

                            // Check if in range
                            if (hit.point.y > 3.0f && hit.point.y < 6.5f && inMap)
                            {
                                // Check to set if valid position
                                if (gridSqaures[(int)position.x, (int)position.z].gridDetails == GRID_DETAILS.empty)
                                {
                                    // Select the tree
                                    GameObject tree = SelectTree();

                                    // Set the rotation
                                    Quaternion rotation = Quaternion.identity;
                                    rotation.eulerAngles = new Vector3(Random.value * 10.0f - 5.0f, Random.value * 360.0f, Random.value * 10.0f - 5.0f);

                                    // Add to the list
                                    trees.Add(Instantiate(tree, new Vector3(position.x, hit.point.y - 0.15f, position.z), rotation, transform) as GameObject);

                                    // Block off grid square
                                    gridSqaures[(int)position.x, (int)position.z].gridDetails = GRID_DETAILS.tree;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    // Generate the rocks
    void GenerateRocks()
    {
        // Tree count
        int count = rocks.Count;

        // set the min and max distances
        int distX = Random.Range(minDistX, maxDistX);
        int distZ = Random.Range(minDistZ, maxDistZ);

        // Get the tree count
        for (int i = 0; i < count; i++)
        {
            // Loop through rocks grid
            for (int x = 0; x < distX; x++)
            {
                for (int z = 0; z < distZ; z++)
                {
                    Vector3 position = new Vector3(rocks[i].GetComponent<Transform>().position.x + x, 0.0f, rocks[i].GetComponent<Transform>().position.z + z);
                    float rocksProb = Random.Range(0.0f, 1.0f);
                    if (rocksProb < rockProbability * 4.0f)
                    {
                        if (Physics.Raycast(new Vector3(position.x, 50.0f, position.z), -Vector3.up, out RaycastHit hit, 1000.0f))
                        {
                            bool inMap = ((position.x > 0 && position.x < xSize) && (position.z > 0 && position.x < zSize)) ? true : false;
                            if (hit.point.y > 6.0f && hit.point.y < 11.0f && inMap)
                            {
                                if (gridSqaures[(int)position.x, (int)position.z].gridDetails == GRID_DETAILS.empty)
                                {
                                    GameObject rock = SelectRock();
                                    Quaternion rotation = Quaternion.identity;
                                    rotation.eulerAngles = new Vector3(0.0f, Random.value * 360.0f, 0.0f);
                                    rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                                    rocks.Add(Instantiate(rock, new Vector3(position.x, hit.point.y, position.z), rotation, transform) as GameObject);
                                    gridSqaures[(int)position.x, (int)position.z].gridDetails = GRID_DETAILS.rock;
                                }
                            }
                        }
                    }
                }
            }

            // Loop through rocks grid
            for (int x = distX; x > 0; x--)
            {
                for (int z = distZ; z > 0; z--)
                {
                    Vector3 position = new Vector3(rocks[i].GetComponent<Transform>().position.x + x, 0.0f, rocks[i].GetComponent<Transform>().position.z + z);
                    float rocksProb = Random.Range(0.0f, 1.0f);
                    if (rocksProb < rockProbability)
                    { 
                        if (Physics.Raycast(new Vector3(position.x, 50.0f, position.z), -Vector3.up, out RaycastHit hit, 1000.0f))
                        {
                            bool inMap = ((position.x > 0 && position.x < xSize) && (position.z > 0 && position.x < zSize)) ? true : false;
                            if (hit.point.y > 6.0f && hit.point.y < 11.0f && inMap)
                            {
                                if (gridSqaures[(int)position.x, (int)position.z].gridDetails == GRID_DETAILS.empty)
                                {
                                    GameObject rock = SelectRock();
                                    Quaternion rotation = Quaternion.identity;
                                    rotation.eulerAngles = new Vector3(0.0f, Random.value * 360.0f, 0.0f);
                                    rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                                    rocks.Add(Instantiate(rock, new Vector3(position.x, hit.point.y, position.z), rotation, transform) as GameObject);
                                    gridSqaures[(int)position.x, (int)position.z].gridDetails = GRID_DETAILS.rock;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    // Set size
    public void SetSize(int xSize, int zSize)
    {
        this.xSize = xSize;
        this.zSize = zSize;
    }
}