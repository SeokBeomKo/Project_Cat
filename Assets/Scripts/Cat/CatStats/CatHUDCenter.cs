using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHUDCenter : MonoBehaviour
{
    [Header("보스 파츠별 충돌 확인")]
    [SerializeField] public List<PartsSubject> partsSubjects;
    [Header("보스 스탯")]
    [SerializeField] public CatStatsSubject catStatsSubject;

    [Header("보스 HUD")]
    //[SerializeField] public CleanlinessObserver cleanlinessObserver;
    [SerializeField] public LikeabilityObserver likeabilityObserver;
    [SerializeField] public LikeabilityObserver likeabilityPopUpObserver;

    void Start()
    {
        foreach (var partsSubject in partsSubjects)
        {
            partsSubject.AddObserver<IObserver>(partsSubject.observers, catStatsSubject);
        }

        catStatsSubject.AddObserver<IObserver>(catStatsSubject.likeabilityObservers, likeabilityObserver);
        catStatsSubject.AddObserver<IObserver>(catStatsSubject.likeabilityObservers, likeabilityPopUpObserver);

        //catStatsSubject.AddObserver<IObserver>(catStatsSubject.cleanlinessObservers, cleanlinessObserver);
    }
}
