using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWheel : MonoBehaviour
{
    public Transform center; // 중앙을 기준으로 마우스 각도 계산
    public Transform selectObject; // 선택된 거 회전
    //public GameObject temp;

    public GameObject itemMenu; // 아이템 휠 메뉴
    bool isMenuActive; // 메뉴의 활성 상태

    public TextMeshProUGUI itemName; // 아이템 이름
    public TextMeshProUGUI itemExplanation; // 아이템 설명

    public string[] itemNameArray;
    public string[] itemExplanationArray;

    public Transform[] itemSlotArray; // 아이템 이미지 확대
    public Transform[] energySlotArray; // 운동에너지 이미지 확대

    public Transform itemMin, itemMax; // 아이템 휠 원 경계
    public Transform energyMin, energyMax; // 운동에너지 원 경계

    public GameObject kineticEnergyMenu; // 운동 에너지 선택시 메뉴창
    bool isEnergyMenuActive; // 운동에너지 메뉴 활성 상태

    public GameObject selectEnergyLeft;
    public GameObject selectEnergyRight;

    public TextMeshProUGUI moveSpeed; // 이동속도 설명
    public TextMeshProUGUI attackSpeed; // 공격속도 설명

    bool hasRightMouseClicked = false;


    void Start()
    {
        DeactivateMenu();
        DeactivateEnergyMenu();
        selectObject.gameObject.SetActive(false);
        selectEnergyLeft.SetActive(false);
        selectEnergyRight.SetActive(false);
        //temp.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2)) // 마우스 휠 버튼 무기창 활성화
        {
            isMenuActive = !isMenuActive;

            if (isMenuActive && !isEnergyMenuActive)
                itemMenu.SetActive(true);
            if (!isMenuActive)
                itemMenu.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            hasRightMouseClicked = false;
        }

        if (isMenuActive)
        {
            UpdateMenu();
        }

        if (isEnergyMenuActive)
        {
            UpdateEnergyMenu();
        }
    }



    void ActivateMenu()
    {
        isMenuActive = true;
        itemMenu.SetActive(true);
    }

    void DeactivateMenu()
    {
        isMenuActive = false;
        itemMenu.SetActive(false);
    }

    void ActivateEnergyMenu()
    {
        isEnergyMenuActive = true;
        kineticEnergyMenu.SetActive(true);
    }

    void DeactivateEnergyMenu()
    {
        isEnergyMenuActive = false;
        kineticEnergyMenu.SetActive(false);
    }

    float CalculateAngle(Vector3 centerPosition, Vector3 targetPosition)
    {
        Vector2 delta = centerPosition - targetPosition; // 중앙에서부터 마우스 위치의 차이
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg; // 각도 구하는 공식 
        angle += 180; // 각도가 -180에서 180이므로 180 더해줌 (각도 쉽게 처리하기 위해서)

        return angle;
    }

    void UpdateMenu()
    {
        // 중앙으로부터의 마우스 거리가 경계값 안에 있는지 확인
        if (Vector3.Distance(Input.mousePosition, center.position) < Vector3.Distance(itemMax.position, center.position) && Vector3.Distance(Input.mousePosition, center.position) > Vector3.Distance(itemMin.position, center.position))
        {
            selectObject.gameObject.SetActive(true);
            float angle = CalculateAngle(center.position, Input.mousePosition);

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

                    if (Input.GetMouseButtonDown(0) && !hasRightMouseClicked)
                    {
                        hasRightMouseClicked = true;

                        Debug.Log(itemNameArray[currentItem] + "선택");
                        DeactivateMenu();

                        // 운동에너지 선택시 새로운 팝업창 생성
                        if (angle >= 120 && angle < 180)
                        {
                            ActivateEnergyMenu();
                        }
                    }
                }
                currentItem++;
            }
        }
        else
        {
            selectObject.gameObject.SetActive(false);
            itemName.text = " ";
            itemExplanation.text = " ";

            foreach (Transform t in itemSlotArray)
            {
                t.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("선택 취소");
            DeactivateMenu();
        }
    }

    void UpdateEnergyMenu()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("운동 에너지 선택 취소");
            DeactivateEnergyMenu();
            ActivateMenu();
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("선택 취소");
            DeactivateEnergyMenu();
            DeactivateMenu();
        }
        
        selectEnergyLeft.SetActive(false);
        selectEnergyRight.SetActive(false);

        if (Vector3.Distance(Input.mousePosition, center.position) < Vector3.Distance(energyMax.position, center.position) && Vector3.Distance(Input.mousePosition, center.position) > Vector3.Distance(energyMin.position, center.position))
        {
            float energyAngle = CalculateAngle(center.position, Input.mousePosition);

            int selectedEnergySlot = (energyAngle > 90 && energyAngle < 270) ? 0 : 1;

            energySlotArray[0].transform.localScale = selectedEnergySlot == 0 ? new Vector3(1.3f, 1.3f, 1.3f) : Vector3.one; //Vector3.one = 모든 축에 대해 크기를 1로
            energySlotArray[1].transform.localScale = selectedEnergySlot == 1 ? new Vector3(1.3f, 1.3f, 1.3f) : Vector3.one;

            moveSpeed.text = selectedEnergySlot == 0 ? "캐릭터의 이동속도를 증가시킨다." : " ";
            attackSpeed.text = selectedEnergySlot == 1 ? "플레이어의 공격 속도를 상승시킨다." : " ";

            if(selectedEnergySlot == 0)
            {
                selectEnergyLeft.SetActive(true);
                selectEnergyRight.SetActive(false);
            } 
            else if(selectedEnergySlot == 1) 
            {
                selectEnergyLeft.SetActive(false);
                selectEnergyRight.SetActive(true);
            }

            if (Input.GetMouseButtonDown(0) && !hasRightMouseClicked)
            {
                hasRightMouseClicked = true;
                Debug.Log(energySlotArray[selectedEnergySlot] + "선택");
                DeactivateEnergyMenu();
            }
        }
        else
        {
            moveSpeed.text = " ";
            attackSpeed.text = " ";

            foreach (Transform t in energySlotArray)
            {
                t.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}

// Atan2의 반환값은 라디안 이기 때문에 도수법을 사용하기 위해서 Mathf.Rad2Deg 를 이용
// Mathf.Rad2Deg는 라디안을 각도로 변환해주는 상수를 나타내고, 그값은 360 / ( PI * 2 )와 같다.