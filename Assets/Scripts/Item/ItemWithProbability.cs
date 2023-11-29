using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemWithProbability
{
    public GameObject itemPrefab;
    [Range(0, 1)]
    public float probability;
}