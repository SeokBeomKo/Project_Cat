using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Likeability : MonoBehaviour
{
    public Image likeabilityProgressBar;

    private float likeability = 300;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (likeability > 0)
            {
                likeability -= 30;
                likeabilityProgressBar.fillAmount = likeability / 300;
            }
        }
    }
}
