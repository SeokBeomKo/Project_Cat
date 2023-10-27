using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheel : MonoBehaviour
{
    public Transform center; // 중앙을 기준으로 마우스 각도 계산
    public Transform selectObject; // 선택된 거 회전

    public GameObject weaponMenu;

    bool isActive; // 메뉴의 활성 상태



    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2)) // 마우스 휠 버튼 무기창 활성화
        {
            isActive = !isActive;
            if (isActive)
                weaponMenu.SetActive(true);
            else
                weaponMenu.SetActive(false);

            if(isActive)
            {
                // 각도 계산 
                Vector2 delta = center.position - Input.mousePosition; // 중앙에서부터 마우스 위치의 차이
                float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg; // 각도 구하는 공식 
                angle += 180; // 각도가 -180에서 180이므로 180 더해줌 (각도 쉽게 처리하기 위해서)

                for(int i = 0; i < 360; i += 90) 
                {
                    if(angle >= i && angle < i + 45)
                    {
                        selectObject.eulerAngles = new Vector3(0, 0, i); // Z축 주위로 회전

                    }
                }
            }
        }
    }
}

// Atan2의 반환값은 라디안 이기 때문에 도수법을 사용하기 위해서 Mathf.Rad2Deg 를 이용
// Mathf.Rad2Deg는 라디안을 각도로 변환해주는 상수를 나타내고, 그값은 360 / ( PI * 2 )와 같다.


