using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Object/HP")]
public class HPData : GameData
{
    string KEY_HP = "HP";

    [Header("저장 데이터")]
    public float hp;

    public override void ProcessData(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columnSize; j++)
            {
                hp = float.Parse(column[0]);
            }
        }

        isLoaded = true;
    }

    public override void SaveDataToPrefs()
    {
        PlayerPrefs.SetFloat(key + KEY_HP, hp);
    }

    public override void LoadDataFromPrefs()
    {

        if (PlayerPrefs.HasKey(key + KEY_HP))
            hp = PlayerPrefs.GetFloat(key + KEY_HP);

    }

}
