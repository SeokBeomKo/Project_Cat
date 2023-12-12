using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponData : GameData
{
    [Header("저장 데이터")]
    public int minDamage;
    public int maxDamage;
    public int maxBullet;
    public float shootDelay;

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
                minDamage  = int.Parse(column[0]);
                maxDamage  = int.Parse(column[1]);
                maxBullet       = int.Parse(column[2]);
                shootDelay      = float.Parse(column[3]);
            }
        }
        
        isLoaded = true;
    }
}
