using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int index;

    public bool isForwardWall = true;
    public bool isBackWall = true;
    public bool isLeftWall = true;
    public bool isRightWall = true;

    public GameObject ForwardWall;
    public GameObject BackWall;
    public GameObject LeftWall;
    public GameObject RightWall;

    void Start()
    {
        ShowWalls();   
    }

    public void ShowWalls()
    {
        ForwardWall.SetActive(isForwardWall);
        BackWall.SetActive(isBackWall);
        LeftWall.SetActive(isLeftWall);
        RightWall.SetActive(isRightWall);
    }

    public bool CheckAllWall()
    {
        return isForwardWall && isBackWall && isLeftWall && isRightWall;
    }
  
}
