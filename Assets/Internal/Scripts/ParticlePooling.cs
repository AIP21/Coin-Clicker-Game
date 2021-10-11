using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticlePooling : MonoBehaviour
{
    public List<GameObject> pooledUpArrows;
    public List<GameObject> pooledDownArrows;
    public GameObject UpArrow;
    public GameObject DownArrow;
    public int amountToPool;
    public Transform canvas;

    void Start()
    {
        pooledUpArrows = new List<GameObject>();
        GameObject tmpUp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmpUp = Instantiate(UpArrow, canvas, false) as GameObject;
            tmpUp.SetActive(false);
            pooledUpArrows.Add(tmpUp);
        }
        pooledDownArrows = new List<GameObject>();
        GameObject tmpDown;
        for (int i = 0; i < amountToPool; i++)
        {
            tmpDown = Instantiate(DownArrow, canvas, false) as GameObject;
            tmpDown.SetActive(false);
            pooledDownArrows.Add(tmpDown);
        }
    }

    public GameObject GetPooledUpArrow()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledUpArrows[i].activeInHierarchy)
            {
                print("Found pooled up arrow: " + pooledUpArrows[i]);
                return pooledUpArrows[i];
            }
        }
        return null;
    }

    public GameObject GetPooledDownArrow()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledDownArrows[i].activeInHierarchy)
            {
                print("Found pooled down arrow: " + pooledDownArrows[i]);
                return pooledDownArrows[i];
            }
        }
        return null;
    }
}