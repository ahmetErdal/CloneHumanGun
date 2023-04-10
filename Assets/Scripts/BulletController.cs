using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float delay;
    [SerializeField] float timer = 0;

    [SerializeField] Transform firePosition;
    [SerializeField] Pooler bulletPooler;

    private Bullet Bullet()
    {
        return bulletPooler.GetGo<Bullet>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            Fire(delay);
           
        }
        if (other.CompareTag("ExpoBarrel"))
        {
            Fire(delay);
        }
    }

    void Fire(float delay)
    {
        timer -= Time.deltaTime;
        if (timer<=0)
        {
            Bullet bullet = Bullet();
            bullet.gameObject.SetActive(true);

            bullet.transform.position = firePosition.position;
            bullet.transform.rotation = Quaternion.identity;

            timer = delay;
        }
    }

}
