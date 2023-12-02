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
            NotifyObservers(observers);
        }
    }

    private float damage = 5;

    public float currentDamage
    {
        get { return damage; }
        set
        {
            damage = value;
            NotifyObservers(observers);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            currentDamage = other.gameObject.GetComponentInChildren<IAttackable>().GetDamage();
            switch (gameObject.tag)
            {
                case "Parts1":
                    currentParts = PartsEnums.FOREPAWLEFT;
                    break;

                case "Parts2":
                    currentParts = PartsEnums.FOREPAWRIGHT;
                    break;

                case "Parts3":
                    currentParts = PartsEnums.HEAD;
                    break;

                case "Parts4":
                    currentParts = PartsEnums.UPPERBODY;
                    break;

                case "Parts5":
                    currentParts = PartsEnums.BACK;
                    break;

                case "Parts6":
                    currentParts = PartsEnums.LOWERBODY;
                    break;

                case "Parts7":
                    currentParts = PartsEnums.REARPAWLEFT;
                    break;

                case "Parts8":
                    currentParts = PartsEnums.REARPAWRIGHT;
                    break;

                default:
                    break;
            }
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
