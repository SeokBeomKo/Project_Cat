using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("체력")]
    [SerializeField]    
    private int maxHealth;
    private int currentHealth;

    [Header("회피")]
    [SerializeField]    
    private int maxRoll;
    private int currentRoll;
    [SerializeField]    
    private float rollDelay;

    [Header("더블 점프")]
    [SerializeField]    
    private int maxDouble;
    private int currentDouble;

    [Header("수치 값")]
    public float moveSpeed;
    public float rollSpeed;
    public float jumpForce;

    private void Start() 
    {
        currentHealth   = maxHealth;
        currentRoll     = maxRoll;
        currentDouble   = maxDouble;
    }

    private void FixedUpdate()
    {
        RecoveryRollCount();
    }

    float rollTime;
    private void RecoveryRollCount()
    {
        if (currentRoll < maxRoll)
        {
            rollTime += Time.fixedDeltaTime;
            if (rollTime >= rollDelay)
            {
                currentRoll++;
                rollTime = 0;
            }
        }
    }

    public int GetRollCount()
    {
        return currentRoll;
    }

    public int GetDoubleCount()
    {
        return currentDouble;
    }

    public bool CanDouble()
    {
        return currentDouble != 0;
    }

    public bool CanRoll()
    {
        return currentRoll != 0;
    }

    public void UseRoll()
    {
        currentRoll--;
    }

    public void UseDouble()
    {
        currentDouble--;
    }

    public void GetDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            // >> : 플레이어 사망 처리
        }
    }

    public void FillHealth(int fill = 1)
    {
        currentHealth += fill;
        if (currentHealth > maxHealth)  currentHealth = maxHealth;
    }

    public void FillRollCount(int fill = 1)
    {
        currentRoll += fill;
        if (currentRoll > maxRoll)  currentRoll = maxRoll;
    }

    public void FillDoubleCount(int fill = 1)
    {
        currentDouble += fill;
        if (currentDouble > maxDouble)  currentDouble = maxDouble;
    }
}
