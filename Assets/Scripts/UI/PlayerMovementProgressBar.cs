using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementProgressBar : MonoBehaviour
{
    public Transform player;
    public Transform startPoint;
    public Transform endPoint;

    public Slider progressBar;
    //public Image playerIcon;

    void Update()
    {
        float maxDistance = Vector3.Distance(new Vector3(startPoint.position.x, 0, startPoint.position.z) , new Vector3(endPoint.position.x, 0, endPoint.position.z));
        float distance = Vector3.Distance(new Vector3(player.position.x, 0, player.position.z), new Vector3(endPoint.position.x, 0, endPoint.position.z));
        
        float progress = Mathf.Clamp01((maxDistance - distance) / maxDistance);
        progressBar.value = progress;

        //float iconYPosition = Mathf.Lerp(progressBar.fillRect.rect.yMin, progressBar.fillRect.rect.yMax, progress);
        //playerIcon.rectTransform.anchoredPosition = new Vector2(playerIcon.rectTransform.anchoredPosition.x, iconYPosition);
    }
}
 
