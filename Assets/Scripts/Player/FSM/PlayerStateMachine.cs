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
            {PlayerStateEnums.Idle,         new PlayerIdleState(this)},
            {PlayerStateEnums.Run,          new PlayerRunState(this)},
            {PlayerStateEnums.Jump,         new PlayerJumpState(this)},
            {PlayerStateEnums.Fall,         new PlayerFallState(this)},
            {PlayerStateEnums.Rolling,      new PlayerRollingState(this)},
            {PlayerStateEnums.Stiffen,      new PlayerStiffenState(this)},
            {PlayerStateEnums.Transform,    new PlayerTransformState(this)},

            {PlayerStateEnums.Dead,         new PlayerDeadState(this)},
        };

        ChangeState(PlayerStateEnums.Idle);
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
