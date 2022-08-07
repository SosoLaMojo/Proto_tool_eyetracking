using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class Time_bar : MonoBehaviour
{
    public GameObject bar;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        AnimateBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, time);
    }
}
