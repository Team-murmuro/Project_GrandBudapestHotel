using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    private CustomerController controller;

    private void Start()
    {
        controller = GetComponent<CustomerController>();
    }

    // 다음 행동 설정
    public void SetAction()
    {

    }
}