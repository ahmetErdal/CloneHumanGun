using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class Door : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lensText;
    [SerializeField] GameObject dummy;
    [SerializeField] Pooler dummyPooler;
    public int doorCount;
    public bool isIncrease;
    
    
    // Start is called before the first frame update
    void Start()
    {
        lensText.text = doorCount.ToString();
        if (doorCount>=0)
        {
            isIncrease = true;
        }
        else
        {
            isIncrease = false;
        }
    }

    public void StartCalculate()
    {
        if (isIncrease)
        {
            IncreaseDummy();
        }
        else
        {
            DecreaseDummy();
        }
    }

    public void IncreaseDummy()
    {
        WeaponSpecs.instance.playerCount += doorCount;
        for (int i = 0; i < doorCount; i++)
        {
            GameObject newDummy = Instantiate(dummy, transform.position, Quaternion.identity);
            newDummy.transform.DOJump(transform.position + new Vector3(0, 0, 1.5f), 1f, 1, .5f);
            newDummy.tag = "CollactableDummy";
            newDummy.AddComponent<Rigidbody>();
            newDummy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
    public void DecreaseDummy()
    {
        Debug.Log("azaldý");
        if (WeaponSpecs.instance.playerCount>=1)
        {
            if (WeaponSpecs.instance.isPistol)
            {
                Debug.Log("1");
                WeaponSpecs.instance.playerCount -= Mathf.Abs(doorCount) ;
                for (int i = 0; i < Mathf.Abs(doorCount); i++)
                {
                    Transform lastDummy = WeaponSpecs.instance.gunsDummy[WeaponSpecs.instance.gunsDummy.Count-1];
                    lastDummy.parent = null;
                    WeaponSpecs.instance.gunsDummy.Remove(lastDummy);
                    lastDummy.transform.DOJump(transform.position + new Vector3(0, 0, 3), 2f, 1, .5f);
                    lastDummy.gameObject.AddComponent<Rigidbody>();
                }
            }
            else if (WeaponSpecs.instance.isRiffle)
            {
                Debug.Log("2");
                WeaponSpecs.instance.playerCount -= Mathf.Abs(doorCount);
                for (int i = 0; i < Mathf.Abs(doorCount); i++)
                {
                    Transform lastDummy = WeaponSpecs.instance.riffleDummy[WeaponSpecs.instance.riffleDummy.Count];
                    lastDummy.parent = null;
                    WeaponSpecs.instance.riffleDummy.Remove(lastDummy);
                    lastDummy.transform.DOJump(transform.position + new Vector3(0, 0, 3), 2f, 1, .5f);
                    lastDummy.gameObject.AddComponent<Rigidbody>();
                }
            }
            else if (WeaponSpecs.instance.isShoutgun)
            {
                Debug.Log("3");
                WeaponSpecs.instance.playerCount -= Mathf.Abs(doorCount);
                for (int i = 0; i < Mathf.Abs(doorCount); i++)
                {
                    Transform lastDummy = WeaponSpecs.instance.shootgunDummy[WeaponSpecs.instance.shootgunDummy.Count];
                    lastDummy.parent = null;
                    WeaponSpecs.instance.shootgunDummy.Remove(lastDummy);
                    lastDummy.transform.DOJump(transform.position + new Vector3(0, 0, 3), 2f, 1, .5f);
                    lastDummy.gameObject.AddComponent<Rigidbody>();
                }
            }
            else if (WeaponSpecs.instance.isGrenade)
            {
                Debug.Log("4");
                WeaponSpecs.instance.playerCount -= Mathf.Abs(doorCount);
                for (int i = 0; i < Mathf.Abs(doorCount); i++)
                {
                    Transform lastDummy = WeaponSpecs.instance.grenadeDuumy[WeaponSpecs.instance.grenadeDuumy.Count];
                    lastDummy.parent = null;
                    WeaponSpecs.instance.grenadeDuumy.Remove(lastDummy);
                    lastDummy.transform.DOJump(transform.position + new Vector3(0, 0, 3), 2f, 1, .5f);
                    lastDummy.gameObject.AddComponent<Rigidbody>();
                }
            }
            WeaponSpecs.instance.ReducationForGuns();
        }
      
    }
}
