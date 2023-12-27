using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementProgressBar : MonoBehaviour
{
    [Header("이동하는 물체")]
    public Transform obj;

    [Header("시작 위치")]
    public Transform startPoint;

    [Header("끝 위치")]
    public Transform endPoint;

    public Slider progressBar;
    //public Image playerIcon;

    void Update()
    {
        float maxDistance = Vector3.Distance(new Vector3(startPoint.position.x, 0, startPoint.position.z) , new Vector3(endPoint.position.x, 0, endPoint.position.z));
        float distance = Vector3.Distance(new Vector3(obj.position.x, 0, obj.position.z), new Vector3(endPoint.position.x, 0, endPoint.position.z));
        
        float progress = Mathf.Clamp01((maxDistance - distance) / maxDistance);
        progressBar.value = progress;

        //float iconYPosition = Mathf.Lerp(progressBar.fillRect.rect.yMin, progressBar.fillRect.rect.yMax, progress);
        //playerIcon.rectTransform.anchoredPosition = new Vector2(playerIcon.rectTransform.anchoredPosition.x, iconYPosition);
    }
}
 
