using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusPreparingState : VirusShotState
{
    public VirusAttackOperation virus { get; set; }
    public VirusStateMachine stateMachine { get; set; }

    public float CoolTime = 3f;
    private float curTime;
    public VirusPreparingState(VirusStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        virus = stateMachine.virusAttackOperation;
    }

    public void Execute()
    {
        if(CollisionCheck())
        {
            curTime += Time.deltaTime;
            if(curTime > CoolTime)
            {
                stateMachine.ChangeState(VirusEnum.Attack);
                curTime = 0;
            }
        }
        else
        {
            stateMachine.ChangeState(VirusEnum.Idle);

        }
    }

    public void OnStateEnter()
    {
        Debug.Log("Preparing");

    }

    public void OnStateExit()
    {
    }

    private bool CollisionCheck()
    {
        Collider[] colliders =
                    Physics.OverlapSphere(virus.transform.position, virus.radius);

        foreach (Collider col in colliders)
        {
            if (col.name == "Sphere" /* 자기 자신은 제외 */) continue;

            if (col.gameObject.CompareTag("Player"))
            {
                virus.PlayerPosition = col.transform.parent.position;
                return true;
            }
        }
        return false;
    }
}
