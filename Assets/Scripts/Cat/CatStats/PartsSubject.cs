using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSubject : MonoBehaviour, ISubject, IDamageable
{
    public List<IObserver> observers = new List<IObserver>();
    private PartsEnums partsEnum;

    private bool collisionPossible = true;

    public PartsEnums currentParts
    {
        get { return partsEnum; }
        set
        {
            partsEnum = value;
        }
    }

    private float damage = 5;

    public float currentDamage
    {
        get { return damage; }
        set
        {
            damage = value;
        }
    }

    private void Start()
    {
        currentParts = partsEnum;
        currentDamage = damage;
    }

    public void BeAttacked(float playerDamage)
    {
        currentDamage = playerDamage;

        GetPartsTag();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack") && collisionPossible)
        {
            currentDamage = other.gameObject.GetComponentInChildren<IAttackable>().GetDamage();

            GetPartsTag();
        }
    }

    private void GetPartsTag()
    {
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
        NotifyObservers(observers);

        StartCoroutine(DisableCollisionForSeconds(1.0f));
    }

    private IEnumerator DisableCollisionForSeconds(float seconds)
    {
        collisionPossible = false;
        yield return new WaitForSeconds(seconds);
        collisionPossible = true;
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
