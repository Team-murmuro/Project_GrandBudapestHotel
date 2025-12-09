using UnityEngine;
using UnityEngine.AI;
using Utils.ClassUtility;
using Utils.EnumType;
using static UnityEngine.GraphicsBuffer;

public class CustomerController : MonoBehaviour
{
    public CustomerState customerState = CustomerState.Idle;
    private CustomerData customerData = null;

    private NavMeshAgent agent;
    private Transform target;

    private GameObject speechBubble;

    private float currentTime = 0.0f;
    private const float waitTime = 15.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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
            case CustomerState.Idle:
                break;
            case CustomerState.Move:

                break;
            case CustomerState.Wait:
                if(currentTime >= waitTime)
                {
                    currentTime = 0.0f;
                    speechBubble.SetActive(false);
                    customerState = CustomerState.CheckOut;
                }
                else
                {
                    currentTime += Time.deltaTime;
                }

                    break;
            case CustomerState.Wander:
                break;
            case CustomerState.Rest:
                break;
            case CustomerState.Room:
                break;
            case CustomerState.Event:
                break;
            case CustomerState.Angry:
                break;
            case CustomerState.CheckOut:
                SetDestination(CustomerManager.Instance.spawnPos);

                break;
        }
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

    private void OnTriggerStay2D(Collider2D _coll)
    {
        if (_coll.CompareTag("Destination") && HasReacheDestination() && target != null)
        {
            if (_coll.transform == target)
            {
                if(customerState == CustomerState.Move)
                {
                    Debug.Log(":: 목적지 도착 ::");
                    target = null;
                    customerState = CustomerState.Wait;
                    speechBubble.SetActive(true);
                }
                else if(customerState == CustomerState.CheckOut)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}