using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatStatsSubject : MonoBehaviour, IObserver, ISubject
{
    public List<IObserver> likeabilityObservers = new List<IObserver>();
    public List<IObserver> cleanlinessObservers = new List<IObserver>();

    [Header("데이터")]
    public CleanlinessLikeability data;

    private Dictionary<PartsEnums, (float, float)> catCleanliness = new Dictionary<PartsEnums, (float, float)>();
    private float partsCleanliness;
    private float partsMaxCleanliness;
    private float likeability;

    public float currentMaxLikeability
    {
        get { return data.maxLikeability; }
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
        catCleanliness.Add(PartsEnums.BACK, (0, data.maxBodyCleanliness));
        catCleanliness.Add(PartsEnums.FOREPAWLEFT, (0, data.maxFootCleanliness));
        catCleanliness.Add(PartsEnums.FOREPAWRIGHT, (0, data.maxFootCleanliness));
        catCleanliness.Add(PartsEnums.LOWERBODY, (0, data.maxBodyCleanliness));
        catCleanliness.Add(PartsEnums.REARPAWLEFT, (0, data.maxFootCleanliness));
        catCleanliness.Add(PartsEnums.REARPAWRIGHT, (0, data.maxFootCleanliness));
        catCleanliness.Add(PartsEnums.UPPERBODY, (0, data.maxBodyCleanliness));

        currentLikeability = data.maxLikeability;
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

    // 세척도
    public float GetPartsCleanliness(PartsEnums partsEnum)
    {
        if (catCleanliness.TryGetValue(partsEnum, out (float, float) cleanlinessValue))
        {
            partsCleanliness = cleanlinessValue.Item1;
            return partsCleanliness;
        }
        else
        {
            return 0;
        }
    }

    public float GetPartsMaxCleanliness(PartsEnums partsEnum)
    {
        if (catCleanliness.TryGetValue(partsEnum, out (float, float) cleanlinessValue))
        {
            partsMaxCleanliness = cleanlinessValue.Item2;
            return partsMaxCleanliness;
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
            partsCleanliness = cleanlinessValue.Item1;
            totalCleanliness += partsCleanliness;
        }

        return totalCleanliness;
    }

    public float GetTotalMaxCleanliness()
    {
        float totalMaxCleanliness = 0;

        foreach (var cleanlinessValue in catCleanliness.Values)
        {
            partsMaxCleanliness = cleanlinessValue.Item2;
            totalMaxCleanliness += partsMaxCleanliness;
        }

        return totalMaxCleanliness;
    }

    public void IncreaseCleanliness(PartsEnums currentParts, float fill = 5)
    {
        partsCleanliness = catCleanliness[currentParts].Item1;
        partsMaxCleanliness = catCleanliness[currentParts].Item2;

        catCleanliness[currentParts] = (partsCleanliness + fill, partsMaxCleanliness);

        if (partsCleanliness > partsMaxCleanliness)
        {
            catCleanliness[currentParts] = (partsMaxCleanliness, partsMaxCleanliness);
        }

        Debug.Log("파츠 : " + currentParts + ", 세척도 : " + catCleanliness[currentParts].Item1);
    }

    // 호감도
    public void IncreaseLikeability(float fill = 5)
    {
        currentLikeability += fill;
        
        if (currentLikeability > data.maxLikeability)
        {
            currentLikeability = data.maxLikeability;
        }

        Debug.Log("호감도 증가 : " + currentLikeability);

        NotifyObservers(likeabilityObservers);
    }

    public void DecreaseLikeability(float damage = 5)
    {
        currentLikeability -= damage;

        if (currentLikeability <= 0)
        {
            SceneManager.LoadScene("BadEnding");
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
