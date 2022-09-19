using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallNextCustomer : MonoBehaviour
{ 
    int k = 4;
    [SerializeField]
    CustomerSlotManager customerSlotManager;
    private void Start()
    {
        EventHandler.Instance.OnCallNextCustomer += OnCallNextCustomer;
    }

    private async void OnCallNextCustomer(Vector3 pos)
    { // have to change this k 's hardcore value.
        Debug.Log("In Callnextcustomer method");
        if (k < 10)
        {

            await new WaitUntil(customerSlotManager.CheckFirstFourCustomerSpwaned);
            Debug.Log("In Callnextcustomer method if condition");
            await new WaitForSeconds(5);
            GameObject customer = CustomerPooler.Instance.GetPooledObject();
            customer.SetActive(true);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            customer.transform.parent = customerSlotManager.customerSpwanPoint;
            customer.transform.localPosition = temp;
            if (k > 5)
            {
                customer.GetComponent<CustomerManager>().customerId = k;
            }
            customerSlotManager.customerList.Add(customer);
            EventHandler.Instance.InvokeGiveSlotTransformToCustomer(pos, k);
            LevelManager.Instance.noOfCustomerSpwaned++;
            k++;

        }
    }
    private void OnDisable()
    {
        EventHandler.Instance.OnCallNextCustomer -= OnCallNextCustomer;
    }
}
