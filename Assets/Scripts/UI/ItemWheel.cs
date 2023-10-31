using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWheel : MonoBehaviour
{
    public Transform center; // 중앙을 기준으로 마우스 각도 계산
    public Transform selectObject; // 선택된 거 회전

    public GameObject itemMenu; // 아이템 휠 메뉴
    bool isActive; // 메뉴의 활성 상태

    public TextMeshProUGUI itemName; // 아이템 이름
    public TextMeshProUGUI itemExplanation; // 아이템 설명

    public string[] itemNameArray;
    public string[] itemExplanationArray;

    public Transform[] itemSlotArray; // 아이템 이미지 확대
    public Transform[] energySlotArray; // 운동에너지 이미지 확대

    public Transform itemMin, itemMax; // 아이템 휠 원 경계
    public Transform energyMin, energyMax; // 운동에너지 원 경계

    public GameObject kineticEnergyMenu; // 운동 에너지 선택시 메뉴창
    bool isMenuActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        itemMenu.SetActive(false);

        isMenuActive = false;
        kineticEnergyMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2)) // 마우스 휠 버튼 무기창 활성화
        {
            isActive = !isActive;
            if (isActive && !isMenuActive)
                itemMenu.SetActive(true);
            if(!isActive)
                itemMenu.SetActive(false);
        }

        if (isActive)
        {
            // 중앙으로부터의 마우스 거리가 경계값 안에 있는지 확인
            if (Vector3.Distance(Input.mousePosition, center.position) < Vector3.Distance(itemMax.position, center.position) && Vector3.Distance(Input.mousePosition, center.position) > Vector3.Distance(itemMin.position, center.position))
            {
                // 각도 계산 
                Vector2 delta = center.position - Input.mousePosition; // 중앙에서부터 마우스 위치의 차이
                float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg; // 각도 구하는 공식 
                angle += 180; // 각도가 -180에서 180이므로 180 더해줌 (각도 쉽게 처리하기 위해서)

                int currentItem = 0; // 아이템 번호 확인

                for (int i = 0; i < 360; i += 60)
                {
                    if (angle >= i && angle < i + 60)
                    {
                        selectObject.eulerAngles = new Vector3(0, 0, i); // Z축 주위로 회전


                        itemName.text = itemNameArray[currentItem];
                        itemExplanation.text = itemExplanationArray[currentItem];

                        foreach (Transform t in itemSlotArray)
                        {
                            t.transform.localScale = new Vector3(1, 1, 1); // 모든 이미지 크기 (1, 1, 1)로 설정
                        }
                        itemSlotArray[currentItem].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

                        if (Input.GetMouseButtonDown(0))
                        { 
                            Debug.Log(angle);
                            Debug.Log(itemNameArray[currentItem] + "선택");
                            isActive = false;
                            itemMenu.SetActive(false);

                            // 운동에너지 선택시 새로운 팝업창 생성
                            if(angle >= 300 && angle < 360)
                            {
                                isMenuActive = true;
                                kineticEnergyMenu.SetActive(true);
                            }
                        }
                    }
                    currentItem++;
                }
            }
            else
            {
                itemName.text = "MENU";
                itemExplanation.text = " ";

                foreach (Transform t in itemSlotArray)
                {
                    t.transform.localScale = new Vector3(1, 1, 1); // 모든 이미지 크기 (1, 1, 1)로 설정
                }

                /*if (Input.GetMouseButtonDown(0))
                {
                    itemName.text = "MENU";
                    itemExplanation.text = " ";

                    isActive = false;
                    itemMenu.SetActive(false);
                }*/
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("선택 취소");
                isActive = false;
                itemMenu.SetActive(false);
            }
        }

        if (isMenuActive)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("운동 에너지 선택 취소");
                isMenuActive = false;
                kineticEnergyMenu.SetActive(false);

                isActive = true;
                itemMenu.SetActive(true);
            }
            else if (Input.GetMouseButtonDown(2))
            {
                Debug.Log("선택 취소");
                isMenuActive = false;
                kineticEnergyMenu.SetActive(false);
            }

            if (Vector3.Distance(Input.mousePosition, center.position) < Vector3.Distance(energyMax.position, center.position) && Vector3.Distance(Input.mousePosition, center.position) > Vector3.Distance(energyMin.position, center.position))
            {
                // 각도 계산 
                Vector2 delta2 = center.position - Input.mousePosition; // 중앙에서부터 마우스 위치의 차이
                float angle2 = Mathf.Atan2(delta2.y, delta2.x) * Mathf.Rad2Deg; // 각도 구하는 공식 
                angle2 += 180;

                if (angle2 > 90 && angle2 < 270)
                {
                    energySlotArray[0].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                    energySlotArray[1].transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    energySlotArray[0].transform.localScale = new Vector3(1, 1, 1);
                    energySlotArray[1].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                }
                        /*if (Input.GetMouseButtonDown(0))
                        {
                            Debug.Log(energySlotArray[currentItem2] + "선택");
                            isMenuActive = false;
                            kineticEnergyMenu.SetActive(false);
                        }*/
                
            }
            else
            {
                foreach (Transform e in energySlotArray)
                {
                    e.transform.localScale = new Vector3(1, 1, 1);
                }
            }

        }

    }
}

// Atan2의 반환값은 라디안 이기 때문에 도수법을 사용하기 위해서 Mathf.Rad2Deg 를 이용
// Mathf.Rad2Deg는 라디안을 각도로 변환해주는 상수를 나타내고, 그값은 360 / ( PI * 2 )와 같다.


