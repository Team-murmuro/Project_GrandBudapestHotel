using UnityEngine;
using UnityEngine.AI;
using Utils.EnumType;
using Utils.ClassUtility;

public class CustomerController : MonoBehaviour
{
    private CustomerState customerState = CustomerState.Idle;
    private CustomerData customerData = null;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        StateHandler();
        Debug.Log(HasReacheDestination());
    }

    private void StateHandler()
    {
        switch (customerState)
        {
            case CustomerState.Idle:
                break;
            case CustomerState.Wait:
                break;
            case CustomerState.Wander:
                break;
            case CustomerState.Rest:
                break;
            case CustomerState.Room:
                break;
            case CustomerState.Event:
                break;
            case CustomerState.CheckOut:
                break;
        }
    }

    // 목적지 설정
    public void SetDestination(Transform _target)
    {
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
}