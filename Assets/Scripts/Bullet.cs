using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            Barrel barrel = other.GetComponent<Barrel>();

            barrel.barrelCount--;
            barrel.barrelText.text = barrel.barrelCount.ToString();
            if (barrel.barrelCount<=0)
            {
                Destroy(other.gameObject);
                //expo;
            }
            gameObject.SetActive(false);
        }
        if (other.CompareTag("ExpoBarrel"))
        {
            ExpoBarrel expoBarrel = other.GetComponent<ExpoBarrel>();
            expoBarrel.barrelCount--;
            expoBarrel.barrelCountText.text = expoBarrel.barrelCount.ToString();

            if (expoBarrel.barrelCount<=0)
            {
                expoBarrel.explosionVFX.Play();
                expoBarrel.isExplosion = true;
                Destroy(other.gameObject, .3f);
            }
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Range"))
        {
            gameObject.SetActive(false);
        }
    }
}
