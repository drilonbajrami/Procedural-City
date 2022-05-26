using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralCity.Terrain;

namespace ProceduralCity
{
    [CreateAssetMenu(menuName = "Terrain/Map Data",fileName = "Map Data")]
    public class MapData : ScriptableObject
    {
        [HideInInspector]
        public event System.Action OnValuesUpdated;

        [Range(10f, 100f)] public float scale = 50f;
        [Range(1, 6)] public int octaves = 4;
        [Range(0f, 1f)] public float persistance = .5f;
        [Range(0.1f, 4f)] public float lacunarity = 2;
        public int seed;
        public Vector2 offset;

        public bool useFalloff;
        public float meshHeightMultiplier;
        public AnimationCurve meshHeightCurve;

        public bool autoUpdate;

        public void OnValidate()
        {
            if (scale < 0) scale = 0;
            if (lacunarity < 1) lacunarity = 1;

            if (autoUpdate) NotifyOfUpdatedValues();
        }

        public void NotifyOfUpdatedValues() => OnValuesUpdated?.Invoke();

        public void ApplyToMaterial(Material material)
        {

        }
    }
}