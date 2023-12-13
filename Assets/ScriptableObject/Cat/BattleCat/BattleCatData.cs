using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BattleCat/CatAttackTime")]
public class BattleCatData : GameData
{
    [Header("���� ������")]
    public float attackResumptionTime;
    public float waveAttackTime;
    public float movementSpeed;

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
                attackResumptionTime = float.Parse(column[0]);
                waveAttackTime = float.Parse(column[1]);
                movementSpeed = float.Parse(column[2]);
            }
        }
        isLoaded = true;
    }
}

