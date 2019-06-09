// Created using the tutorials provided by Sebastian Lague on procedural landmass generation
// Videos 1 - 5 in the playlist @ https://www.youtube.com/playlist?list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3
using UnityEngine;

// Falloff map generator
public static class FalloffGenerator
{
    // Creates the falloff map
    public static float[,] GenerateFalloffMap(int size, int falloffPower, int falloffDistance)
    {
        // Create the falloff map
        float[,] falloffMap = new float[size, size];

        // Loop through the positions
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                // Clamp values of positions betwen -1 and 1
                float x = i / (float)size * 2 - 1;
                float z = j / (float)size * 2 - 1;

                // Which of x and z is closest to edge of map
                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(z));

                // Set the the value at the postion of the falloff map
                falloffMap[i, j] = Evaluate(value, falloffPower, falloffDistance);
            }
        }

        // Return the falloffmap 
        return falloffMap;
    }

    // Evaluate the the value of the falloff map at position
    static float Evaluate(float value, int falloffPower, int falloffDistance)
    {
        // The resulting falloff
        float result = Mathf.Pow(value, falloffPower) / (Mathf.Pow(value, falloffPower) + Mathf.Pow(falloffDistance - (falloffDistance * value), falloffPower));

        // Return the result
        return result;
    }
}