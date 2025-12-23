using UnityEngine;
using Utils.ClassUtility;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    private static CustomerManager instance;
    public static CustomerManager Instance {  get { return instance; } }

    public GameObject customerPrefab;
    public List<CustomerData> customerData;
    public List<CustomerController> customers = new List<CustomerController>();      // 호텔에 있는 손님들
    public List<CustomerController> customerQueue = new List<CustomerController>();  // 체크인줄에 있는 손님들

    public Transform spawnPos;
    public Transform[] impormationPos;

    private float currentTime = 0.0f;
    private float customerSpawnTime = 10.0f;   // 손님 생성 시간
    private const int maxCustomerQueue = 5;    // 체크인줄 최대 손님수

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        CustomerSpawn();
    }

    public void Init()
    {
        customerData = DataManager.Instance.LoadJson<CustomerList>(DataManager.Instance.customerDataFileName).Customers;
        spawnPos = GameObject.Find("Door").transform.GetChild(0).transform;
        impormationPos = GameObject.Find("ImpormationLine").GetComponentsInChildren<Transform>();
    }

    // 손님 생성
    public void CustomerSpawn()
    {
        if(currentTime >= customerSpawnTime && customerQueue.Count < maxCustomerQueue)
        {
            if(customers.Count < RoomManager.Instance.parlors.Count)
            {
                currentTime = 0.0f;
                CustomerController customer = Instantiate(customerPrefab, spawnPos.position, Quaternion.identity).GetComponent<CustomerController>();

                customer.SetCustomer(customerData[Random.Range(0, customerData.Count)]);
                customer.SetDestination(impormationPos[customerQueue.Count + 1]);
                customer.customerState = Utils.EnumType.CustomerState.MoveToInformation;
                customerQueue.Add(customer);
            }
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    // 손님 대기줄 이동
    public void InfomationLineMove()
    {
        customerQueue.Remove(customerQueue[0]);

        for (int i = 0; i < customerQueue.Count; i++)
        {
            customerQueue[i].SetDestination(impormationPos[i + 1]);
        }
    }
}