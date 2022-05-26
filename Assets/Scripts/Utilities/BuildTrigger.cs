using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralCity.Utils
{
	public class BuildTrigger : MonoBehaviour
	{
		[SerializeField] private KeyCode BuildKey = KeyCode.Space;
		[SerializeField] private bool BuildOnStart = false;

		//private Shape Root;
		private RandomGenerator _random;

		void Start()
		{
			//Root = GetComponent<Shape>();
			_random = GetComponent<RandomGenerator>();
			if (BuildOnStart) Build();
		}

		void Update()
        {
            if (Input.GetKeyDown(BuildKey)) Build();
        }

        void Build()
		{
			if (_random != null)
			{
				_random.ResetRandom();
			}
			//if (Root != null)
			{
				//Root.Generate();
			}
		}
	}
}