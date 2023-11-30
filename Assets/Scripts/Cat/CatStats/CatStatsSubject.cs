using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatsSubject : MonoBehaviour, IObserver, ISubject
{
    public List<IObserver> likeabilityObservers = new List<IObserver>();
    public List<IObserver> cleanlinessObservers = new List<IObserver>();

    [Header("세척도 최댓값")]
    [SerializeField]
    private int maxCleanliness = 100;

    private Dictionary<PartsEnums, int> catCleanliness = new Dictionary<PartsEnums, int>();

    [Header("호감도 최댓값")]
    [SerializeField]
    private int maxLikeability = 100;

    private int likeability;

    public int currentLikeability
    {
        get { return likeability; }
        set
        {
            likeability = value;
            NotifyObservers(likeabilityObservers);
        }
    }

    private void Start()
    {
        catCleanliness.Add(PartsEnums.BACK, 0);
        catCleanliness.Add(PartsEnums.FOREPAWLEFT, 0);
        catCleanliness.Add(PartsEnums.FOREPAWRIGHT, 0);
        catCleanliness.Add(PartsEnums.LOWERBODY, 0);
        catCleanliness.Add(PartsEnums.REARPAWLEFT, 0);
        catCleanliness.Add(PartsEnums.REARPAWRIGHT, 0);
        catCleanliness.Add(PartsEnums.UPPERBODY, 0);

        likeability = maxLikeability;
    }

    public void Notify(ISubject subject)
    {
        var partsSubject = subject as PartsSubject;

        if (partsSubject.currentParts == PartsEnums.HEAD)
        {
            DecreaseLikeability(partsSubject.damage);
        }
        else
        {
            IncreaseCleanliness(partsSubject.currentParts, partsSubject.damage);
            DecreaseLikeability(partsSubject.damage);
        }
    }

    public int GetPartsCleanliness(PartsEnums partsEnum)
    {
        if (catCleanliness.TryGetValue(partsEnum, out int cleanlinessValue))
        {
            return cleanlinessValue;
        }
        else
        {
            return 0;
        }
    }

    public int GetTotalCleanliness()
    {
        int totalCleanliness = 0;

        foreach (var cleanlinessValue in catCleanliness.Values)
        {
            totalCleanliness += cleanlinessValue;
        }

        return totalCleanliness;
    }

    public void IncreaseCleanliness(PartsEnums currentParts, int fill = 5)
    {
        catCleanliness[currentParts] += fill;

        if (catCleanliness[currentParts] > maxCleanliness)
        {
            catCleanliness[currentParts] = maxCleanliness;
        }

        Debug.Log("파츠 : " + currentParts + ", 세척도 : " + catCleanliness[currentParts]);
    }

    public void IncreaseLikeability(int fill = 5)
    {
        likeability += fill;
        
        if (likeability > maxLikeability)
        {
            likeability = maxLikeability;
        }

        Debug.Log("호감도 증가 : " + likeability);
    }

    public void DecreaseLikeability(int damage = 5)
    {
        likeability -= damage;

        if (likeability <= 0)
        {
            // 게임 오버
        }

        Debug.Log("호감도 감소 : " + likeability);
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
