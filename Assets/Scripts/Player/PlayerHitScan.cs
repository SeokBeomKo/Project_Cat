using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitScan : MonoBehaviour
{
    public delegate void PlayerHitScanHandle(int damage);
    public event PlayerHitScanHandle OnPlayerHitScan;

    [SerializeField]
    private Collider upperCollider;
    [SerializeField]
    private Collider lowerCollider;

    public void EnterJumpPlayer()
    {
        lowerCollider.enabled = false;
    }

    public void ExitJumpPlayer()
    {
        lowerCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            OnPlayerHitScan?.Invoke(5);
        }
    }
}
