using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpoBarrelCheckControl : MonoBehaviour
{
    [SerializeField] ExpoBarrel expoBarrel;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            if (expoBarrel.isExplosion)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
