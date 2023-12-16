using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, ISubject
{
    [Header("데이터")]
    public PlayerData data;
    public List<IObserver> hpObserverList = new List<IObserver>();
    public List<IObserver> rollObserverList = new List<IObserver>();


    [Header("체력")]
    [SerializeField]    
    private int maxHealth;
    private int _currentHealth;

    public int currentHealth 
    {
        get { return _currentHealth; }
        set 
        {
            _currentHealth = value;
            NotifyObservers(hpObserverList);
        }
    }

    [Header("회피")]
    [SerializeField]    
    private int maxRoll;
    private int _currentRoll;
    public int currentRoll
    {
        get { return _currentRoll; }
        set 
        {
            _currentRoll = value;
            NotifyObservers(rollObserverList);
        }
    }
    [SerializeField]    
    private float rollDelay;

    [Header("더블 점프")]
    [SerializeField]    
    private int maxDouble;
    public int currentDouble;

    [HideInInspector]
    public float moveSpeedOffset = 1f;
    [Header("수치 값")]
    public float moveSpeed;
    public float rollSpeed;
    public float jumpForce;

    void Awake()
    {
        data.LoadDataFromPrefs();
        
        moveSpeed = data.moveSpeed;
        rollSpeed = data.rollSpeed;
        jumpForce = data.jumpForce;
    }
    private void Start() 
    {
        currentRoll     = maxRoll;
        currentDouble   = maxDouble;
    }

    public void SetCurHp(int hp)
    {
        currentHealth = hp;
    }

    public void AddMoveSpeed(float time)
    {
        moveSpeedOffset = 2f;
        StartCoroutine(RecoveryMoveSpeed(time));
    }

    IEnumerator RecoveryMoveSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        moveSpeedOffset = 1f;
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

    public void GetDamage(int damage = 5)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // >> : 플레이어 사망 처리
        }
    }

    public void FillHealth(int fill = 5)
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

    public void AddObserver<T>(List<T> observerList, T observer) where T : IObserver
    {
        observerList.Add(observer);
    }

    public void RemoveObserver<T>(List<T> observerList, T observer) where T : IObserver
    {
        observerList.Remove(observer);
    }

    public void NotifyObservers<T>(List<T> observerList) where T : IObserver
    {
        foreach (T observer in observerList)
        {
            observer.Notify(this);
        }
    }
}
