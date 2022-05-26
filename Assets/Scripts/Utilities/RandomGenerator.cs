using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace ProceduralCity.Utils
{
	/// <summary>
	/// Random generator with a random seed (seed == 0) or set seed (seed != 0).
	/// </summary>
	public class RandomGenerator : MonoBehaviour
    {
		/// <summary>
		/// Seed for random generator.
		/// </summary>
		[SerializeField] private int _seed = 0;

		/// <summary>
		/// Random generator.
		/// </summary>
		private static Random _random = null;

		/// <summary>
		/// Random seed generator.
		/// </summary>
		private static Random _randomSeedGenerator = new Random();

		/// <summary>
		/// Returns the random generator instance.
		/// </summary>
		public Random Random {
			get {
				if (_random == null) ResetRandom();
				return _random;
			}
		}

		/// <summary>
		/// Returns a random integer between 0 and maxValue(exclusive).
		/// </summary>
		public int Next(int maxValue) => Random.Next(maxValue);

		/// <summary>
		/// Returns a random integer between given minValue(inclusive) and maxValue(exclusive).
		/// </summary>
		public int Next(int minValue, int maxValue) => Random.Next(minValue, maxValue);

		/// <summary>
		/// Set the seed for this random generator.
		/// </summary>
		/// <param name="seed">Seed.</param>
		public void SetSeed(int seed)
		{
			_seed = seed;
			ResetRandom();
		}

		/// <summary>
		/// Resets the random generator by using the current seed. If seed is '0' then a randomly generated seed is used.
		/// </summary>
		public void ResetRandom() => _random = _seed == 0 ? new Random(_randomSeedGenerator.Next()) : new Random(_seed);
	}
}