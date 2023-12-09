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
    
    // private Transform weaponContainer; // 무기 컨테이너
    
    public TextMeshProUGUI softRifleText;
    public TextMeshProUGUI splashBusterText;
    public TextMeshProUGUI bubbleGunText;

    public TextMeshProUGUI softRifleMax;
    public TextMeshProUGUI splashBusterMax;
    public TextMeshProUGUI bubbleGunMax;
    
    public Image softRifleProgress;
    public Image splashBusterProgress;
    public Image bubbleGunProgress;
    public Image softRifleBorder;
    public Image splashBusterBorder;
    public Image bubbleGunBorder;

    public Color selectColor;

    private void Awake() 
    {
        selectColor = new Color(93 / 255f, 123 / 255f, 195 / 255f);
    }
    
    void Start()
    {
        SetCurWeapon(0);
    }

    public void SetCurWeapon(int number = 0)
    {
        SelectWeapon(number);

        SetWeaponColor(softRifleProgress, softRifleBorder);
        SetWeaponColor(splashBusterProgress, splashBusterBorder);
        SetWeaponColor(bubbleGunProgress, bubbleGunBorder);

        switch (number)
        {
            case 0:
                SetWeaponColor(softRifleProgress, softRifleBorder, true);
                break;
            case 1:
                SetWeaponColor(splashBusterProgress, splashBusterBorder, true);
                break;
            case 2:
                SetWeaponColor(bubbleGunProgress, bubbleGunBorder, true);
                break;
            default:
                break;
        }
        
    }

    private void SetWeaponColor(Image progress, Image border, bool select = false)
    {
        if (select)
            progress.color = border.color = selectColor;
        else
            progress.color = border.color = Color.white;
    }

    void SelectWeapon(int weaponNum)
    {
        // 선택한 무기를 부모 컨테이너의 가장 아래로 이동
        transform.GetChild(weaponNum).GetComponent<RectTransform>().anchoredPosition = new Vector3(-55, -280, 0);
        // 선택한 무기 크기 커지게
        transform.GetChild(weaponNum).localScale = new Vector3(1.3f, 1.3f, 1.3f);
        int j = 1;
        // 모든 무기 위치 재정렬
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == weaponNum) continue;
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -270 + j * 60, 0);
            transform.GetChild(i).localScale = Vector3.one;
            j++;
        }
    }

    public void SelectSoftRifle(int bullet)
    {
        softRifleProgress.fillAmount = bullet / 10f;
        softRifleText.text = bullet.ToString();
        
    }
    public void SelectSplashBuster(int bullet)
    {
        splashBusterProgress.fillAmount = bullet / 30f;
        splashBusterText.text = bullet.ToString();
        
    }
    public void SelectBubbleGun(int bullet)
    {
        bubbleGunProgress.fillAmount = bullet / 400f;
        bubbleGunText.text = bullet.ToString();
    }

    public void SetMaxBullet(int softBullet, int splashBullet, int bubbleBullet)
    {
        softRifleMax.text = softBullet.ToString();
        splashBusterMax.text = splashBullet.ToString();
        bubbleGunMax.text = bubbleBullet.ToString();
    }
}