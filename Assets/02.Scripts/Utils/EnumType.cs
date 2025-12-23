namespace Utils.EnumType
{
    // 플레이어 상태
    public enum PlayerState
    {
        Idle,
        Move
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

    // 구역 타입
    public enum ZoneType
    {
        None,       // 기본 상태
        Infomation, // 인포메이션
        Corridor,   // 복도
        Parlor,     // 객실
        Facility,   // 시설
        Elevator,   // 엘리베이터
        CCTV        // CCTV실
    }

    // 손님 상태
    public enum CustomerState
    {
        // 체크인
        Idle,              // 기본 상태
        MoveToInformation, // 인포메이션 이동
        WaitInQueue,       // 체크인 대기 중

        // 시설 이용
        MoveToParlor,      // 방으로 이동
        InRoom,            // 방 내부 (휴식, 수면 등)
        MoveToFacility,    // 시설로 이동
        UseFacility,       // 시설 이용 중

        Wander,            // 호텔 내부 배회 중
        Event,             // 이벤트 발생

        // 체크아웃
        MoveToExit,        // 출구로 이동
        Exit               // 호텔 퇴장
    }
}