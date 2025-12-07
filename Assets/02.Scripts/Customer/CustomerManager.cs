using UnityEngine;
using Utils.ClassUtility;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    private static CustomerManager instance;
    public static CustomerManager Instance {  get { return instance; } }

    public GameObject customerPrefab;
    public List<CustomerData> customers;
    private Queue<CustomerController> customerQueue = new Queue<CustomerController>();

    private Vector3 spawnPos = new Vector3(0.0f, -11.0f, 0.0f);
    public Transform[] impormationPos;

    private const int maxCustomerQueue = 5;
    private float customerSpawnTime = 10.0f;
    private float currentTime = 0.0f;

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
        customers = DataManager.Instance.LoadJson<CustomerList>(DataManager.Instance.customerDataFileName).Customers;
        impormationPos = GameObject.Find("ImpormationLine").GetComponentsInChildren<Transform>();
    }

    // ¼Õ´Ô »ý¼º
    public void CustomerSpawn()
    {
        if(currentTime >= customerSpawnTime && customerQueue.Count < 5)
        {
            currentTime = 0.0f;
            CustomerController customer = Instantiate(customerPrefab, spawnPos, Quaternion.identity).GetComponent<CustomerController>();
            customer.SetDestination(impormationPos[customerQueue.Count + 1]);
            customerQueue.Enqueue(customer);
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}