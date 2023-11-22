using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerShotState
{
    PlayerController player {get; set;}
    PlayerShotStateMachine stateMachine {get; set;}

    void Execute();

    void OnStateEnter();
    void OnStateExit();
}
