using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemWheel : MonoBehaviour
{
    [Header("아이템 휠 설정")]
    public Transform center; // 중앙을 기준으로 마우스 각도 계산
    public Transform selectObject; // 선택된 거 회전

    public GameObject itemMenu; // 아이템 휠 메뉴
    bool isMenuActive; // 메뉴의 활성 상태

    public TextMeshProUGUI itemName; // 아이템 이름
    public TextMeshProUGUI itemExplanation; // 아이템 설명
    public TextMeshProUGUI moveSpeed; // 이동 속도
    public TextMeshProUGUI attackSpeed; // 공격 속도
    
    [Header("아이템 이름")]
    public string[] itemNameArray;
    [Header("아이템 설명")]
    public string[] itemExplanationArray;

    [Header("아이템 이미지")]
    public Transform[] itemSlotArray; // 아이템 이미지 확대
    [Header("운동에너지 아이템 이미지")]
    public Transform[] energySlotArray; // 운동에너지 이미지 확대
    [Header("아이템 휠 경계 값")]
    public Transform itemMin, itemMax; // 아이템 휠 원 경계
    public Transform energyMin, energyMax; // 운동에너지 원 경계

    public GameObject kineticEnergyMenu; // 운동 에너지 선택시 메뉴창
    public GameObject selectEnergyLeft;
    public GameObject selectEnergyRight;
    public GameObject crossHair;

    [Header("아이템 개수 텍스트")]
    public TextMeshProUGUI[] itemCountArray; 

    public delegate void UseItem(string itemName);
    public event UseItem onItemClick;

    private bool isEnergyMenuActive; // 운동에너지 메뉴 활성 상태
    private bool hasRightMouseClicked = false;

    void Start()
    {
        DeactivateMenu();
        DeactivateEnergyMenu();
        selectObject.gameObject.SetActive(false);
        selectEnergyLeft.SetActive(false);
        selectEnergyRight.SetActive(false);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Pause") == 1) return;

        if (Input.GetMouseButtonDown(2)) // 마우스 휠 버튼 무기창 활성화
        {
            SoundManager.Instance.PlaySFX("Hover");
            isMenuActive = !isMenuActive;

            if (isMenuActive && !isEnergyMenuActive)
            {
                itemMenu.SetActive(true);
                crossHair.SetActive(false);
            }
            if (!isMenuActive)
            {
                itemMenu.SetActive(false);
                crossHair.SetActive(true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            hasRightMouseClicked = false;
        }

        if (isMenuActive)
        {
            UpdateMenu();
            UpdateText();
        }

        if (isEnergyMenuActive)
        {
            UpdateEnergyMenu();
        }
    }

    private void UpdateText()
    {
        for(int i = 0; i < 6; i++)
        {
            itemCountArray[i].text = InventoryManager.Instance.GetItemCount(itemNameArray[i]).ToString();
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
                    // : 마우스가 마우스 휠에 들어왔을 시 동작
                    if (120 <= i && i < 240) // 비활성화된 아이템 선택 안되게 설정
                    {
                        selectObject.gameObject.SetActive(false);
                        return;
                    }
                    else
                    {
                        selectObject.gameObject.SetActive(true);
                        selectObject.eulerAngles = new Vector3(0, 0, i); // Z축 주위로 회전
                    }
                    //

                    // :아이템 이름 및 설명 텍스트
                    if (itemName.text != itemNameArray[currentItem])
                    {
                        SoundManager.Instance.PlaySFX("Hover");
                    }
                    itemName.text = itemNameArray[currentItem];
                    
                    if(itemExplanationArray[currentItem].Contains(" ")) // 띄어쓰기 두 번이면 줄 바꿈
                        itemExplanationArray[currentItem] = itemExplanationArray[currentItem].Replace("  ", "\n");
                    itemExplanation.text = itemExplanationArray[currentItem];
                    //

                    // : 아이템 선택시 이미지 커지게
                    foreach (Transform t in itemSlotArray)
                    {
                        t.transform.localScale = new Vector3(1, 1, 1); // 모든 이미지 크기 (1, 1, 1)로 설정
                    }
                    itemSlotArray[currentItem].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                    //

                    // : 아이템 선택
                    if (Input.GetMouseButtonDown(0) && !hasRightMouseClicked)
                    {
                        if (InventoryManager.Instance.GetItemCount(itemNameArray[currentItem]) > 0) // 아이템을 보유하고 있을 때 눌리게
                        {
                            hasRightMouseClicked = true;
                            SoundManager.Instance.PlaySFX("Click");

                            if (angle >= 0 && angle < 60) // : 운동에너지 선택시 새로운 팝업창 생성
                            {
                                DeactivateMenu();
                                ActivateEnergyMenu();
                                crossHair.SetActive(false);
                            }
                            else
                            {
                                Debug.Log("[ItemWheel] " + itemNameArray[currentItem] + "선택");
                                onItemClick?.Invoke(itemNameArray[currentItem]);
                                InventoryManager.Instance.UseItem(itemNameArray[currentItem]);
                                DeactivateMenu();
                                crossHair.SetActive(true);
                            }


                        }
                    }
                    // 
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
            SoundManager.Instance.PlaySFX("Hover");
            DeactivateMenu();
            crossHair.SetActive(true);
        }
    }

    void UpdateEnergyMenu()
    {
        if (Input.GetMouseButtonDown(1)) // 우클릭 시 아이템 휠 활성화 
        {
            SoundManager.Instance.PlaySFX("Hover");
            DeactivateEnergyMenu();
            ActivateMenu();
        }
        else if (Input.GetMouseButtonDown(2)) // 스크롤 클릭 시 모든 휠 꺼지게 
        {
            SoundManager.Instance.PlaySFX("Hover");
            DeactivateEnergyMenu();
            DeactivateMenu();
            crossHair.SetActive(true);
        }
        
        selectEnergyLeft.SetActive(false);
        selectEnergyRight.SetActive(false);

        if (Vector3.Distance(Input.mousePosition, center.position) < Vector3.Distance(energyMax.position, center.position) && Vector3.Distance(Input.mousePosition, center.position) > Vector3.Distance(energyMin.position, center.position))
        {
            float energyAngle = CalculateAngle(center.position, Input.mousePosition);

            int selectedEnergySlot = (energyAngle > 90 && energyAngle < 270) ? 0 : 1;
                        
            energySlotArray[0].transform.localScale = selectedEnergySlot == 0 ? new Vector3(1.3f, 1.3f, 1.3f) : Vector3.one; //Vector3.one = 모든 축에 대해 크기를 1로
            energySlotArray[1].transform.localScale = selectedEnergySlot == 1 ? new Vector3(1.3f, 1.3f, 1.3f) : Vector3.one;

            energySlotArray[0].name = "이동속도";
            energySlotArray[1].name = "공격력";

            if(selectedEnergySlot == 0)
            {
                selectEnergyLeft.SetActive(true);
                selectEnergyRight.SetActive(false);

                if (moveSpeed.text != "캐릭터의 이동속도를 증가시킨다.") SoundManager.Instance.PlaySFX("Hover");

                moveSpeed.text = "캐릭터의 이동속도를 증가시킨다.";
                attackSpeed.text = " ";
            } 
            else if(selectedEnergySlot == 1) 
            {
                selectEnergyLeft.SetActive(false);
                selectEnergyRight.SetActive(true);
                moveSpeed.text = " ";
                if (attackSpeed.text != "플레이어의 공격 속도를 상승시킨다.") SoundManager.Instance.PlaySFX("Hover");
                attackSpeed.text = "플레이어의 공격 속도를 상승시킨다.";
            }

            if (Input.GetMouseButtonDown(0) && !hasRightMouseClicked)
            {

                SoundManager.Instance.PlaySFX("Click");
                hasRightMouseClicked = true;
                Debug.Log("[ItemWheel] " + energySlotArray[selectedEnergySlot].name + "선택");                
                InventoryManager.Instance.UseItem("운동에너지"); 
                onItemClick?.Invoke(energySlotArray[selectedEnergySlot].name);
                
                DeactivateEnergyMenu();
                crossHair.SetActive(true);
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