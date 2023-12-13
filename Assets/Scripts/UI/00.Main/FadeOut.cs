using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public FadeInOut fade;

    void Start()
    {
        fade.FadeOut();
    }
}
