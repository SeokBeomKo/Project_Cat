using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/*
데이터 시트 : https://docs.google.com/spreadsheets/d/1j1Up9jrjtqlqc1WICDEWxQm8xlup9lxVhw3hOlREtN8/export?format=tsv
칸 영역 : &range=A3:C3 (A3 ~ C3)
시트 영역 : &gid=48646553 (시트마다 숫자가 다름)
*/

public class GoogleSheetManager : Singleton<ObjectPoolManager>
{
    const string URL = "https://docs.google.com/spreadsheets/d/1j1Up9jrjtqlqc1WICDEWxQm8xlup9lxVhw3hOlREtN8/export?format=tsv&range=A3:C3&gid=48646553";

    IEnumerator Start() 
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }
}
