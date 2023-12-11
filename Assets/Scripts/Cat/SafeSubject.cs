using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSubject : MonoBehaviour, ISubject
{
    public List<IObserver> observers = new List<IObserver>();
    
    private bool safeCheck = false;

    public bool currentSafeCheck
    {
        get { return safeCheck; }
        set
        {
            safeCheck = value;
            NotifyObservers(observers);
        }
    }

    private void OnEnable()
    {
        currentSafeCheck = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentSafeCheck = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentSafeCheck = true;
        }
    }

    public void AddObserver<T>(List<T> observerList, T observer) where T : IObserver
    {
        observers.Add(observer);
    }

    public void RemoveObserver<T>(List<T> observerList, T observer) where T : IObserver
    {
        observers.Remove(observer);
    }

    public void NotifyObservers<T>(List<T> observerList) where T : IObserver
    {
        foreach (var observer in observerList)
        {
            observer.Notify(this);
        }
    }
}
