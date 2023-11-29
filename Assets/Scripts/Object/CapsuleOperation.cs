using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleOperation : MonoBehaviour
{
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        angle++;
        if(angle==360)
        {
            angle = 0;
        }
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
