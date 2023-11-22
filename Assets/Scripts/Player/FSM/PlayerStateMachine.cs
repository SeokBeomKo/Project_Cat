using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("플레이어 컨트롤러")]
    [SerializeField] public PlayerController playerController;
    [HideInInspector] public IPlayerState curState;

    public Dictionary<PlayerMovementStateEnums, IPlayerState> stateDictionary;

    private void Awake() 
    {
        stateDictionary = new Dictionary<PlayerMovementStateEnums, IPlayerState>
        {
            {PlayerMovementStateEnums.IDLE,             new PlayerIdleState(this)},
            {PlayerMovementStateEnums.MOVE,             new PlayerMoveState(this)},

            {PlayerMovementStateEnums.JUMP,             new PlayerJumpState(this)},
            {PlayerMovementStateEnums.DOUBLE,           new PlayerDoubleJumpState(this)},
            {PlayerMovementStateEnums.FALL,             new PlayerFallState(this)},
            {PlayerMovementStateEnums.LAND,             new PlayerLandState(this)},

            {PlayerMovementStateEnums.BACKROLL,         new PlayerBackRollState(this)},
            {PlayerMovementStateEnums.DIVEROLL,         new PlayerDiveRollState(this)},

            {PlayerMovementStateEnums.STIFFEN,          new PlayerStiffenState(this)},
            {PlayerMovementStateEnums.TRANSFORM,        new PlayerTransformState(this)},

            {PlayerMovementStateEnums.AIM,              new PlayerAimState(this)},
            {PlayerMovementStateEnums.AIM_MOVE,         new PlayerAimMoveState(this)},

            {PlayerMovementStateEnums.AIM_SHOOT,        new PlayerAimShootState(this)},
            {PlayerMovementStateEnums.SHOOT,            new PlayerShootState(this)},
            {PlayerMovementStateEnums.AIM_MOVE_SHOOT,   new PlayerAimMoveShootState(this)},

            {PlayerMovementStateEnums.DEAD,             new PlayerDeadState(this)},
        };

        if (stateDictionary.TryGetValue(PlayerMovementStateEnums.IDLE, out IPlayerState newState))
        {
            curState = newState;
            curState.OnStateEnter();
        }
    }

    public bool Contains(IPlayerState state)
    {
        return curState == state;
    }

    public void ChangeStateAny(PlayerMovementStateEnums newStateType)
    {
        if (null == curState)   return;

        curState.OnStateExit();

        if (stateDictionary.TryGetValue(newStateType, out IPlayerState newState))
        {
            newState.OnStateEnter();
            curState = newState;
        }
    }

    public void ChangeStateInput(PlayerMovementStateEnums newStateType)
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

    public void ChangeStateLogic(PlayerMovementStateEnums newStateType)
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
