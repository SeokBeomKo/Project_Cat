using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectEnergyUse : MonoBehaviour
{
    public GameObject ProtectEnergy;

    public void CreateProtectEnergy(Vector3 position, Transform transform)
    {
        {
            // 플레이어의 현재 위치에 오브젝트 생성
            Vector3 playerPosition = position;
            playerPosition.y += 0.25f;
            GameObject newObject = Instantiate(ProtectEnergy, playerPosition, Quaternion.identity, transform);

            // 5초 뒤에 생성된 오브젝트 파괴
            Destroy(newObject, 5f);
        }
    }


}
