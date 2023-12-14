using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BattleCat/CleanlinessLikeability")]
public class CleanlinessLikeability : GameData
{
    string KEY_MIN_ATTACK_SIZE = "BATTLE_CAT_MIN_ATTACK_SIZE";
    string KEY_MAX_ATTACK_SIZE = "BATTLE_CAT_MAX_ATTACK_SIZE";
    string KEY_GROWTH_SPEED = "BATTLE_CAT_GROWTH_SPEED";

    [Header("���� ������")]
    public float maxBodyCleanliness;
    public float maxFootCleanliness;
    public float maxLikeability;
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
                maxBodyCleanliness = float.Parse(column[0]);
                maxFootCleanliness = float.Parse(column[1]);
                maxLikeability = float.Parse(column[2]);

                SaveDataToPrefs();
            }
        }
        isLoaded = true;
    }

    public override void SaveDataToPrefs()
    {
        PlayerPrefs.SetFloat(key + KEY_MIN_ATTACK_SIZE, maxBodyCleanliness);
        PlayerPrefs.SetFloat(key + KEY_MAX_ATTACK_SIZE, maxFootCleanliness);
        PlayerPrefs.SetFloat(key + KEY_GROWTH_SPEED, maxLikeability);
    }

    public override void LoadDataFromPrefs()
    {
        if (PlayerPrefs.HasKey(key + KEY_MIN_ATTACK_SIZE))
            maxBodyCleanliness = PlayerPrefs.GetFloat(key + KEY_MIN_ATTACK_SIZE);

        if (PlayerPrefs.HasKey(key + KEY_MAX_ATTACK_SIZE))
            maxFootCleanliness = PlayerPrefs.GetFloat(key + KEY_MAX_ATTACK_SIZE);

        if (PlayerPrefs.HasKey(key + KEY_GROWTH_SPEED))
            maxLikeability = PlayerPrefs.GetFloat(key + KEY_GROWTH_SPEED);
    }

}
