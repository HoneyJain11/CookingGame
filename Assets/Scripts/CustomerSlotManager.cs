﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Threading.Tasks;

public class CustomerSlotManager : MonoBehaviour
{
    
    [SerializeField]
    List<Transform> slots;
    [SerializeField]
    public Transform customerSpwanPoint;
    [SerializeField]
    GameObject customerPrefab;
    public List <GameObject> customerList;
    bool spwanedAllFourCustomer = false;
    GameObject moveWasteBread;
    public bool movement;
    public float speed;
    [SerializeField] Transform dustBinTransform;
    private void Start()
    {
        EventHandler.Instance.OnReadyBreadClick += CheckItem;
        EventHandler.Instance.OnRemoveBreadToDustBin += RemoveBreadToDustbin;
        slots = GetRandom(slots);
        customerList = new List<GameObject>();
        

    }
    private void RemoveBreadToDustbin(GameObject wastebread)
    {
        moveWasteBread = wastebread;
        Debug.Log("Bread name - " + moveWasteBread);
        movement = true;
    }

    private void Update()
    {
        //Bread Move To Dustbin logic .
        if (movement == true)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            moveWasteBread.transform.position = Vector3.MoveTowards(moveWasteBread.transform.position, dustBinTransform.position, step);
            if (Vector3.Distance(moveWasteBread.transform.position, dustBinTransform.position) < 0.001f)
            {
               // dustBinTransform.position *= -1.0f;
                movement = false;
                Destroy(moveWasteBread);
            }
        }
    }
    private void CheckItem(GameObject gameObject)
    {
        int itemCheckId;
        bool isItemDeliver;
        int recipeId;
        MachineType machineType = MachineType.None;

        if (gameObject.transform.childCount >= 1)
        {
            if(!gameObject.transform.GetChild(0).gameObject.GetComponent<ToastBread>())
            {
                itemCheckId = gameObject.transform.GetChild(2).gameObject.GetComponent<CoffeeGlass>().itemId;
                machineType = MachineType.CoffeeMachine;
            }
            else
            {
                itemCheckId = gameObject.transform.GetChild(0).gameObject.GetComponent<ToastBread>().itemId;

            }
             
              
            Debug.Log(" clicked item id = " + itemCheckId);
            for (int i = 0; i < customerList.Count; i++)
            {
                if(customerList[i].GetComponent<CustomerManager>().order!=null)
                {
                    for (int j = 0; j < customerList[i].GetComponent<CustomerManager>().order.RecipeList.Count; j++)
                    {

                        isItemDeliver = customerList[i].GetComponent<CustomerManager>().order.IsItemDelivered[j];
                        recipeId = customerList[i].GetComponent<CustomerManager>().order.RecipeList[j].recipeId;
                        if (recipeId == itemCheckId && isItemDeliver == false)
                        {
                            Debug.Log("i =  " + i + " j = " + j);
                            var wishList = customerList[i].transform.GetChild(0);
                            var recipieList = wishList.gameObject.transform.GetChild(j);
                            Debug.Log("recipe item id = " + customerList[i].GetComponent<CustomerManager>().order.RecipeList[j].recipeId);
                            recipieList.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                            customerList[i].GetComponent<CustomerManager>().order.IsItemDelivered[j] = true;
                            // Add deliver right sigbn here
                            Debug.Log("item name  " + customerList[i].transform.GetChild(0).gameObject.transform.GetChild(j).gameObject.transform.GetChild(0).gameObject.name);
                            Debug.Log("i =  " + i + " j = " + j);
                            CheckCustomerAllItemDelivered(customerList[i]);
                            i = customerList.Count;
                            if (machineType == MachineType.CoffeeMachine)
                            {
                                gameObject.GetComponent<Machine>().MachineMode = MachineMode.Idle;
                                Destroy(gameObject.transform.GetChild(2).gameObject);
                                EventHandler.Instance.InvokeOnAgainStartCoffeeMachine();
                                break;
                            }
                            else
                            {
                                gameObject.GetComponent<Plates>().plateStateBread = PlateStateBread.Free;
                                Debug.Log(" plate state bread- " + gameObject.GetComponent<Plates>().plateStateBread);
                                Destroy(gameObject.transform.GetChild(0).gameObject);

                                break;
                            }

                        }
                    }
                }
            }
           
        }

    }

    private void CheckCustomerAllItemDelivered(GameObject customer)
    {
        Debug.Log("In CheckCustomerAllItemDelivered ");
        
        int no = customer.GetComponent<CustomerManager>().order.IsItemDelivered.Count;
        for (int i = 0; i < no; i++)
        {
            if(customer.GetComponent<CustomerManager>().order.IsItemDelivered[i] == true)
            {
                if(i == customer.GetComponent<CustomerManager>().order.IsItemDelivered.Count -1)
                {
                    customer.GetComponent<CustomerManager>().order.IsOrderDelivered = true;
                    int id = customer.GetComponent<CustomerManager>().customerId;
                    Vector3 pos = customer.transform.position;
                    EventHandler.Instance.InvokeOrderDelivered(id);
                    LevelManager.Instance.achievedLevelGoal++;
                    CallNextCustomer(pos);
                    break;
                }
            }
            else
            {
                break;
            }
        }
    }

    private void CallNextCustomer(Vector3 pos)
    { // have to change this k 's hardcore value.
        /* Debug.Log("In Callnextcustomer method");
        if(k < 10)
         {

                 await new WaitUntil(CheckFirstFourCustomerSpwaned);
                 Debug.Log("In Callnextcustomer method if condition");
                 await new WaitForSeconds(5);
                 GameObject customer = CustomerPooler.Instance.GetPooledObject();
                 customer.SetActive(true);
                 Vector3 temp = new Vector3(0f, 0f, 0f);
                 customer.transform.parent = customerSpwanPoint;
                 customer.transform.localPosition = temp;
                 if(k > 5)
                 {
                  customer.GetComponent<CustomerManager>().customerId = k;
                 }
                 customerList.Add(customer);
                 EventHandler.Instance.InvokeGiveSlotTransformToCustomer(pos, k);
                 k++;

         }*/
        
        EventHandler.Instance.InvokeOnCallNextCustomer(pos);

    }

   

    public bool CheckFirstFourCustomerSpwaned()
    {
       
        if (spwanedAllFourCustomer == true)
            return true;
        else
            return false;

    }

    public async void SpwanCustomer()
    {

        for (int i = 0; i < slots.Count; i++)
        {
            GameObject customer = CustomerPooler.Instance.GetPooledObject();
            customer.SetActive(true);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            customer.transform.parent = customerSpwanPoint;
            customer.transform.localPosition = temp;
            customerList.Add(customer);
            EventHandler.Instance.InvokeGiveSlotTransformToCustomer(slots[i].position, i);
            LevelManager.Instance.noOfCustomerSpwaned++;
            await new WaitForSeconds(5);
            
        }
        spwanedAllFourCustomer = true;
     }

    private List<T> GetRandom <T> (List<T> temp)
    {
        var rnd = new System.Random();
        var randomized = temp.OrderBy(item => rnd.Next()).ToList();

        temp.Clear();
        foreach (var value in randomized)
        {
            print(value.ToString());

            temp.Add(value);
        }

        return temp;
    }

    private List<GameObject> AccessSlots(int slotsCout, List<Color> slotColor)
    {
        List<GameObject> slotsList = new List<GameObject>(slotsCout);

        slotsList.Clear();
        for (int i = 0; i < slotsCout; i++)
        {
            slotsList.Add(gameObject.transform.GetChild(i).gameObject);
            // Assign Customer from Customer List Here;
            slotsList[i].GetComponent<SpriteRenderer>().color = slotColor[i];
            print("Acessing Original slot list : " + slotsList[i].name);
        }
        return slotsList;
    }

    private void OnDisable()
    {
        EventHandler.Instance.OnReadyBreadClick -= CheckItem;
        
    }

}
