using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("플레이어 컨트롤러")]
    [SerializeField] public PlayerController playerController;
    [HideInInspector] public IPlayerState curState;

    public Dictionary<PlayerStateEnums, IPlayerState> stateDictionary;

    private void Awake() 
    {
        stateDictionary = new Dictionary<PlayerStateEnums, IPlayerState>
        {
            {PlayerStateEnums.IDLE,         new PlayerIdleState(this)},
            {PlayerStateEnums.RUN,          new PlayerRunState(this)},
            {PlayerStateEnums.JUMP,         new PlayerJumpState(this)},
            {PlayerStateEnums.DOUBLE,       new PlayerDoubleJumpState(this)},
            {PlayerStateEnums.FALL,         new PlayerFallState(this)},
            {PlayerStateEnums.BACKROLL,     new PlayerBackRollState(this)},
            {PlayerStateEnums.DIVEROLL,     new PlayerDiveRollState(this)},
            {PlayerStateEnums.STIFFEN,      new PlayerStiffenState(this)},
            {PlayerStateEnums.TRANSFORM,    new PlayerTransformState(this)},

            {PlayerStateEnums.AIM,          new PlayerAimState(this)},
            {PlayerStateEnums.AIM_RUN,      new PlayerAimRunState(this)},

            {PlayerStateEnums.DEAD,         new PlayerDeadState(this)},
        };

        if (stateDictionary.TryGetValue(PlayerStateEnums.IDLE, out IPlayerState newState))
        {
            curState = newState;
            curState.OnStateEnter();
        }
    }

    public bool Contains(IPlayerState state)
    {
        return curState == state;
    }

    public void ChangeStateInput(PlayerStateEnums newStateType)
    {
        if (null == curState)   return;
        if (!curState.allowedInputHash.Contains(newStateType))   return;

        curState.OnStateExit();

        if (stateDictionary.TryGetValue(newStateType, out IPlayerState newState))
        {
            newState.OnStateEnter();
            curState = newState;
        }
    }

    public void ChangeStateLogic(PlayerStateEnums newStateType)
    {
        if (null == curState)   return;
        if (!curState.allowedLogicHash.Contains(newStateType))   return;

        curState.OnStateExit();

        if (stateDictionary.TryGetValue(newStateType, out IPlayerState newState))
        {
            curState = newState;
            curState.OnStateEnter();
        }
    }
}
