using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionSoundHandle : MonoBehaviour
{
    public void StepSound()
    {
        SoundManager.Instance.PlaySFX("PlayerStep");
    }

    public void JumpSound()
    {
        SoundManager.Instance.PlaySFX("PlayerJump");
    }

    public void RollSound()
    {
        SoundManager.Instance.PlaySFX("PlayerRoll");
    }
}
