using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer : MonoBehaviour, IObserver
{
    public virtual void Notify(ISubject subject)
    {

    }
}
