using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player")]
public class PlayerData : GameData
{
    string KEY_MAX_HEALTH = "PLAYER_MAX_HEALTH";
    string KEY_MOVE_SPEED = "PLAYER_MOVE_SPEED";
    string KEY_ROLL_SPEED = "PLAYER_ROLL_SPEED";
    string KEY_JUMP_FORCE = "PLAYER_JUMP_FORCE";

    [Header("저장 데이터")]
    public int maxHealth;
    public float moveSpeed;
    public float rollSpeed;
    public float jumpForce;

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
                maxHealth  = int.Parse(column[0]);
                moveSpeed  = float.Parse(column[1]);
                rollSpeed  = float.Parse(column[2]);
                jumpForce  = float.Parse(column[3]);

                PlayerPrefs.SetInt(key + KEY_MAX_HEALTH, maxHealth);
                PlayerPrefs.SetFloat(key + KEY_MOVE_SPEED, moveSpeed);
                PlayerPrefs.SetFloat(key + KEY_ROLL_SPEED, rollSpeed);
                PlayerPrefs.SetFloat(key + KEY_JUMP_FORCE, jumpForce);
            }
        }
        isLoaded = true;
    }

    public override void LoadDataFromPrefs()
    {
        if(PlayerPrefs.HasKey(key + KEY_MAX_HEALTH))
            maxHealth = PlayerPrefs.GetInt(key + KEY_MAX_HEALTH);

        if(PlayerPrefs.HasKey(key + KEY_MOVE_SPEED))
            moveSpeed = PlayerPrefs.GetInt(key + KEY_MOVE_SPEED);
        
        if(PlayerPrefs.HasKey(key + KEY_ROLL_SPEED))
            rollSpeed = PlayerPrefs.GetInt(key + KEY_ROLL_SPEED);

        if(PlayerPrefs.HasKey(key + KEY_JUMP_FORCE))
            jumpForce = PlayerPrefs.GetFloat(key + KEY_JUMP_FORCE);
    }
}
