using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMaze : MonoBehaviour
{
    public Transform ToyBox;
    private Vector3 startToyBoxPosition;
    
    private void Start()
    {
        startToyBoxPosition = ToyBox.position;
    }

    private void Update()
    {
        if(startToyBoxPosition != ToyBox.position)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }


}
