using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ChaseCat")]
public class ChaseCatData : GameData
{
    string KEY_MOVE_SPEED = "CHASE_CAT_MOVE_SPEED";
    [Header("저장 데이터")]
    public float moveSpeed;

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
                moveSpeed = float.Parse(column[0]);

                PlayerPrefs.SetFloat(key + KEY_MOVE_SPEED, moveSpeed);
            }
        }
        isLoaded = true;
    }

    public override void LoadDataFromPrefs()
    {
        if(PlayerPrefs.HasKey(key + KEY_MOVE_SPEED))
            moveSpeed = PlayerPrefs.GetFloat(key + KEY_MOVE_SPEED);
    }
}

