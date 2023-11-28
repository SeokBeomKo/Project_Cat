using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundCenter : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public Button masterButton;
    public Button bgmButton;
    public Button sfxButton;

    public TextMeshProUGUI masterTxt;
    public TextMeshProUGUI bgmTxt;
    public TextMeshProUGUI sfxTxt;

    public GameObject soundSetting;

    private void Awake()
    {
        InitValue();
        InitToggle();
    }

    private void Update()
    {
        if (soundSetting.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                soundSetting.SetActive(false);
            }
        }
    }

    public void InitValue()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master");
        bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX");
    }

    public void InitToggle()
    {
        masterButton.gameObject.SetActive(PlayerPrefs.GetInt("isMasterOn") == 1);
        bgmButton.gameObject.SetActive(PlayerPrefs.GetInt("isBGMOn") == 1);
        sfxButton.gameObject.SetActive(PlayerPrefs.GetInt("isSFXOn") == 1);
    }

    public void ChangeMasterVolume(float volume)
    {
        SoundManager.Instance.ChangeMasterVolume(volume);
        int curPercent = Mathf.RoundToInt(volume * 100);
        masterTxt.text = curPercent.ToString();
    }

    public void ChangeBGMVolume(float volume)
    {
        SoundManager.Instance.ChangeBGMVolume(volume);
        int curPercent = Mathf.RoundToInt(volume * 100);
        bgmTxt.text = curPercent.ToString();
    }

    public void ChangeSFXVolume(float volume)
    {
        SoundManager.Instance.ChangeSFXVolume(volume);
        int curPercent = Mathf.RoundToInt(volume * 100);
        sfxTxt.text = curPercent.ToString();
    }

    public void ClosePopUp()
    {
        soundSetting.SetActive(false);
    }

    public void ToggleMaster()
    {
        SoundManager.Instance.ToggleMaster();

        masterButton.gameObject.SetActive(PlayerPrefs.GetInt("isMasterOn") == 1);
        bgmButton.gameObject.SetActive(PlayerPrefs.GetInt("isBGMOn") == 1);
        sfxButton.gameObject.SetActive(PlayerPrefs.GetInt("isSFXOn") == 1);
    }

    public void ToggleBGM()
    {
        SoundManager.Instance.ToggleBGM();

        bgmButton.gameObject.SetActive(PlayerPrefs.GetInt("isBGMOn") == 1);
    }

    public void ToggleSFX()
    {
        SoundManager.Instance.ToggleSFX();

        sfxButton.gameObject.SetActive(PlayerPrefs.GetInt("isSFXOn") == 1);
    }
}
