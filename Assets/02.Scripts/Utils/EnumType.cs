namespace Utils.EnumType
{
    // 플레이어 상태
    public enum PlayerState
    {
        Idle,
        Move
    }

    // 손님 상태
    public enum CustomerState
    {
        Idle,    // 기본 상태
        Wait,    // 대기 상태
        Wander,  // 배회 상태
        Rest,    // 휴식 상태
        Event,   // 이벤트 상태
        CheckOut // 체크아웃 상태
    }

    // 직원 상태
    public enum StaffState
    {
        Idle,  // 기본 상태
        Wait,  // 대기 상태
        Move,  // 이동 상태
        Work,  // 작업 상태
        Rest   // 휴식 상태
    }
}