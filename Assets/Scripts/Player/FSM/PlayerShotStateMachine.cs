using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotStateMachine : MonoBehaviour
{
    [Header("플레이어 컨트롤러")]
    [SerializeField] public PlayerController playerController;
    [HideInInspector] public IPlayerShotState curState;

    public Dictionary<PlayerShotStateEnums, IPlayerShotState> stateDictionary;

    private void Awake() 
    {
        stateDictionary = new Dictionary<PlayerShotStateEnums, IPlayerShotState>
        {
            {PlayerShotStateEnums.NOTHING,          new PlayerNothingShotState(this)},

            {PlayerShotStateEnums.ENTER,            new PlayerEnterShotState(this)},
            {PlayerShotStateEnums.EXCUTE,           new PlayerExcuteShotState(this)},
            {PlayerShotStateEnums.EXIT,             new PlayerExitShotState(this)},
        };

        if (stateDictionary.TryGetValue(PlayerShotStateEnums.NOTHING, out IPlayerShotState newState))
        {
            curState = newState;
            curState.OnStateEnter();
        }
    }

    public void ChangeState(PlayerShotStateEnums newStateType)
    {
        if (null == curState)   return;

        curState.OnStateExit();

        if (stateDictionary.TryGetValue(newStateType, out IPlayerShotState newState))
        {
            newState.OnStateEnter();
            curState = newState;
        }
    }
}
