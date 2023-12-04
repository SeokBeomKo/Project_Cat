using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitScan : MonoBehaviour
{
    public delegate void PlayerHitScanHandle(int damage);
    public event PlayerHitScanHandle OnPlayerHitScan;

    public delegate void PlayerItemEventHandle();
    public event PlayerItemEventHandle OnPlayerDamageUp;
    public event PlayerItemEventHandle OnPlayerSpeedUp;

    [SerializeField]
    private Collider upperCollider;
    [SerializeField]
    private Collider lowerCollider;

    [SerializeField]
    private float invincibleTime = 0.5f; // 무적 시간 설정. 단위는 초입니다.
    
    private float lastHitTime; // 마지막으로 공격을 당한 시간을 저장합니다.

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
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack") && Time.time - lastHitTime > invincibleTime)
        {
            lastHitTime = Time.time; // 공격을 당한 시간을 갱신합니다.
            if (null != other.GetComponent<IAttackable>())
                OnPlayerHitScan?.Invoke((int)other.GetComponent<IAttackable>().GetDamage());
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("InstantItem"))
        {
            switch (other.gameObject.tag)
            {
                case "SpeedUp":
                    OnPlayerSpeedUp?.Invoke();
                    break;
                case "DamageUp":
                    OnPlayerDamageUp?.Invoke();
                    break;
                default:
                    break;
            }
            
        }
    }
}
