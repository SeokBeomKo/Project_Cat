using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/*
데이터 시트 : https://docs.google.com/spreadsheets/d/1j1Up9jrjtqlqc1WICDEWxQm8xlup9lxVhw3hOlREtN8/export?format=tsv
칸 영역 : &range=A3:C3 (A3 ~ C3)
시트 영역 : &gid=48646553 (시트마다 숫자가 다름)
*/

public class DataReceiver : MonoBehaviour
{
    private string BASE_URL = "https://docs.google.com/spreadsheets/d/1j1Up9jrjtqlqc1WICDEWxQm8xlup9lxVhw3hOlREtN8/export?format=tsv";

    public IEnumerator DataReceive(string start, string end, string sheet, Action<string> callback) 
    {
        string URL = BASE_URL + "&range=" + start + ":" + end + "&gid=" + sheet;
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) // 요청이 실패한 경우
        {
            Debug.LogError("Failed to receive data: " + www.error);
            yield break;
        }
        
        string data = www.downloadHandler.text;
        callback?.Invoke(data); // 콜백 실행하여 데이터 전달
    }
}
