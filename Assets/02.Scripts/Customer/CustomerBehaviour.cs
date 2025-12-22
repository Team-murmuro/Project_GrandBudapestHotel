using UnityEngine;
using UnityEngine.AI;
using Utils.EnumType;

public class CustomerBehaviour : MonoBehaviour
{
    private CustomerController controller;

    private Vector3 randomPos;
    private float currentTime = 0.0f;
    private const float facilityValue = 0.4f;
    private const float roomValue = 0.5f;

    private void Start()
    {
        controller = GetComponent<CustomerController>();
    }

    // 상태 변경
    public void ChangeState(CustomerState _state)
    {
        controller.customerState = _state;

        switch (_state)
        {
            case CustomerState.Idle:
                break;
        }
    }

    // 기다리기
    public void OnWaitting(float _timer)
    {
        if (currentTime >= _timer)
        {
            currentTime = 0.0f;

            switch (controller.zone)
            {
                case ZoneType.Infomation:
                    controller.customerState = CustomerState.MoveToExit;
                    CustomerManager.Instance.InfomationLineMove();
                    controller.speechBubble.SetActive(false);
                    break;
                case ZoneType.Parlor:
                    SetAction();
                    break;
                case ZoneType.Facility:
                    SetAction();
                    break;
            }
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    // 다음 행동 결정
    public void SetAction()
    {
        if(Random.value < facilityValue)
        {
            if(Random.value < roomValue)
            {
                // 방 이용
                Debug.Log("::: 방 사용 :::");
                controller.customerState = CustomerState.MoveToParlor;
                controller.SetDestination(controller.roomTransform);
            }
            else
            {
                // 시설 이용
                Debug.Log("::: 호텔 이용 :::");
                controller.customerState = CustomerState.MoveToFacility;
                controller.SetDestination(RoomManager.Instance.facilitys[Random.Range(0, RoomManager.Instance.facilitys.Count)].gameObject.transform);
            }
        }
        else
        {
            // 배회
            if(GetRandomPoint(transform.position, 15.0f, out randomPos))
            {
                Debug.Log("::: 호텔 배회 :::");
                controller.customerState = CustomerState.Wander;
                controller.SetDestination(randomPos);
            }
        }
    }

    // 랜덤 위치 가져오기
    public bool GetRandomPoint(Vector3 _center, float _range, out Vector3 _result, int _tryCount = 30)
    {
        for (int i = 0; i < _tryCount; i++)
        {
            Vector3 randomPoint = _center + Random.insideUnitSphere * _range;

            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
            {
                _result = hit.position;
                return true;
            }
        }

        _result = Vector3.zero;
        return false;
    }
}