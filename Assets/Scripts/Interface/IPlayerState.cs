using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    PlayerController player {get; set;}
    PlayerStateMachine stateMachine {get; set;}

    // >> : TODO : 리스트로 자기자신이 변경 가능한 상태 목록 보유
    // List<IPlayerState>
    void Init(PlayerStateMachine stateMachine);
    void Execute();

    void OnStateEnter();
    void OnStateExit();

    void ChangeState(IPlayerState newState);
}
