using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Object/WaterChargeAmount")]
public class WaterChargeAmountData : GameData
{
    string KEY_AMOUNT = "WATER_CHARGE_AMOUNT";

    [Header("저장 데이터")]
    public float amount;

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
                amount = float.Parse(column[0]);
            }
        }
        SaveDataToPrefs();

        isLoaded = true;
    }

    public override void SaveDataToPrefs()
    {
        PlayerPrefs.SetFloat(key + KEY_AMOUNT, amount);
    }

    public override void LoadDataFromPrefs()
    {
        if (PlayerPrefs.HasKey(key + KEY_AMOUNT))
            amount = PlayerPrefs.GetFloat(key + KEY_AMOUNT);
        

    }
}
