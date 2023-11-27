using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    /*public void ToggleMaster()
    {

    }*/

    public void ToggleBGM()
    {
        SoundManager.Instance.ToggleBGM();
    }

    public void ToggleSFX()
    {
        SoundManager.Instance.ToggleSFX();
    }
}
