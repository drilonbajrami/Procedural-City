using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ProceduralCity.Terrain;

namespace ProceduralCity.EditorTools
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MapGenerator mapGenerator = (MapGenerator)target;

            if(DrawDefaultInspector())
            {
                if (mapGenerator.autoUpdate) mapGenerator.GenerateMap();
            }

            if (GUILayout.Button("Generate"))
            {
                mapGenerator.GenerateMap();
            }
        }
    }
}
