using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPose : MonoBehaviour
{
    public string poseName;
    Animator dummyAnimator;
    private void Start()
    {
        dummyAnimator = this.GetComponent<Animator>();
        dummyAnimator.SetTrigger(poseName);
    }
}
