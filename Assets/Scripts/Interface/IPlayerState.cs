using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    PlayerController player {get; set;}
    PlayerStateMachine stateMachine {get; set;}

    // >> : 리스트로 자기자신이 변경 가능한 상태 목록 보유
    HashSet<PlayerMovementStateEnums> allowedInputHash { get; }
    HashSet<PlayerMovementStateEnums> allowedLogicHash { get; }
    void Execute();

    void OnStateEnter();
    void OnStateExit();
}
