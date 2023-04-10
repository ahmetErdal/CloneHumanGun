using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ExpoBarrel : MonoBehaviour
{
    public int barrelCount;
    public TextMeshProUGUI barrelCountText;
    public ParticleSystem explosionVFX;

    public bool isExplosion = false;
    void Start()
    {
        barrelCountText.text = barrelCount.ToString();
    }

}
