using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Object/VirusProjectile")]
public class VirusProjectileData : GameData
{
    string KEY_SPEED = "VIRUS_PROJECTILE_SPEED";
    string KEY_DAMAGE = "VIRUS_PROJECTILE_DAMAGE";

    [Header("저장 데이터")]
    public float speed;
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
                speed = float.Parse(column[0]);
                damage = float.Parse(column[1]);
            }
        }
        SaveDataToPrefs();
        isLoaded = true;
    }

    public override void SaveDataToPrefs()
    {
        PlayerPrefs.SetFloat(key + KEY_SPEED, speed);
        PlayerPrefs.SetFloat(key + KEY_DAMAGE, damage);
    }

    public override void LoadDataFromPrefs()
    {
        if (PlayerPrefs.HasKey(key + KEY_SPEED))
            speed = PlayerPrefs.GetFloat(key + KEY_SPEED);

        if (PlayerPrefs.HasKey(key + KEY_DAMAGE))
            damage = PlayerPrefs.GetFloat(key + KEY_DAMAGE);
    }
}
