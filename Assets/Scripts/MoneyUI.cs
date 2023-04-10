using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    #region Singleton
    public static MoneyUI instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion
    private Camera mainCamera; 
    public Transform iconTransform;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 Get›conPosition(Vector3 target)
    {
        Vector3 uiPos = iconTransform.position;
        uiPos.z = (target - mainCamera.transform.position).z;
        Vector3 result = mainCamera.ScreenToWorldPoint(uiPos);
        return result;
    }
}
