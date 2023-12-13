using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BattleCat/CatAttackTime")]
public class BattleCatData : GameData
{
    string KEY_RESUMPTION_TIME = "BATTLE_CAT_RESUMPTION_TIME";
    string KEY_WAVE_TIME = "BATTLE_CAT_WAVE_TIME";
    string KEY_MOVE_SPEED = "BATTLE_CAT_MOVE_SPEED";

    [Header("저장 데이터")]
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

                PlayerPrefs.SetFloat(key + KEY_RESUMPTION_TIME, attackResumptionTime);
                PlayerPrefs.SetFloat(key + KEY_WAVE_TIME, waveAttackTime);
                PlayerPrefs.SetFloat(key + KEY_MOVE_SPEED, movementSpeed);
            }
        }
        isLoaded = true;
    }

    public override void LoadDataFromPrefs()
    {
        if(PlayerPrefs.HasKey(key + KEY_RESUMPTION_TIME))
            attackResumptionTime = PlayerPrefs.GetFloat(key + KEY_RESUMPTION_TIME);

        if(PlayerPrefs.HasKey(key + KEY_WAVE_TIME))
            waveAttackTime = PlayerPrefs.GetFloat(key + KEY_WAVE_TIME);
        
        if(PlayerPrefs.HasKey(key + KEY_MOVE_SPEED))
            movementSpeed = PlayerPrefs.GetFloat(key + KEY_MOVE_SPEED);
    }
}

