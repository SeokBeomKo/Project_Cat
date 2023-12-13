using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameData : ScriptableObject
{
    [Header("해당 시트의 넘버링")]
    public string sheetNumber;
    [Header("시작 행렬")]
    public string colrowStartNumber;
    [Header("종료 행렬")]
    public string colrowEndNumber;
    
    public string key;

    [HideInInspector]
    public bool isLoaded = false;
    
    public abstract void ProcessData(string tsv);
    public abstract void LoadDataFromPrefs();
}
