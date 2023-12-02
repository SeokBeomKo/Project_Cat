using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface VirusShotState
{
    VirusAttackOperation virus { get; set;}
    VirusStateMachine stateMachine { get; set; }

    void Execute();

    void OnStateEnter();
    void OnStateExit();
}

