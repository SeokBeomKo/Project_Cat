using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDCenter : MonoBehaviour
{
    [Header("플레이어 스탯")]
    [SerializeField] public PlayerStats playerStats;
    [Header("플레이어 HUD")]
    [SerializeField] public InputHandler inputHandle;

    private void Start() 
    {
    }
}
