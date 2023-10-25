using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject // 구독자
{
    void AddObserver(Observer observer); // 등록
    void RemoveObserver(Observer observer); // 삭제
    void NotifyObservers(); // 
}
