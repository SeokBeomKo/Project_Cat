using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCleanerMovementX : MonoBehaviour
{
    public int Speed;
    private int angle;
    private bool rotation;
    private int direction;

    // Update is called once per frame
    void Update()
    {
        if (rotation)
        {
            if (direction > 0)
            {
                angle++;
                if (angle == 180)
                {
                    rotation = false;
                }
            }
            else
            {
                angle--;
                if(angle==0)
                {
                    rotation = false;
                }
            }
            transform.rotation = Quaternion.Euler(0, angle, 0);


        }
        else
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if(angle==0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            rotation = true;
        }

    }


}
