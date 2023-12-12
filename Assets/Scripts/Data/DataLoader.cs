using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public delegate void DataLoadHandle();
    public event DataLoadHandle OnDataLoad;
    [SerializeField] public DataReceiver dataReceiver;
    [SerializeField] public List<GameData> gameDataList;
    private void Awake() 
    {
        foreach(GameData data in gameDataList)
        {
            data.isLoaded = false;
        }
        StartCoroutine(LoadAllData());
    }

    private IEnumerator LoadAllData() 
    {
        foreach(GameData data in gameDataList)
        {
            yield return LoadDataSO(data);
        }

        bool allDataLoaded = false; 
        while(!allDataLoaded)
        {
            allDataLoaded = AllDataLoaded();
        }

        if(allDataLoaded)
        {
            Debug.Log(allDataLoaded);
            OnDataLoad?.Invoke();
        }
    }

    bool AllDataLoaded()
    {
        bool dataLoad = true;
        foreach(GameData data in gameDataList)
        {
            Debug.Log(data.isLoaded);
            if(!data.isLoaded)
            {
                dataLoad = false;
                break;
            }
        }
        return dataLoad;
    }

    IEnumerator LoadDataSO(GameData gameData)
    {
        yield return dataReceiver.DataReceive(gameData.colrowStartNumber, gameData.colrowEndNumber, gameData.sheetNumber, (data) =>
        {
            gameData.ProcessData(data);
        });
    }
}
