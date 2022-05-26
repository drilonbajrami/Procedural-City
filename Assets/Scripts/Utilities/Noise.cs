using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralCity.Utils
{
    /// <summary>
    /// Layered noise generator.
    /// </summary>
    public static class Noise
    {
        public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, MapData noiseData)
        {
            float[,] noiseMap = new float[mapWidth, mapHeight];

            // Random generator with seed.
            System.Random rand = new System.Random(noiseData.seed);

            // Offsets for each octave.
            Vector2[] octaveOffsets = new Vector2[noiseData.octaves];

            // Set random offsets for each octave.
            for (int i = 0; i < noiseData.octaves; i++)
            {
                float offsetX = rand.Next(-100000, 100000) + noiseData.offset.x;
                float offsetY = rand.Next(-100000, 100000) + noiseData.offset.y;
                octaveOffsets[i] = new Vector2(offsetX, offsetY);
            }

            // If scale set to '0', set it to '0.001f' so to not divide sample points by '0'.
            if (noiseData.scale <= 0) noiseData.scale = 0.0001f;

            // Used for normalizing the noise map
            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;

            // Mid point for scaling the noise map from center.
            float halfWidth = mapWidth / 2f;
            float halfHeight = mapHeight / 2f;

            for (int y = 0; y < mapHeight; y++)
                for (int x = 0; x < mapWidth; x++)
                {
                    float amplitude = 1;
                    float frequency = 1;
                    float noiseHeight = 0;

                    // Octave layering
                    for (int i = 0; i < noiseData.octaves; i++)
                    {
                        // Sample height values and divide by scale for getting non-integer values.
                        float sampleX = (x - halfWidth) / noiseData.scale * frequency + octaveOffsets[i].x;
                        float sampleY = (y - halfHeight) / noiseData.scale * frequency + octaveOffsets[i].y;

                        // (* 2 - 1) to give negative values as well.
                        float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                        noiseHeight += perlinValue * amplitude;

                        // For each sample point in the next octave:
                        // Amplitude decreases (amplitude = persistance ^ i)
                        // Frequency increases (frequency = lacunarity ^ i)
                        amplitude *= noiseData.persistance;
                        frequency *= noiseData.lacunarity;

                        noiseMap[x, y] = perlinValue;
                    }

                    // Store maximum and minimum noise height for normalizing
                    if (noiseHeight > maxNoiseHeight) maxNoiseHeight = noiseHeight;
                    else if (noiseHeight < minNoiseHeight) minNoiseHeight = noiseHeight;

                    noiseMap[x, y] = noiseHeight;
                }

            for (int y = 0; y < mapHeight; y++)
                for (int x = 0; x < mapWidth; x++)
                    noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);

            return noiseMap;
        }
    }
}