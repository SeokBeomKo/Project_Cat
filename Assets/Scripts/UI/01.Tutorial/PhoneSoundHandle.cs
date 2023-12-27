using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneSoundHandle : MonoBehaviour
{
    public void PlaySound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX("Phone");
        }
    }

    public void StopSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopSFX();
        }
    }
}
