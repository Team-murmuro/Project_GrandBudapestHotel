using UnityEngine;
using UnityEngine.AI;
using Utils.EnumType;
using Utils.ClassUtility;

public class CustomerController : MonoBehaviour
{
    public ZoneType zone = ZoneType.None;
    public CustomerState customerState = CustomerState.Idle;

    private RoomData room;
    private CustomerData customerData;

    private Transform target;
    private NavMeshAgent agent;
    private CustomerBehaviour behaviour;

    private SpriteRenderer spriteRenderer; 
    public GameObject speechBubble;

    private const float idleTime = 10.0f;
    private const float waitTime = 15.0f;

    private void Awake()
    {
        behaviour = GetComponent<CustomerBehaviour>();
        agent = GetComponent<NavMeshAgent>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        speechBubble = transform.GetChild(0).GetChild(0).gameObject;

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        StateHandler();
    }

    private void StateHandler()
    {
        switch (customerState)
        {
            case CustomerState.MoveToInformation:
                break;
            case CustomerState.WaitInQueue:
                behaviour.OnWaitting(waitTime);
                break;
            case CustomerState.MoveToRoom:
                break;
            case CustomerState.InRoom:
                break;
            case CustomerState.MoveToFacility:
                break;
            case CustomerState.UseFacility:
                break;
            case CustomerState.Wander:
                break;
            case CustomerState.Event:
                break;
            case CustomerState.MoveToExit:
                SetDestination(CustomerManager.Instance.spawnPos);
                break;
            case CustomerState.Exit:
                break;
        }
    }

    // 손님 초기 설정
    public void SetCustomer(CustomerData _customer)
    {
        customerData = _customer;
    }

    // 방 할당
    public void SetRoom(Room _room)
    {
        room = _room.roomData;
        customerData.roomID = room.id;
        speechBubble.SetActive(false);
        SetDestination(_room.transform);
        customerState = CustomerState.MoveToRoom;
    }

    // 목적지 설정
    public void SetDestination(Transform _target)
    {
        target = _target;
        agent.SetDestination(_target.position);
    }

    // 목적지에 도착했는지 확인
    private bool HasReacheDestination()
    {
        // 경로 계산이 끝났다면
        if (!agent.pathPending)
        {
            // 남은 거리가 짧다면
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // 움직이지 않는 상태라면
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
                    return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D _coll)
    {
        if(_coll.GetComponent<Room>()?.roomType == ZoneType.Elevator)
        {
            spriteRenderer.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D _coll)
    {
        if (_coll.CompareTag("Line") && HasReacheDestination() && target != null)
        {
            if (_coll.transform == target)
            {
                switch (customerState)
                {
                    case CustomerState.MoveToInformation:
                        target = null;
                        speechBubble.SetActive(true);
                        zone = ZoneType.Infomation;
                        customerState = CustomerState.WaitInQueue;
                        break;
                    case CustomerState.MoveToRoom:
                        break;
                    case CustomerState.MoveToExit:
                        Destroy(gameObject);
                        break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D _coll)
    {
        if (_coll.GetComponent<Room>()?.roomType == ZoneType.Elevator)
        {
            spriteRenderer.enabled = true;
        }
    }
}