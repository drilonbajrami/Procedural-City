using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralCity.Utils;

namespace ProceduralCity.Terrain
{
    public class MapGenerator : MonoBehaviour
    {
        public enum DrawMode { NoiseMap, ColorMap, Mesh, Falloff }
        public DrawMode drawMode;
        public MapData mapData;
        const int mapChunkSize = 241;
        [Range(0, 6)]
        public int levelOfDetail;
        public bool autoUpdate = true;
        public TerrainType[] regions;
        public float[,] falloffMap;

        public Material terrainMaterial;

        void OnValuesUpdated()
        {
            if(!Application.isPlaying)
                GenerateMap();
        }

        public void GenerateMap()
        {
            float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, mapData);

            Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
            for(int y = 0; y < mapChunkSize; y++)
                for(int x = 0; x < mapChunkSize; x++)
                {
                    if (mapData.useFalloff)
                        noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                    float currentHeight = noiseMap[x, y];

                    for(int i = 0; i < regions.Length; i++)
                        if(currentHeight <= regions[i].height)
                        {
                            colorMap[y * mapChunkSize + x] = regions[i].color;
                            break;
                        }
                }

            MapDisplay display = FindObjectOfType<MapDisplay>();

            if (drawMode == DrawMode.NoiseMap)
                display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
            else if (drawMode == DrawMode.ColorMap)
                display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
            else if (drawMode == DrawMode.Mesh)
                display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, mapData.meshHeightMultiplier, mapData.meshHeightCurve, levelOfDetail),
                                 TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
            else if (drawMode == DrawMode.Falloff)
                display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapChunkSize)));

            mapData.ApplyToMaterial(terrainMaterial);
        }

        public void OnValidate()
        {
            if (mapData != null)
            {
                mapData.OnValuesUpdated -= OnValuesUpdated;
                mapData.OnValuesUpdated += OnValuesUpdated;
            }
            falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
        }
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
}