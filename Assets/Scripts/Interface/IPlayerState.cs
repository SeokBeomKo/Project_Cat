using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    PlayerController player {get; set;}
    PlayerStateMachine stateMachine {get; set;}
    void Init(PlayerStateMachine stateMachine);
    void Execute();

    void OnStateEnter();
    void OnStateExit();
}
