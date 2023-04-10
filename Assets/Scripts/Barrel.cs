using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Barrel : MonoBehaviour
{
    public int barrelCount;
    public TextMeshProUGUI barrelText;
    void Start()
    {
        barrelText.text = barrelCount.ToString();
    }

   
}
