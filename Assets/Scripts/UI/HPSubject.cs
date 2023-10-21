using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSubject : Subject
{
    [SerializeField]
    private List<Observer> observerList = new List<Observer>();
   
    public int hp = 10;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            hp--;
            NotifyObservers();
        }
    }

    public override void AddObserver(Observer observer)
    {
        observerList.Add(observer);
    }
    public override void RemoveObserver(Observer observer)
    {
        observerList.Remove(observer);
    }
    public override void NotifyObservers()
    {
        foreach (var observer in observerList)
            observer.Notify(this);             
    }
}
