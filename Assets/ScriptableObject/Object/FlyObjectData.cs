using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Object/FlyObject")]
public class FlyObjectData : GameData
{
    string KEY_HEIGHT = "FLY_OBJECT_HEIGHT";
    string KEY_SPEED = "FLY_OBJECT_SPEED";

    [Header("저장 데이터")]
    public float height;
    public float speed;

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
                height = float.Parse(column[0]);
                speed = float.Parse(column[1]);
            }
        }

        isLoaded = true;
    }

    public override void SaveDataToPrefs()
    {
        PlayerPrefs.SetFloat(key + KEY_HEIGHT, height);
        PlayerPrefs.SetFloat(key + KEY_SPEED, speed);
    }

    public override void LoadDataFromPrefs()
    {
        if (PlayerPrefs.HasKey(key + KEY_HEIGHT))
            height = PlayerPrefs.GetFloat(key + KEY_HEIGHT);

        if (PlayerPrefs.HasKey(key + KEY_SPEED))
            speed = PlayerPrefs.GetFloat(key + KEY_SPEED);

    }
}
