using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BattleCat/CatAttackDamage")]

public class BattleCatDamageData : GameData
{
    [Header("저장 데이터")]
    public float damage;

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
                damage = float.Parse(column[0]);
            }
        }
    }
}
