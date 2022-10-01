using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallNextCustomer : MonoBehaviour
{ 
   
    [SerializeField]
    CustomerSlotManager customerSlotManager;
    private void Start()
    {
        EventHandler.Instance.OnCallNextCustomer += OnCallNextCustomer;
    }

    private async void OnCallNextCustomer(Vector3 pos)
    { 
        Debug.Log("In Callnextcustomer method");
       
        if (LevelManager.Instance.noOfCustomerSpwaned < LevelManager.Instance.levelDataSO.totalCustomerWantToSpwan -1)
        {

            await new WaitUntil(customerSlotManager.CheckFirstFourCustomerSpwaned);
            Debug.Log("In Callnextcustomer method if condition");
            await new WaitForSeconds(5);
            GameObject customer = CustomerPooler.Instance.GetPooledObject();
            customer.SetActive(true);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            customer.transform.parent = customerSlotManager.customerSpwanPoint;
            customer.transform.localPosition = temp;
            if (LevelManager.Instance.noOfCustomerSpwaned > 5)
            {
                customer.GetComponent<CustomerManager>().customerId = LevelManager.Instance.noOfCustomerSpwaned;
            }
            customerSlotManager.customerList.Add(customer);
            EventHandler.Instance.InvokeGiveSlotTransformToCustomer(pos, LevelManager.Instance.noOfCustomerSpwaned);
            LevelManager.Instance.noOfCustomerSpwaned++;
        }

    }
    private void OnDisable()
    {
        EventHandler.Instance.OnCallNextCustomer -= OnCallNextCustomer;
    }
}
