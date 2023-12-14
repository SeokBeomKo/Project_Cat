using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAttackState :MonoBehaviour, VirusShotState
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
        stateMachine.ChangeState(VirusEnum.Idle);
    }

    public void OnStateEnter()
    {
        //SpawnProjectile(stateMachine.PlayerPosition);
        Fire();
    }

    public void OnStateExit()
    {

    }

    public void Fire()
    {
        // 총구 위치에서 타겟 포인트를 향하는 방향을 계산합니다.
        Vector3 fireDirection = (virus.GetPlayPosition() - virus.transform.parent.position);
        fireDirection.y += 0.2f;
        // 총알을 발사합니다.
        GameObject bullet = Instantiate(virus.ProjectilePrefab, virus.transform.parent.position, Quaternion.LookRotation(fireDirection));
        bullet.transform.LookAt(bullet.transform.position + fireDirection);
        bullet.GetComponent<VirusProjectile>().SetDirection(fireDirection);

    }
}
