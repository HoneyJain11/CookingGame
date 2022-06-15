using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class CustomerSlotManager : MonoBehaviour
{
    List<Color> slotColor = new List<Color>();
    [SerializeField]
    List <Transform> slots;
    [SerializeField]
    Transform customerSpwanPoint;
    [SerializeField]
    GameObject customerPrefab;

    private void Start()
    {
        
        slotColor.Clear();

        slotColor.Add(Color.black);
        slotColor.Add(Color.blue);
        slotColor.Add(Color.yellow);
        slotColor.Add(Color.red);
        slotColor.Add(Color.green);
        slotColor.Add(Color.grey);
        slotColor.Add(Color.magenta);
        slotColor.Add(Color.cyan);

        
        /*int slotsCout = gameObject.transform.childCount;
        print("Total Slots : " + slotsCout);
        if (slotsCout>0)
        {
            // slots = AccessSlots(slotsCout, slotColor);

            // slots = GetRandom(slots);
            slots = GetRandom(slots);
            //SetCustomerToSlots(slots, customer);

        }*/
        slots = GetRandom(slots);
        SpwanCustomer();

    }

    private async void SpwanCustomer()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            GameObject customer = CustomerPooler.Instance.GetPooledObject();
            customer.SetActive(true);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            customer.transform.parent = customerSpwanPoint;
            customer.transform.localPosition = temp;
            EventHandler.Instance.InvokeGiveSlotTransformToCustomer(slots[i].position,i);
            await new WaitForSeconds(5);
        }
       


    }

    private async void SetCustomerToSlots(List<GameObject> slots, List<GameObject> customer)
    {

        if(slotColor.Count % slots.Count == 0)
        {

            for (int i =0;i<slotColor.Count; i++)
            {
                if(i<slots.Count)
                {
                    slots[i].GetComponent<SpriteRenderer>().color = slotColor[i];
                }
                else
                {
                    await new WaitForSeconds(2f);
                    slots[i - slots.Count].GetComponent<SpriteRenderer>().color = slotColor[i];
                }
                
            }
            
        }

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


}
