using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEnter : MonoBehaviour
{
    public delegate void MazeEnterHandle();
    public event MazeEnterHandle OnMazeEnter;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("a");
            OnMazeEnter?.Invoke();
        }
    }
}
