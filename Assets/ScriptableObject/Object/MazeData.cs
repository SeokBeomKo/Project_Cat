using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Object/Maze")]
public class MazeData : GameData
{
    string KEY_WIDTH = "MAZE_WIDTH";
    string KEY_HEIGHT = "MAZE_HEIGHT";

    [Header("저장 데이터")]
    public int width;
    public int height;

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
                width = int.Parse(column[0]);
                height = int.Parse(column[1]);
            }
        }
        SaveDataToPrefs();
        isLoaded = true;
    }

    public override void SaveDataToPrefs()
    {
        PlayerPrefs.SetInt(key + KEY_WIDTH, width);
        PlayerPrefs.SetInt(key + KEY_HEIGHT, height);
    }

    public override void LoadDataFromPrefs()
    {
        if (PlayerPrefs.HasKey(key + KEY_WIDTH))
            width = PlayerPrefs.GetInt(key + KEY_WIDTH);

        if (PlayerPrefs.HasKey(key + KEY_HEIGHT))
            height = PlayerPrefs.GetInt(key + KEY_HEIGHT);


    }
}
