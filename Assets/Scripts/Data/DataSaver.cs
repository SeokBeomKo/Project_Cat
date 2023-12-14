using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    [SerializeField] public List<GameData> gameDataList;

    private void Awake() 
    {
        SaveAllData();
    }
    void SaveAllData() 
    {
        foreach(GameData data in gameDataList)
        {
            SaveDataSO(data);
        }
    }

    void SaveDataSO(GameData gameData)
    {
        gameData.SaveDataToPrefs();
    }

}
