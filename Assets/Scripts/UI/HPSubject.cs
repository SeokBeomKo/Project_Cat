using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSubject : Subject
{
    [Header("¿ÉÀú¹ö ¸®½ºÆ®")]
    [SerializeField]
    private List<Observer> observerList = new List<Observer>();
   
    public int hp = 100;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (hp > 0)
            {
                hp -= 5;
                NotifyObservers();
            }

            if (hp == 0)
                Debug.Log("²ôÀÄ");
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
