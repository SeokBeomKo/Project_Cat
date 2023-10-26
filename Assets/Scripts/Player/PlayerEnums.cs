public enum PlayerStateEnums
{
    IDLE,           // 대기 상태
    RUN,            // 이동 상태
    JUMP,           // 점프 상태
    DOUBLE,         // 이중 점프 상태
    FALL,           // 낙하 상태
    LAND,           // 착지 상태
    
    BACKROLL,       // 대기 회피  상태
    DIVEROLL,       // 이동 회피 상태
    TRANSFORM,      // 변장 상태

    AIM,            // 조준 상태
    AIM_RUN,       // 조준 이동 상태
    SHOOT,          // 사격 상태
    
    STIFFEN,        // 경직 상태
    DEAD,           // 사망 상태
}