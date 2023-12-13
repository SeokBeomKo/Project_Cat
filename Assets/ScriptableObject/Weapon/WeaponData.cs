using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponData : GameData
{
    string KEY_MIN_DAMAGE = "WEAPON_MIN_DAMAGE";
    string KEY_MAX_DAMAGE = "WEAPON_MAX_DAMAGE";
    string KEY_MAX_BULLET = "WEAPON_MAX_BULLET";
    string KEY_SHOOT_DELAY = "WEAPON_SHOOT_DELAY";

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

                PlayerPrefs.SetInt(key + KEY_MIN_DAMAGE, minDamage);
                PlayerPrefs.SetInt(key + KEY_MAX_DAMAGE, maxDamage);
                PlayerPrefs.SetInt(key + KEY_MAX_BULLET, maxBullet);
                PlayerPrefs.SetFloat(key + KEY_SHOOT_DELAY, shootDelay);
            }
        }
        
        isLoaded = true;
    }

    public override void LoadDataFromPrefs()
    {
        if(PlayerPrefs.HasKey(key + KEY_MIN_DAMAGE))
            minDamage = PlayerPrefs.GetInt(key + KEY_MIN_DAMAGE);

        if(PlayerPrefs.HasKey(key + KEY_MAX_DAMAGE))
            maxDamage = PlayerPrefs.GetInt(key + KEY_MAX_DAMAGE);
        
        if(PlayerPrefs.HasKey(key + KEY_MAX_BULLET))
            maxBullet = PlayerPrefs.GetInt(key + KEY_MAX_BULLET);

        if(PlayerPrefs.HasKey(key + KEY_SHOOT_DELAY))
            shootDelay = PlayerPrefs.GetFloat(key + KEY_SHOOT_DELAY);
    }
}
