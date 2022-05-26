using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace ProceduralCity
{
    public class GlobalRandom : MonoBehaviour
    {
        private int _cachedSeed;
        [SerializeField] private int _seed = 0;

        private static Random _globalRandom;
        public Random Random
        {
            get
            {
                if (_globalRandom == null) ResetRandom();
                return _globalRandom;
            }
        }

        public void Awake()
        {
            _cachedSeed = _seed;
            ResetRandom();
        }

        public int Next() => Random.Next();

        public int Next(int maxVal) => Random.Next(maxVal);
        public int Next(int minVal, int maxVal) => Random.Next(minVal, maxVal);

        public void ResetRandom() => _globalRandom = _seed == 0 ? new Random() : new Random(_seed);

        public void OnValidate()
        {
            if (_cachedSeed != _seed)
            {
                ResetRandom();
                _cachedSeed = _seed;
            }
        }
    }
}
