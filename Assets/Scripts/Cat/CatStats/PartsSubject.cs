using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSubject : MonoBehaviour, ISubject
{
    public List<IObserver> observers = new List<IObserver>();
    private PartsEnums _currentParts;

    public PartsEnums currentParts
    {
        get { return _currentParts; }
        set
        {
            _currentParts = value;
        }
    }

    private int _damage = 5;

    public int damage
    {
        get { return _damage; }
        set
        {
            _damage = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            switch (gameObject.tag)
            {
                case "Parts1":
                    _currentParts = PartsEnums.FOREPAWLEFT;
                    break;

                case "Parts2":
                    _currentParts = PartsEnums.FOREPAWRIGHT;
                    break;

                case "Parts3":
                    _currentParts = PartsEnums.HEAD;
                    break;

                case "Parts4":
                    _currentParts = PartsEnums.UPPERBODY;
                    break;

                case "Parts5":
                    _currentParts = PartsEnums.BACK;
                    break;

                case "Parts6":
                    _currentParts = PartsEnums.LOWERBODY;
                    break;

                case "Parts7":
                    _currentParts = PartsEnums.REARPAWLEFT;
                    break;

                case "Parts8":
                    _currentParts = PartsEnums.REARPAWRIGHT;
                    break;

                default:
                    break;
            }
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
}
