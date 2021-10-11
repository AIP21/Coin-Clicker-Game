using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Wait());
    }

    public void Begin()
    {
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Go");
        yield return new WaitForSeconds(1);
        print("Deativated arrow object");
        gameObject.SetActive(false);
    }
}