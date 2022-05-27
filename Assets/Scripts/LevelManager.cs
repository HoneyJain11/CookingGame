using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : GenericSingleton<LevelManager>
{

    [SerializeField]
    List <GameObject> leftMachineSlots;
    [SerializeField]
    List<GameObject> rightMachineSlots;
    [SerializeField]
    List<GameObject> traySlots;
    [SerializeField]
    List<GameObject> trayPrefabs;
    [SerializeField]
    GameObject leftMachinePrefab;
    [SerializeField]
    GameObject rightMachinePrefab;
    [SerializeField]
    GameObject platesPrefab;
    [SerializeField]
    int platesUnlocked;
    [SerializeField]
    ToastBread toastBread;
    [SerializeField]
    BreadSO breadSO;

    [SerializeField]
    List<ToasterSO> toasterSO;
    //[SerializeField]
    //LeftSideMachineSO toasterSO;



    private void Start()
    { //placing all the object on it's specific slots

        //Set LeftTable Objects here eg. Toaster
        //placing leftside machine on proper leftsideslot.
      //  SetTableTopObjects(leftMachineSlots, breadSO.toastMachinePrefab);

        //Set RightTable Objects here eg.Coffee Machine
        //placing rightside machine on proper rightsideslot.
        //SetTableTopObjects(rightMachineSlots, rightMachinePrefab);
        //SetRightMachineItems();

        //Set Tray Objects here eg. strawberry, chocolate, eggs, peanuts
        //placing tarys on correct slot.needs array of prefabs ex - chocolate tary prefab, peanuts tray prefab. 
        SetTableTopObjects(traySlots, trayPrefabs);

        // Set Serving Area Objects on Table top here eg. 4 nos. of Plates
        //placing plates on correct slot.
   

    }
    private void Update()
    {
        MouseClick();
    }
    // checking the mouseclick , on which raycast is hitting
    public void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.zero);

            if(hit && hit.collider != null )
            {
                          
                    if ( hit.collider.GetComponent<BreadElement>()!=null)
                     {
                         Debug.Log("Click on Bread");

                         EventHandler.Instance.InvokeOnBreadClickEvent();

                     }

                     else if(hit.collider.GetComponent<Machine>()!=null && hit.collider.GetComponent<Machine>().machineType == MachineType.Toaster)
                     {
                         print("Clicked On toaster");
                        int machineId = hit.collider.GetComponent<Machine>().machineId;
                         EventHandler.Instance.InvokeOnToasterClickEvent(machineId);

                     }
                     else if(hit.collider.GetComponent<Machine>() != null && hit.collider.GetComponent<Machine>().machineType == MachineType.CoffeeMachine)
                     {
                         print("Clicked On coffe machine");
                         int machineId = hit.collider.GetComponent<Machine>().machineId;
                         EventHandler.Instance.InvokeOnCoffeeMachineClickEvent(machineId);
                     }
                     else if (hit.collider.GetComponent<Plates>() != null)
                     {
                         Debug.Log("platestate -  " + hit.collider.GetComponent<Plates>().plateState);
                         if(hit.collider.GetComponent<Plates>().plateState != PlateState.Unlocked)
                         hit.collider.GetComponent<Plates>().plateState = PlateState.Unlocked;
                         Debug.Log("platestate -  " + hit.collider.GetComponent<Plates>().plateState);
                         GameObject gameObject = hit.collider.gameObject;
                         EventHandler.Instance.InvokeOnReadyBreadClickEvent(gameObject);
                     }

                     else if (hit.collider.GetComponent<Trays>() != null)
                     {
                         if (hit.collider.GetComponent<Trays>().trayType == TrayType.StrawberryTray)
                         {
                             Debug.Log("hit with strawberry Tray");
                             EventHandler.Instance.InvokeOnStrawberryClickEvent();
                         }
                         else if (hit.collider.GetComponent<Trays>().trayType == TrayType.ChocolateTray)
                         {
                             Debug.Log("hit with chocolate Tray");
                             EventHandler.Instance.InvokeOnChocolateClickEvent();
                         }
                         else if (hit.collider.GetComponent<Trays>().trayType == TrayType.PeanutTray)
                         {
                             Debug.Log("hit with Penaut Tray");
                             EventHandler.Instance.InvokeOnPenautClickEvent();
                         }
                         else if (hit.collider.GetComponent<Trays>().trayType == TrayType.EggTray)
                         {
                             Debug.Log("hit with egg Tray");
                             EventHandler.Instance.InvokeOnEggClickEvent();
                         }

                     }
                    else
                    {
                        Handheld.Vibrate();
                    }
                }
                
            }
           
        }
        
    
    
    private void SetTableTopObjects(List<GameObject> SpawnSlots, GameObject initiatePrefab )
    {
        for (int i = 0; i < SpawnSlots.Count; i++)
        {
            GameObject childGameObject = Instantiate(initiatePrefab);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            childGameObject.transform.parent = SpawnSlots[i].transform;
            childGameObject.transform.localPosition = temp;

        }
    }

    private void SetTableTopObjects(List<GameObject> SpawnSlots, List<GameObject> initiatePrefab)
    {
        for (int i = 0; i < SpawnSlots.Count; i++)
        {
            GameObject childGameObject = Instantiate(initiatePrefab[i]);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            childGameObject.transform.parent = SpawnSlots[i].transform;
            childGameObject.transform.localPosition = temp;

        }
    }


    private void SetTableTopObjects(List<GameObject> SpawnSlots, GameObject [] initiatePrefab)
    {
        for (int i = 0; i < SpawnSlots.Count; i++)
        {
            GameObject childGameObject = Instantiate(initiatePrefab[i]);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            childGameObject.transform.parent = SpawnSlots[i].transform;
            childGameObject.transform.localPosition = temp;

        }
    }
    
    

}
