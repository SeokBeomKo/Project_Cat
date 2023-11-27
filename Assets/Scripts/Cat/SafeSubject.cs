using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSubject : MonoBehaviour, ISubject
{
    public List<IObserver> observers = new List<IObserver>();
    
    private bool _safeCheck = false;

    public bool safeCheck
    {
        get { return _safeCheck; }
        set
        {
            _safeCheck = value;
            NotifyObservers(observers);
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
        foreach (var observer in observers)
        {
            observer.Notify(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            safeCheck = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            safeCheck = true;
        }
    }
}
