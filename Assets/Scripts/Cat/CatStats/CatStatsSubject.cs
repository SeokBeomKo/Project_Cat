using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatsSubject : MonoBehaviour, IObserver, ISubject
{
    public List<IObserver> likeabilityObservers = new List<IObserver>();
    public List<IObserver> cleanlinessObservers = new List<IObserver>();

    [Header("세척도 최댓값")]
    [SerializeField]
    private float maxCleanliness = 300;

    private Dictionary<PartsEnums, float> catCleanliness = new Dictionary<PartsEnums, float>();
    private float cleanliness;

    public float currentMaxCleanliness
    {
        get { return maxCleanliness; }
    }

    public float currentCleanliness
    {
        get { return cleanliness; }
        set
        {
            cleanliness = value;
            NotifyObservers(cleanlinessObservers);
        }
    }

    [Header("호감도 최댓값")]
    [SerializeField]
    private float maxLikeability = 300;
    private float likeability;

    public float currentMaxLikeability
    {
        get { return maxLikeability; }
    }

    public float currentLikeability
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

        currentLikeability = maxLikeability;
        currentCleanliness = maxCleanliness;
    }

    public void Notify(ISubject subject)
    {
        var partsSubject = subject as PartsSubject;

        if (partsSubject.currentParts == PartsEnums.HEAD)
        {
            DecreaseLikeability(partsSubject.currentDamage);
        }
        else
        {
            IncreaseCleanliness(partsSubject.currentParts, partsSubject.currentDamage);
            DecreaseLikeability(partsSubject.currentDamage);
        }
    }

    public float GetPartsCleanliness(PartsEnums partsEnum)
    {
        if (catCleanliness.TryGetValue(partsEnum, out float cleanlinessValue))
        {
            return cleanlinessValue;
        }
        else
        {
            return 0;
        }
    }

    public float GetTotalCleanliness()
    {
        float totalCleanliness = 0;

        foreach (var cleanlinessValue in catCleanliness.Values)
        {
            totalCleanliness += cleanlinessValue;
        }

        return totalCleanliness;
    }

    public void IncreaseCleanliness(PartsEnums currentParts, float fill = 5)
    {
        catCleanliness[currentParts] += fill;

        currentCleanliness = catCleanliness[currentParts];

        if (currentCleanliness > maxCleanliness)
        {
            currentCleanliness = maxCleanliness;
            catCleanliness[currentParts] = currentCleanliness;
        }

        Debug.Log("파츠 : " + currentParts + ", 세척도 : " + currentCleanliness);
    }

    public void IncreaseLikeability(float fill = 5)
    {
        currentLikeability += fill;
        
        if (currentLikeability > maxLikeability)
        {
            currentLikeability = maxLikeability;
        }

        Debug.Log("호감도 증가 : " + currentLikeability);

        NotifyObservers(likeabilityObservers);
    }

    public void DecreaseLikeability(float damage = 5)
    {
        currentLikeability -= damage;

        if (currentLikeability <= 0)
        {
            // 게임 오버
        }

        Debug.Log("호감도 감소 : " + currentLikeability);
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
