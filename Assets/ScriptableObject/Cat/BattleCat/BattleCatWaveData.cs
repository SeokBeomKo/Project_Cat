using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BattleCat/CatWave")]
public class BattleCatWaveData : GameData
{
    string KEY_MIN_ATTACK_SIZE = "BATTLE_CAT_MIN_ATTACK_SIZE";
    string KEY_MAX_ATTACK_SIZE = "BATTLE_CAT_MAX_ATTACK_SIZE";
    string KEY_GROWTH_SPEED = "BATTLE_CAT_GROWTH_SPEED";

    [Header("���� ������")]
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

                PlayerPrefs.SetFloat(key + KEY_MIN_ATTACK_SIZE, minAttackSize);
                PlayerPrefs.SetFloat(key + KEY_MAX_ATTACK_SIZE, maxAttackSize);
                PlayerPrefs.SetFloat(key + KEY_GROWTH_SPEED, growthSpeed);
            }
        }
        isLoaded = true;
    }

    public override void LoadDataFromPrefs()
    {
        if(PlayerPrefs.HasKey(key + KEY_MIN_ATTACK_SIZE))
            minAttackSize = PlayerPrefs.GetInt(key + KEY_MIN_ATTACK_SIZE);

        if(PlayerPrefs.HasKey(key + KEY_MAX_ATTACK_SIZE))
            maxAttackSize = PlayerPrefs.GetInt(key + KEY_MAX_ATTACK_SIZE);
        
        if(PlayerPrefs.HasKey(key + KEY_GROWTH_SPEED))
            growthSpeed = PlayerPrefs.GetInt(key + KEY_GROWTH_SPEED);
    }
    
}
