using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player")]
public class PlayerData : GameData
{
    [Header("저장 데이터")]
    public int maxHealth;
    public float moveSpeed;
    public float rollSpeed;
    public float jumpForce;

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
                maxHealth  = int.Parse(column[0]);
                moveSpeed  = float.Parse(column[1]);
                rollSpeed  = float.Parse(column[2]);
                jumpForce  = float.Parse(column[3]);
            }
        }
        isLoaded = true;
    }
}
