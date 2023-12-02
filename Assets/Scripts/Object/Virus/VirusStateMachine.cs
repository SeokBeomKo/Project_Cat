using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VirusEnum { Idle, Preparing, Attack }

public class VirusStateMachine : MonoBehaviour
{
    [Header("바이러스 컨트롤러")]
    [SerializeField] public VirusAttackOperation virusAttackOperation;
    [HideInInspector] public VirusShotState curState;

    public Dictionary<VirusEnum, VirusShotState> virusStateDictionary;

    private void Awake()
    {
        virusStateDictionary = new Dictionary<VirusEnum, VirusShotState>
        {
            {VirusEnum.Idle,         new VirusIdleState(this) },
            {VirusEnum.Preparing,    new VirusPreparingState(this)},
            {VirusEnum.Attack,       new VirusAttackState(this)},
        };

        if (virusStateDictionary.TryGetValue(VirusEnum.Idle, out VirusShotState newState))
        {
            curState = newState;
            curState.OnStateEnter();
        }
    }

    public void ChangeState(VirusEnum newStateType)
    {
        if (null == curState) return;

        curState.OnStateExit();

        if (virusStateDictionary.TryGetValue(newStateType, out VirusShotState newState))
        {
            newState.OnStateEnter();
            curState = newState;
        }
    }
}
