using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateBallOperation : MonoBehaviour
{
    private int HP = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            HP--;

            Debug.Log("HP : " + HP);

            if(HP == 0)
            {
                // 용액 묻은 공으로 변경
            }
        }
    }
}
