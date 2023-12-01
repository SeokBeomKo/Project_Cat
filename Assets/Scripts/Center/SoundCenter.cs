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

    private void Awake()
    {
        InitValue();
        InitToggle();

        PlayerPrefs.SetInt("Pause", 0);
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


    public void ToggleMaster()
    {
        SoundManager.Instance.PlaySFX("Click");
        SoundManager.Instance.ToggleMaster();

        masterButton.gameObject.SetActive(PlayerPrefs.GetInt("isMasterOn") == 1);
        bgmButton.gameObject.SetActive(PlayerPrefs.GetInt("isBGMOn") == 1);
        sfxButton.gameObject.SetActive(PlayerPrefs.GetInt("isSFXOn") == 1);
    }

    public void ToggleBGM()
    {
        SoundManager.Instance.PlaySFX("Click");
        SoundManager.Instance.ToggleBGM();

        masterButton.gameObject.SetActive(PlayerPrefs.GetInt("isMasterOn") == 1);
        bgmButton.gameObject.SetActive(PlayerPrefs.GetInt("isBGMOn") == 1);
    }

    public void ToggleSFX()
    {
        SoundManager.Instance.PlaySFX("Click");
        SoundManager.Instance.ToggleSFX();

        masterButton.gameObject.SetActive(PlayerPrefs.GetInt("isMasterOn") == 1);
        sfxButton.gameObject.SetActive(PlayerPrefs.GetInt("isSFXOn") == 1);
    }

}
