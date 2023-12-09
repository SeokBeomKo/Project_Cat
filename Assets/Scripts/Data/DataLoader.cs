using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    [SerializeField] public DataReceiver dataReceiver;
    [SerializeField] public List<GameData> gameDataList;
    private void Awake() 
    {
        StartCoroutine(LoadAllData());
    }

    private IEnumerator LoadAllData() 
    {
        foreach(GameData data in gameDataList)
        {
            yield return LoadDataSO(data);
        }
    }

    IEnumerator LoadDataSO(GameData gameData)
    {
        yield return dataReceiver.DataReceive(gameData.colrowStartNumber, gameData.colrowEndNumber, gameData.sheetNumber, (data) =>
        {
            gameData.ProcessData(data);
        });
    }
}
