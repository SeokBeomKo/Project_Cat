public enum PlayerMovementStateEnums
{
    IDLE,           // 대기 상태
    MOVE,           // 이동 상태
    JUMP,           // 점프 상태
    DOUBLE,         // 이중 점프 상태
    FALL,           // 낙하 상태
    LAND,           // 착지 상태
    
    BACKROLL,       // 대기 회피  상태
    DIVEROLL,       // 이동 회피 상태
    TRANSFORM,      // 변장 상태

    AIM,            // 조준 상태
    AIM_MOVE,       // 조준 이동 상태

    SHOOT,          // 사격 상태
    AIM_SHOOT,      // 조준 사격 상태
    AIM_MOVE_SHOOT, // 조준 이동 사격 상태
    
    STIFFEN,        // 경직 상태
    DEAD,           // 사망 상태

    CHASE_IDLE,     // 추격 _ 대기 상태
    CHASE_MOVE,     // 추격 _ 이동 상태
    CHASE_FALL,     // 추격 _ 낙하 상태
    CHASE_LAND,     // 추격 _ 착지 상태
}

public enum PlayerShotStateEnums
{
    NOTHING,        // 사격 대기 상태

    ENTER,          // 사격 시작 상태
    EXCUTE,         // 사격 중 상태
    EXIT,           // 사격 종료 상태
}