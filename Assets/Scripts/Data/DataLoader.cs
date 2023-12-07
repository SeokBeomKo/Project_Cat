using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    [SerializeField] public DataReceiver dataReceiver;

    [SerializeField] public WeaponData soapRifle;
    public string data;
    private void Awake() 
    {
        LoadWeaponSO(soapRifle);
    }

    void LoadWeaponSO(WeaponData weapon)
    {
        StartCoroutine(dataReceiver.DataReceive(weapon.colrowStartNumber, weapon.colrowEndNumber, weapon.sheetNumber, (data) =>
        {
            // data 변수를 사용하여 원하는 작업 수행
            Debug.Log(data);
        }));
    }

    void SetWeaponSO(WeaponData weapon, string tsv)
    {
        Debug.Log(tsv);
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;
        Debug.Log(columnSize);

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columnSize; j++)
            {
                weapon.attackPower = int.Parse(column[0]);
                weapon.maxBullet = int.Parse(column[1]);
                weapon.shootDelay = int.Parse(column[2]);
            }
        }
    }
}
