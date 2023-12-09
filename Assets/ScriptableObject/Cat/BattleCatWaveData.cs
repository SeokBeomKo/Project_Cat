using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CatWave")]
public class BattleCatWaveData : GameData
{
    [Header("저장 데이터")]
    public float minAttackSize;
    public float maxAttackSize;
    public float growthSpeed;
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
                minAttackSize = float.Parse(column[0]);
                maxAttackSize = float.Parse(column[1]);
                growthSpeed = float.Parse(column[2]);
            }
        }
    }
}
