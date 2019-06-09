// Created using the tutorials provided by Sebastian Lague on procedural landmass generation
// Videos 1 - 5 in the playlist @ https://www.youtube.com/playlist?list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3
using UnityEngine;

// Mesh generator
public static class MeshGenerator
{
    // Generates the terrain
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve)
    {
        // Get the map size
        int xSize = heightMap.GetLength(0);
        int zSize = heightMap.GetLength(1);

        // Center the map
        float topLeftX = (xSize - 1) / -2.0f;
        float topLeftZ = (zSize - 1) /  2.0f;

        // Create a mesh data - map size
        MeshData meshData = new MeshData(xSize, zSize);
        int vertIndex = 0;

        // Loop through positions
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                // Create the vertices - shifted to center map
                meshData.verts[vertIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, z]) * heightMultiplier, topLeftZ - z);

                // Create the uvs - percentage between 0 and 1
                meshData.uvs[vertIndex] = new Vector2(x / (float)xSize, z / (float)zSize);

                // Skip over edges of the map - ignore right and bottom vertices
                if (x < xSize - 1 && z < zSize - 1)
                {
                    // Create the mesh triangles
                    meshData.AddTriangle(vertIndex, vertIndex + xSize + 1, vertIndex + xSize);
                    meshData.AddTriangle(vertIndex + xSize + 1, vertIndex, vertIndex + 1);
                }

                // Increase vertices index
                vertIndex++;
            }
        }

        // Return the mesh data
        return meshData;
    }
}

// The mesh data
public class MeshData
{
    // The mesh verticess triangless and uvs
    public Vector3[] verts;
    public Vector2[] uvs;
    public int[] tris;

    // Triangles index
    int trisIndex;

    // Constructor for the meshdata
    public MeshData(int xSize, int zSize)
    {
        // Create the vertices, triangles and uv arrays
        verts = new Vector3[xSize * zSize];
        uvs = new Vector2[xSize * zSize];
        tris = new int[(xSize - 1) * (zSize - 1) * 6];
    }

    // Adds the mesh triangles 
    public void AddTriangle(int first, int second, int third)
    {
        // Set the triangles 
        tris[trisIndex] = first;
        tris[trisIndex + 1] = second;
        tris[trisIndex + 2] = third;

        // Increase triangles index
        trisIndex += 3;
    }

    // Create the mesh
    public Mesh CreateMesh()
    {
        // Create the mesh - vertices, triangles and uvs
        Mesh mesh = new Mesh
        {
            vertices = verts,
            triangles = tris,
            uv = uvs
        };

        // Recalculate the normals
        mesh.RecalculateNormals();

        // Return the mesh
        return mesh;
    }
}