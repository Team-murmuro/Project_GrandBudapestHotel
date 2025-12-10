using UnityEngine;
using Utils.ClassUtility;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    private static CustomerManager instance;
    public static CustomerManager Instance {  get { return instance; } }

    public GameObject customerPrefab;
    public List<CustomerData> customers;
    private List<CustomerController> customerQueue = new List<CustomerController>();

    public Transform spawnPos;
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
        spawnPos = GameObject.Find("Door").transform.GetChild(0).transform;
        impormationPos = GameObject.Find("ImpormationLine").GetComponentsInChildren<Transform>();
    }

    // º’¥‘ ª˝º∫
    public void CustomerSpawn()
    {
        if(currentTime >= customerSpawnTime && customerQueue.Count < 5)
        {
            currentTime = 0.0f;
            CustomerController customer = Instantiate(customerPrefab, spawnPos.position, Quaternion.identity).GetComponent<CustomerController>();
            customer.SetDestination(impormationPos[customerQueue.Count + 1]);
            customer.customerState = Utils.EnumType.CustomerState.Move;
            customerQueue.Add(customer);
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    // º’¥‘ ¿Ãµø
    public void CustomerMove()
    {
        customerQueue.Remove(customerQueue[0]);

        for (int i = 0; i < customerQueue.Count; i++)
        {
            customerQueue[i].SetDestination(impormationPos[i + 1]);
        }
    }
}