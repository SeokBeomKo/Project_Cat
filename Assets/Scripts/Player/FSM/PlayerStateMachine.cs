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
            {PlayerStateEnums.DIVEROLL,     new PlayerDiveRollState(this)},
            {PlayerStateEnums.STIFFEN,      new PlayerStiffenState(this)},
            {PlayerStateEnums.TRANSFORM,    new PlayerTransformState(this)},

            {PlayerStateEnums.DEAD,         new PlayerDeadState(this)},
        };

        ChangeState(PlayerStateEnums.IDLE);
    }

    public void ChangeState(PlayerStateEnums newStateType)
    {
        if (null != curState)   curState.OnStateExit();

        if (stateDictionary.TryGetValue(newStateType, out IPlayerState newState))
        {
            curState = newState;
            curState.OnStateEnter();
        }
    }
}
