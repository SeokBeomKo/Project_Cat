using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HairBallUse : MonoBehaviour
{
    public GameObject Player;
    public GameObject HairBall;
    GameObject newObject;

    [Header("HP")]
    public float HP = 5;
    public ObjectHPbar objectHPbar;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log("Input 9");
            CreateHairBall();
        }
    }

    void CreateHairBall()
    {
        Debug.Log("CreateHairBall");

        // 플레이어의 현재 위치에 오브젝트 생성
        Vector3 playerPosition = Player.transform.position;
        playerPosition.z += 0.7f;
        newObject = Instantiate(HairBall, playerPosition, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(newObject);
    }
  
}
