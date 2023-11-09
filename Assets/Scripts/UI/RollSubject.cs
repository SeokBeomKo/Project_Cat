using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollSubject : Subject
{
    public List<Observer> observerList = new List<Observer>();

    public int maxGauge = 3;
    public int currentGauge;

    public float charginTime = 3.0f;
    public float lastDecreaseTime;

    void Start()
    {
        currentGauge = maxGauge;
        lastDecreaseTime = Time.time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DecreaseGauge();
        }

        if (Time.time - lastDecreaseTime >= charginTime && currentGauge < maxGauge)
        {
            IncreaseGauge();
        }
    }

    private void DecreaseGauge()
    {
        if (currentGauge > 0)
        {
            currentGauge--;
            lastDecreaseTime = Time.time;
            NotifyObservers();
        }
    }

    private void IncreaseGauge()
    {
        if (currentGauge < maxGauge)
        {
            currentGauge++;
            lastDecreaseTime = Time.time;
            NotifyObservers();
        }
    }

    public override void AddObserver(Observer observer) // 등록
    {
        observerList.Add(observer);
    }
    public override void RemoveObserver(Observer observer) // 삭제
    {
        observerList.Remove(observer);
    }
    public override void NotifyObservers() // 알려줘
    {
        foreach (var observer in observerList)
            observer.Notify(this);
    }
}
