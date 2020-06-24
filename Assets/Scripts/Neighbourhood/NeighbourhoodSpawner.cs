using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDMLF.Locations {
    public class NeighbourhoodSpawner : MonoBehaviour
    {

        // Eventually, this script will create a randomized neighbourhood.
        // For now, it just scrambles what's already there.

        [SerializeField] AddressBase[] addresses;

        const int X_RANGE = 8;
        const int Y_RANGE = 3;

        // Start is called before the first frame update
        void Start()
        {
            addresses = GetComponentsInChildren<AddressBase>();
            AssignAddresses();
            RandomizeLocales();
        }

        private void RandomizeLocales()
        {
            Vector2 newPos;

            foreach (AddressBase ab in addresses)
            {
                newPos.x = UnityEngine.Random.Range(-X_RANGE, X_RANGE);
                newPos.y = UnityEngine.Random.Range(-Y_RANGE, Y_RANGE);
                ab.transform.position = newPos;
            }
        }

        private void AssignAddresses()
        {
            int a = 1;
            foreach (AddressBase ab in addresses)
            {
                ab.SetAddress(a);
                a++;
            }
        }




    }
}