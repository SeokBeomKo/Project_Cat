using UnityEngine;
using System.Collections;

public class VirusIdleState : VirusShotState
{
	public VirusAttackOperation virus { get; set; }
	public VirusStateMachine stateMachine { get; set; }

	public VirusIdleState(VirusStateMachine _stateMachine)
	{
		stateMachine = _stateMachine;
		virus = stateMachine.virusAttackOperation;
	}

    public void Execute()
    {
      if (CollisionCheck())
        {
            stateMachine.ChangeState(VirusEnum.Preparing);
        }
    }

    public void OnStateEnter()
    {
        Debug.Log("Idle");
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
                return true;
            }
        }
        return false;
    }

    
}

