using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectEnergyUse : MonoBehaviour
{
    public GameObject Player;
    public GameObject ProtectEnergy;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateProtectEnergy();
        }
    }

    void CreateProtectEnergy()
    {
        {
            // 플레이어의 현재 위치에 오브젝트 생성
            Vector3 playerPosition = Player.transform.position;
            playerPosition.y += 0.25f;
            GameObject newObject = Instantiate(ProtectEnergy, playerPosition, Quaternion.identity, Player.transform);

            // 5초 뒤에 생성된 오브젝트 파괴
            Destroy(newObject, 5f);
        }
    }


}
