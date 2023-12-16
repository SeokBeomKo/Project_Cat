using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Object/VirusStatic")]
public class VirusStaticData : GameData
{
    string KEY_HP = "VIRUS_STATIC_HP";
    string KEY_DAMAGE = "VIRUS_STATIC_DAMAGE";

    [Header("저장 데이터")]
    public float hp;
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
                hp = int.Parse(column[0]);
                damage = int.Parse(column[1]);
            }
        }
        SaveDataToPrefs();
        isLoaded = true;
    }

    public override void SaveDataToPrefs()
    {
        PlayerPrefs.SetFloat(key + KEY_HP, hp);
        PlayerPrefs.SetFloat(key + KEY_DAMAGE, damage);
    }

    public override void LoadDataFromPrefs()
    {
        if (PlayerPrefs.HasKey(key + KEY_HP))
            hp = PlayerPrefs.GetInt(key + KEY_HP);

        if (PlayerPrefs.HasKey(key + KEY_DAMAGE))
            damage = PlayerPrefs.GetInt(key + KEY_DAMAGE);
    }
}
