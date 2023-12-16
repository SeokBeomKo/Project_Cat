using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BattleCat/CleanlinessLikeability")]
public class CleanlinessLikeability : GameData
{
    string KEY_MAX_BODY_CLEANLINESS = "BATTLE_CAT_MAX_BODY_CLEANLINESS";
    string KEY_MAX_FOOT_CLEANLINESS = "BATTLE_MAX_FOOT_CLEANLINESS";
    string KEY_MAX_LIKEABILITY = "BATTLE_CAT_MAX_LIKEABILITY";

    [Header("저장 데이터")]
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
        PlayerPrefs.SetFloat(key + KEY_MAX_BODY_CLEANLINESS, maxBodyCleanliness);
        PlayerPrefs.SetFloat(key + KEY_MAX_FOOT_CLEANLINESS, maxFootCleanliness);
        PlayerPrefs.SetFloat(key + KEY_MAX_LIKEABILITY, maxLikeability);
    }

    public override void LoadDataFromPrefs()
    {
        if (PlayerPrefs.HasKey(key + KEY_MAX_BODY_CLEANLINESS))
            maxBodyCleanliness = PlayerPrefs.GetFloat(key + KEY_MAX_BODY_CLEANLINESS);

        if (PlayerPrefs.HasKey(key + KEY_MAX_FOOT_CLEANLINESS))
            maxFootCleanliness = PlayerPrefs.GetFloat(key + KEY_MAX_FOOT_CLEANLINESS);

        if (PlayerPrefs.HasKey(key + KEY_MAX_LIKEABILITY))
            maxLikeability = PlayerPrefs.GetFloat(key + KEY_MAX_LIKEABILITY);
    }

}
