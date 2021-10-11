using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinButton : MonoBehaviour
{

    public Vector3 scaleAmount;
    public float time;

    private Vector3 origScale;

    private void Start()
    {
        origScale = transform.localScale;
    }

    public void UIButtonClick()
    {
        print("Clicked on the Coin Button");
        LeanTweenExt.LeanScale(transform, transform.localScale + scaleAmount, time).setEasePunch().setOnComplete(() =>
        {
            transform.localScale = origScale;
        });
    }
}