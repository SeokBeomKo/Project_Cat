using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponSelection : MonoBehaviour
{
    //1번 총 : SoftRifle 
    //2번 총 : SplashBuster
    //3번 총 : BubbleGun
    
    /*private float softRifleBulletCount = 10;
    private float splashBusterBulletCount = 40; // 8발씩
    private float bubbleGunBulletCount = 400;*/
    
    private Transform weaponContainer; // 무기 컨테이너
    
    public TextMeshProUGUI softRifleText;
    public TextMeshProUGUI splashBusterText;
    public TextMeshProUGUI bubbleGunText;
    
    private bool isSoftRifleSelected = false;
    private bool isSplashBusterSelected = false;
    private bool isBubbleGunSelected = false;
    
    public Image softRifleProgress;
    public Image splashBusterProgress;
    public Image bubbleGunProgress;
    public Image softRifleBorder;
    public Image splashBusterBorder;
    public Image bubbleGunBorder;
    
    void Start()
    {
        // 게임 시작 시 초기 무기 선택
        weaponContainer = gameObject.transform;
        SelectWeapon(0);
        isSoftRifleSelected = true;
        isSplashBusterSelected = false;
        isBubbleGunSelected = false;

        softRifleProgress.color = new Color(93 / 255f, 123 / 255f, 195 / 255f);
        softRifleBorder.color = new Color(93 / 255f, 123 / 255f, 195 / 255f);

        splashBusterProgress.color = Color.white;
        splashBusterBorder.color = Color.white;
        bubbleGunProgress.color = Color.white;
        bubbleGunBorder.color = Color.white;
    }
    void Update()
    {
        // 키(예: 숫자 1, 2, 3)를 눌러 무기 변경
        if (Input.GetKeyDown(KeyCode.Alpha1)) // SoftRifle
        {
            SelectWeapon(0);
            isSoftRifleSelected = true;
            isSplashBusterSelected = false;
            isBubbleGunSelected = false;

            softRifleProgress.color = new Color(93 / 255f, 123 / 255f, 195 / 255f);
            softRifleBorder.color = new Color(93 / 255f, 123 / 255f, 195 / 255f);

            bubbleGunProgress.color = Color.white;
            bubbleGunBorder.color = Color.white;
            splashBusterProgress.color = Color.white;
            splashBusterBorder.color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // SplashBuster
        {
            SelectWeapon(1);
            isSoftRifleSelected = false;
            isSplashBusterSelected = true;
            isBubbleGunSelected = false;

            splashBusterProgress.color = new Color(93 / 255f, 123 / 255f, 195 / 255f);
            splashBusterBorder.color = new Color(93 / 255f, 123 / 255f, 195 / 255f);

            softRifleProgress.color = Color.white;
            softRifleBorder.color = Color.white;
            bubbleGunProgress.color = Color.white;
            bubbleGunBorder.color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // BubbleGun
        {
            SelectWeapon(2);
            isSoftRifleSelected = false;
            isSplashBusterSelected = false;
            isBubbleGunSelected = true;

            bubbleGunProgress.color = new Color(93 / 255f, 123 / 255f, 195 / 255f);
            bubbleGunBorder.color = new Color(93 / 255f, 123 / 255f, 195 / 255f);

            softRifleProgress.color = Color.white;
            softRifleBorder.color = Color.white;
            splashBusterProgress.color = Color.white;
            splashBusterBorder.color = Color.white;
        }
    }
    void SelectWeapon(int weaponNum)
    {
        // 선택한 무기를 부모 컨테이너의 가장 아래로 이동
        weaponContainer.GetChild(weaponNum).GetComponent<RectTransform>().anchoredPosition = new Vector3(-55, -280, 0);
        // 선택한 무기 크기 커지게
        weaponContainer.GetChild(weaponNum).localScale = new Vector3(1.3f, 1.3f, 1.3f);
        int j = 1;
        // 모든 무기 위치 재정렬
        for (int i = 0; i < weaponContainer.childCount; i++)
        {
            if (i == weaponNum) continue;
            weaponContainer.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -270 + j * 60, 0);
            weaponContainer.GetChild(i).localScale = Vector3.one;
            j++;
        }
    }


    public void SelectSoftRifle(int bullet)
    {
        softRifleProgress.fillAmount = bullet / 10;
        softRifleText.text = bullet.ToString();
        
    }
    public void SelectSplashBuster(int bullet)
    {
        splashBusterProgress.fillAmount = bullet / 40;
        splashBusterText.text = bullet.ToString();
        
    }
    public void SelectBubbleGun(int bullet)
    {
        bubbleGunProgress.fillAmount = bullet / 400;
        bubbleGunText.text = bullet.ToString();
      
    }
}