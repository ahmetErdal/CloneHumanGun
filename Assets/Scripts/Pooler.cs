using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public GameObject defaultGameObject;
    public int poolCount = 1;
    private List<GameObject> list;

    int lastIndex = 0;
    private void Awake()
    {
        InitPooler();
    }
    public void InitPooler()
    {
        if (list == null)
        {
            list = new List<GameObject>();
        }
        defaultGameObject.transform.parent = transform;
        while (list.Count<poolCount)
        {
            GameObject go = Instantiate(defaultGameObject);
            go.SetActive(false);
            go.transform.parent = transform;

            list.Add(go);
        }
        defaultGameObject.SetActive(false);
    }

    public GameObject GetGo()
    {
        GameObject go = null;

        if (!Application.isPlaying)
        {
            go = Instantiate(defaultGameObject);
            go.transform.parent = transform;
            go.SetActive(false);

            return go;
        }

        for (int i = 0; i < list.Count; i++)
        {
            lastIndex++;
            if (lastIndex > list.Count - 1)
                lastIndex = 0;
            if (list[lastIndex].gameObject.activeSelf)
                continue;
            return list[lastIndex];
        }


        go = Instantiate(defaultGameObject);
        go.transform.parent = transform;
        go.SetActive(false);

        list.Add(go);

        return go;
    }

    public T GetGo<T>() where T : MonoBehaviour
    {
        GameObject go = GetGo();

        if (go == null)
            return null;

        return go.GetComponent<T>();
    }

}
