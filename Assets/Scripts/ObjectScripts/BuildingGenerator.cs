using System.Collections.Generic;
using UnityEngine;

// Generates buildings
public class BuildingGenerator : MonoBehaviour
{
    // Building objects
    public List<GameObject> bottoms = new List<GameObject>();
    public List<GameObject> midSections = new List<GameObject>();
    public List<GameObject> roofs = new List<GameObject>();
    public GameObject window;
    public GameObject door;

    [Range(1.0f, 2.0f)]
    public float minSize;

    [Range(2.0f, 3.0f)]
    public float maxSize;

    GameObject buildingBase;
    GameObject midSection;
    GameObject buildingRoof;

    // Start is called before the first frame update
    void Start()
    {
        // Get the base
        buildingBase = Instantiate(SelectBase(), Vector3.zero, Quaternion.identity, null) as GameObject;
        buildingBase.transform.parent = transform;

        // Size of building
        float size = Random.Range(minSize, maxSize);
        buildingBase.transform.localScale = new Vector3(1.0f * size, 1.0f, 1.0f * size);

        // Get the height of the base piece
        float baseHeight = buildingBase.GetComponent<Renderer>().bounds.size.y;

        // Get the mid sections
        midSection = Instantiate(SelectMidSection(), Vector3.zero, Quaternion.identity, null) as GameObject;
        midSection.transform.localScale = new Vector3(1.0f * size, 1.0f, 1.0f * size);
        midSection.transform.position = buildingBase.transform.position + Vector3.up * (baseHeight);
        midSection.transform.parent = transform;

        // Get the height of the midsection
        float midSectionHeight = midSection.GetComponent<Renderer>().bounds.size.y;

        // The roof will be positioned at base + up * (baseHeight + midHeight)
        buildingRoof = Instantiate(SelectRoof(), Vector3.zero, Quaternion.identity, null) as GameObject;
        buildingRoof.transform.localScale = new Vector3(1.0f * size, 1.0f, 1.0f * size);
        buildingRoof.transform.position = buildingBase.transform.position + Vector3.up * (baseHeight + midSectionHeight);
        buildingRoof.transform.parent = transform;
    }

    GameObject SelectBase()
    {
        int count = Random.Range(0, bottoms.Count);
        return bottoms[count];
    }

    GameObject SelectMidSection()
    {
        int count = Random.Range(0, midSections.Count);
        return midSections[count];
    }

    GameObject SelectRoof()
    {
        int count = Random.Range(0, roofs.Count);
        return roofs[count];
    }
}
