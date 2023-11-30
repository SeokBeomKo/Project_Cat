using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAttackState : VirusShotState
{
    public VirusAttackOperation virus { get; set; }
    public VirusStateMachine stateMachine { get; set; }

    public VirusAttackState(VirusStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        virus = stateMachine.virusAttackOperation;
    }

    public void Execute()
    {
    }

    public void OnStateEnter()
    {
        SpawnProjectile(stateMachine.PlayerPosition);
    }

    public void OnStateExit()
    {

    }

    private void SpawnProjectile(Vector3 spawnPosition)
    {
        
    }
}
