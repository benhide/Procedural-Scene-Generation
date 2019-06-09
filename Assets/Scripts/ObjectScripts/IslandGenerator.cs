using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// THe island generator
public class IslandGenerator : MonoBehaviour
{
    // THe island game objects
    public GameObject house;
    public GameObject fire;
    public GameObject bush1;
    public GameObject bush2;

    // The spawned game objects
    public List<GameObject> spawnedObject;

    // Max number to spawn
    public int maxNumberToSpawn;
    int numberSpawned;

    // All items spawned and positioned
    public bool allSpawned = false;
    public bool allPositioned = false;

    // Max min distances
    public float maxDistX;
    public float minDistX;
    public float maxDistZ;
    public float minDistZ;

    // Start is called before the first frame update
    public void Init()
    {
        // Initialse the list
        spawnedObject = new List<GameObject>();

        // Set the position
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        // Spawn the settlements and set their positions
        SpawnSettlement();
        SetPositions();
    }

    // Set settlements psositions
    void SetPositions()
    {
        // Count for loop
        int iterations = 0;

        // Count for items repositioned
        int repositionedCount = 0;

        // While everything has not been spawned
        while (!allSpawned)
        {
            // Loop through objects to spawn
            for (int i = 0; i < spawnedObject.Count; i++)
            {
                for (int j = 0; j < spawnedObject.Count; j++)
                {
                    // If the objects are the same
                    if (spawnedObject[i] == spawnedObject[j])
                        continue;

                    // Check there is not overlapping objects
                    if (spawnedObject[i].transform.GetChild(0).GetComponent<Renderer>().bounds.Intersects(spawnedObject[j].transform.GetChild(0).GetComponent<Renderer>().bounds) || Math.Abs(Vector3.Distance(spawnedObject[i].transform.position, fire.transform.position)) < 1.0f)
                    {
                        // Reposition if overlapping - increase count
                        RepositionObject(spawnedObject[i]);
                        repositionedCount++;
                    }
                }
            }

            // If not objects repositioned
            if (repositionedCount == 0)
            {
                // If everything has not been spawned
                if (!allSpawned)
                {
                    // Set the parent game object
                    foreach (var item in spawnedObject)
                        item.transform.parent = transform;

                    // Roate the island
                    Vector3 euler = transform.eulerAngles;
                    euler.y = Random.Range(0.0f, 360.0f);
                    transform.eulerAngles = euler;
                }

                // Everything spawned
                allSpawned = true;
            }

            // Else reset reposition count
            else
                repositionedCount = 0;

            // Increase the iterations count
            iterations++;

            // Stop an endless loop
            if (iterations > 10000)
                break;
        }
    }

    // Reposition object on island
    void RepositionObject(GameObject obj)
    {
        // Random position
        Vector2 pos = new Vector2(Random.Range(minDistX, maxDistX), Random.Range(minDistZ, maxDistZ));
        obj.transform.position = new Vector3(pos.x, 0.4f, pos.y);

        // Set the rotation
        Vector3 lookPosition = fire.transform.position - obj.transform.position;
        lookPosition.y = 0.0f;
        Quaternion targetRotation = Quaternion.LookRotation(lookPosition);
        obj.transform.rotation = targetRotation;
        obj.transform.Rotate(0, 90, 0);
    }

    // Spawn the settlements
    void SpawnSettlement()
    {
        // The number to spawn
        numberSpawned = Random.Range(maxNumberToSpawn / 2, maxNumberToSpawn);

        // Loop through the sttelments to spawn
        for (int i = 0; i < numberSpawned; i++)
        {
            // Random position
            Vector2 pos = new Vector2(Random.Range(minDistX, maxDistX), Random.Range(minDistZ, maxDistZ));

            // Set the game object
            GameObject go = Instantiate(house, new Vector3(pos.x, 0.4f, pos.y), Quaternion.identity) as GameObject;
            go.transform.position = new Vector3(pos.x, 0.4f, pos.y);

            // Add to the list
            spawnedObject.Add(go);

            // Set the rotation
            Vector3 lookPosition = fire.transform.position - spawnedObject[i].transform.position;
            lookPosition.y = 0.0f;
            Quaternion targetRotation = Quaternion.LookRotation(lookPosition);
            spawnedObject[i].transform.rotation = targetRotation;
            spawnedObject[i].transform.Rotate(0, 90, 0);
        }

        // The bushes
        for (int i = 0; i < numberSpawned / 2; i++)
        {
            // Random position
            Vector2 pos = new Vector2(Random.Range(minDistX, maxDistX), Random.Range(minDistZ, maxDistZ));

            // Set the game object
            GameObject go = Instantiate(bush1, new Vector3(pos.x, 0.4f, pos.y), Quaternion.identity) as GameObject;
            go.transform.localScale *= 0.5f;
            go.transform.position = new Vector3(pos.x, 0.4f, pos.y);

            // Add to the list
            spawnedObject.Add(go);
        }

        // The bushes
        for (int i = 0; i < numberSpawned / 2; i++)
        {
            // Random position
            Vector2 pos = new Vector2(Random.Range(minDistX, maxDistX), Random.Range(minDistZ, maxDistZ));

            // Set the game object
            GameObject go = Instantiate(bush2, new Vector3(pos.x, 0.4f, pos.y), Quaternion.identity) as GameObject;
            go.transform.localScale *= 0.5f;
            go.transform.position = new Vector3(pos.x, 0.4f, pos.y);

            // Add to the list
            spawnedObject.Add(go);
        }
    }
}