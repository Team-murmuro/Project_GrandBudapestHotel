using UnityEngine;
using Utils.EnumType;

public class CustomerBehaviour : MonoBehaviour
{
    private CustomerController controller;
    private float currentTime = 0.0f;

    private void Start()
    {
        controller = GetComponent<CustomerController>();
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
            }
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    // 다음 행동 설정
    public void SetAction()
    {

    }
}