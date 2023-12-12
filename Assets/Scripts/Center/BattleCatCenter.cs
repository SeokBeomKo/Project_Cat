using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCatCenter : MonoBehaviour
{
    [Header("���� ������ �浹 Ȯ��")]
    [SerializeField] public List<PartsSubject> partsSubjects;

    [Header("���� ����")]
    [SerializeField] public CatStatsSubject catStatsSubject;

    [Header("���� HUD")]
    [SerializeField] public CleanlinessProgressObserver cleanlinessProgressObserver;
    [SerializeField] public CleanlinessPopUpObserver cleanlinessPopUp;
    [SerializeField] public LikeabilityObserver likeabilityObserver;
    [SerializeField] public LikeabilityObserver likeabilityPopUpObserver;

    [Header("�ĵ� ����")]
    [SerializeField] public HitObserver hitObserver;
    [SerializeField] public SafeSubject safeSubject;

    void Start()
    {
        foreach (var partsSubject in partsSubjects)
        {
            partsSubject.AddObserver<IObserver>(partsSubject.observers, catStatsSubject);
        }

        catStatsSubject.AddObserver<IObserver>(catStatsSubject.likeabilityObservers, likeabilityObserver);
        catStatsSubject.AddObserver<IObserver>(catStatsSubject.likeabilityObservers, likeabilityPopUpObserver);

        catStatsSubject.AddObserver<IObserver>(catStatsSubject.cleanlinessObservers, cleanlinessProgressObserver);
        catStatsSubject.AddObserver<IObserver>(catStatsSubject.cleanlinessObservers, cleanlinessPopUp);

        safeSubject.AddObserver<IObserver>(safeSubject.observers, hitObserver);
    }
}