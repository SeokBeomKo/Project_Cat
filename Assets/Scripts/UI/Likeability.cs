using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Likeability : MonoBehaviour
{
    public LikeabilityProgressBar likeProgress;
    public LikeabilityProgressBar popupLikeProgress;

    private float _likeability;

    public float likeability
    {
        get { return _likeability; }
        set
        {
            _likeability = value;
            likeProgress.UpdateLikeabilityProgress(_likeability);
            popupLikeProgress.UpdateLikeabilityProgress(_likeability);
        }
    }

    private void Start()
    {
        likeability = 300;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            if (likeability > 0)
                likeability -= 30;
        }
    }
}
