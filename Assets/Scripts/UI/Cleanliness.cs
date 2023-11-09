using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cleanliness : MonoBehaviour
{
    public Image cleanlinessProgressBar;

    private float cleanliness = 100;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (cleanliness > 0)
            {
                cleanliness -= 10;
                cleanlinessProgressBar.fillAmount = cleanliness / 100;
            }
        }
    }
}
