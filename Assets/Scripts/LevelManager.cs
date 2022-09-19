using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    List<ToasterSO> toasterSO;
    [SerializeField]
    int maxRecipeItem;
    [SerializeField]
    List<Recipe> recipesSO;
    //[SerializeField]
    //LeftSideMachineSO toasterSO;
    [SerializeField] LevelData levelDataSO;
    //To Check Double Click Parameters.
    private float firstClickTime, timeBetweenClick;
    private bool coroutineAllowed;
    private int clickCounter;
    [SerializeField]
    GameObject goalPanel;
    [SerializeField]
    TextMeshProUGUI levelGoalText;
    [SerializeField]
    TextMeshProUGUI totalCustomerText;
    [SerializeField]
    GameObject WinLosePanel;
    [SerializeField]
    TextMeshProUGUI WinLoseText;
    public int levelGoal = 0;
    public int noOfCustomerSpwaned = 0;
    private void OnEnable()
    {
       // EventHandler.Instance.InvokeGiveRecipeSOToMenuManager(levelDataSO.LevelRecipes, levelDataSO.LevelRecipes.Count);
    }

    private  void Start()
    {
        SetUIOfLevel();
      
       //placing all the object on it's specific slot
       //Set LeftTable Objects here eg. Toaster
       //placing leftside machine on proper leftsideslot.
       //  SetTableTopObjects(leftMachineSlots, breadSO.toastMachinePrefab);
        EventHandler.Instance.InvokeGiveRecipeSOToMenuManager(levelDataSO.LevelRecipes, levelDataSO.maxRecipeOnWishList);
        //Set RightTable Objects here eg.Coffee Machine
        //placing rightside machine on proper rightsideslot.
        //SetTableTopObjects(rightMachineSlots, rightMachinePrefab);
        //SetRightMachineItems();

        //Set Tray Objects here eg. strawberry, chocolate, eggs, peanuts
        //placing tarys on correct slot.needs array of prefabs ex - chocolate tary prefab, peanuts tray prefab. 
        SetTableTopObjects(traySlots, trayPrefabs);

        // Set Serving Area Objects on Table top here eg. 4 nos. of Plates
        //placing plates on correct slot.

        //To Check Double Click Parameters.
        firstClickTime = 0f;
        timeBetweenClick = 0.2f;
        clickCounter = 0;
        coroutineAllowed = true;

    }

    private async void SetUIOfLevel()
    {
        levelGoalText.text = levelDataSO.levelGoalName;
        totalCustomerText.text = totalCustomerText.text+levelDataSO.levelGoal.ToString();
        goalPanel.SetActive(true);
        await new WaitForSeconds(5f);
        goalPanel.SetActive(false);

    }
    
    private void Update()
    {
        MouseClick();

        if(noOfCustomerSpwaned == levelDataSO.MaxCustomers )
        {
            OpenWinLosePanel();

        }
    }

    private void OpenWinLosePanel()
    {
        if (levelGoal >= levelDataSO.levelGoal)
        {
            WinLosePanel.SetActive(true);
            WinLoseText.text = "Congratulations You Passed Level";
        }
       

        if (levelGoal != levelDataSO.levelGoal)
        {
            WinLosePanel.SetActive(true);
            WinLoseText.text = "Opps You Failed Level";
        }
    }

    // checking the mouseclick , on which raycast is hitting
    public void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCounter += 1;
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
                         GameObject gameObject = hit.collider.gameObject;
                         EventHandler.Instance.InvokeOnReadyBreadClickEvent(gameObject);
                     }
                     else if (hit.collider.GetComponent<Plates>() != null)
                     {
                         Debug.Log("platestate -  " + hit.collider.GetComponent<Plates>().plateState);
                         if(hit.collider.GetComponent<Plates>().plateState != PlateState.Unlocked)
                         hit.collider.GetComponent<Plates>().plateState = PlateState.Unlocked;
                         Debug.Log("platestate -  " + hit.collider.GetComponent<Plates>().plateState);
                         Debug.Log("hit gameobject -  " + hit.collider.gameObject);
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
            if(clickCounter == 1 && coroutineAllowed)
            {
                firstClickTime = Time.time;
                DoubleClickDetection();



            }


    }

    private async void DoubleClickDetection()
    {
        coroutineAllowed = false;
        while (Time.time < firstClickTime + timeBetweenClick)
        {
            if (clickCounter == 2)
            {
                 Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                 RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.zero);

                 if (hit && hit.collider != null)
                 {
                     if (hit.collider.GetComponent<PlateElement>())
                     {
                         Debug.Log("Click on paltes");
                         Debug.Log("Collider name object paltes " + hit.collider.gameObject);
                         EventHandler.Instance.InvokeOnRemoveBreadToDustBin(hit.collider.gameObject);
                         break;

                     }

                 }
               // Debug.Log("Click on paltes");
               
            }
            await new WaitForEndOfFrame();
        }
            clickCounter = 0;
            firstClickTime = 0f;
            coroutineAllowed = true;
        
    }

    private void SetTableTopObjects(List<GameObject> SpawnSlots, GameObject initiatePrefab )
    {
        for (int i = 0; i < levelDataSO.MaxTrays; i++)
        {
            GameObject childGameObject = Instantiate(initiatePrefab);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            childGameObject.transform.parent = SpawnSlots[i].transform;
            childGameObject.transform.localPosition = temp;

        }
    }

    private void SetTableTopObjects(List<GameObject> SpawnSlots, List<GameObject> initiatePrefab)
    {
        for (int i = 0; i < levelDataSO.MaxTrays; i++)
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
