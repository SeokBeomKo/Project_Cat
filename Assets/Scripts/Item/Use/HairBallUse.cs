using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HairBallUse : MonoBehaviour
{
    public GameObject Player;
    public GameObject HairBall;

    [Header("HP")]
    public float HP = 5;
    public ObjectHPbar objectHPbar;

    private void Start()
    {
        objectHPbar.SetHP(HP);
        objectHPbar.ChechHP();
    }

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
        GameObject newObject = Instantiate(HairBall, playerPosition, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball")) // 
        {
            Hit();
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) // 고양이 충돌
        {
            HP = 0;
            Check();
        }
    }

    private void Hit()
    {
        objectHPbar.Demage(1);
        HP = objectHPbar.GetHP();
        Check();
    }

    private void Check()
    {
        if (HP == 0)
        {
            transform.gameObject.SetActive(false);

        }
    }
}
