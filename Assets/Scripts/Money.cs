using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private bool collected = false;

    private void FixedUpdate()
    {
        if (collected)
        {
            Vector3 targetPos = MoneyUI.instance.Get›conPosition(transform.position);
           
            transform.position = Vector4.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
        }
        else
        {
           // gameObject.SetActive(false);
        }
    }
    public void SetCollect()
    {
        collected = true;
    }
}
