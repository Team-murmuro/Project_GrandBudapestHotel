using UnityEngine;
using Utils.ClassUtility;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    private static CustomerManager instance;
    public static CustomerManager Instance {  get { return instance; } }

    public List<CustomerData> customers;

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

    public void Init()
    {
        customers = DataManager.Instance.LoadJson<CustomerList>(DataManager.Instance.customerDataFileName).Customers;
    }
}