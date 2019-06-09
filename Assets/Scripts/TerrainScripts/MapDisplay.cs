// Created using the tutorials provided by Sebastian Lague on procedural landmass generation
// Videos 1 - 5 in the playlist @ https://www.youtube.com/playlist?list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3
using UnityEngine;

// Display the maps
public class MapDisplay : MonoBehaviour
{
    // Mesh filter
    public MeshFilter meshFilter;

    // Mesh renderer
    public MeshRenderer meshRenderer;

    // Mesh collder
    public MeshCollider meshCollider;

    // Draw the the texture map (colour or noise)
    public void DrawTexture(Texture2D texture)
    {
        //// Set the material texture and scale
        //textureRender.sharedMaterial.mainTexture = texture;
        //textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    // Draw the mesh 
    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        // Create the mesh
        Mesh mesh = meshData.CreateMesh();

        // Set the mesh filter mesh
        meshFilter.sharedMesh = mesh;

        // Set the texture to renderer
        meshRenderer.sharedMaterial.mainTexture = texture;

        // Create the mesh collider
        meshCollider.sharedMesh = mesh;
    }
}