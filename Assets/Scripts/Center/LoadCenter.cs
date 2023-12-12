using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCenter : MonoBehaviour
{
    [SerializeField]
    public DataLoader dataLoader;

    [SerializeField]
    public StartScene startScene;

    private void Start() 
    {
        dataLoader.OnDataLoad += ChangeScene;
    }

    public void ChangeScene()
    {
    }
}
