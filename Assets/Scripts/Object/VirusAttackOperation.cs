using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAttackOperation : MonoBehaviour
{
    [Header("HP")]
    public float HP = 5;
    public ObjectHPbar objectHPbar;
 
    [Header("플레이어 감지 범위")]
    public float radius = 0.1f;
    
    [Header("유한 상태 기계")]
    [SerializeField] public VirusStateMachine virusMachine;

    public Vector3 PlayerPosition;
    public GameObject ProjectilePrefab;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.transform.position, radius);
    }

    void Update()
    {
        if (null != virusMachine.curState)
        {
            virusMachine.curState.Execute();
        }

    }

    void Start()
    {
        objectHPbar.SetHP(HP);
        objectHPbar.CheckHP();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            objectHPbar.Damage(1);
            HP = objectHPbar.GetHP();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            HP = 0;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player HP--");
        }
    }


    private void Check()
    {
        if (HP == 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
