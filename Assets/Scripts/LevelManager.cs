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
    List<GameObject> platesSlots;
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


    private void Start()
    { //placing all the object on it's specific slots

        //Set LeftTable Objects here eg. Toaster
        //placing leftside machine on proper leftsideslot.
        SetTableTopObjects(leftMachineSlots, leftMachinePrefab);

        //Set RightTable Objects here eg.Coffee Machine
        //placing rightside machine on proper rightsideslot.
        SetTableTopObjects(rightMachineSlots, rightMachinePrefab);
        //SetRightMachineItems();

        //Set Tray Objects here eg. strawberry, chocolate, eggs, peanuts
        //placing tarys on correct slot.needs array of prefabs ex - chocolate tary prefab, peanuts tray prefab. 
        SetTableTopObjects(traySlots, trayPrefabs);

        // Set Serving Area Objects on Table top here eg. 4 nos. of Plates
        //placing plates on correct slot.
        SetTableTopObjects(platesSlots, platesPrefab);

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
            if (hit && hit.collider != null && hit.collider.gameObject.GetComponent<Machine>())
            {
                Machine temp = hit.collider.gameObject.GetComponent<Machine>();


                if (temp.MachineMode == MachineMode.Idle)
                {
                    Debug.Log("Machine Name " + hit.collider.name);
                    temp.OnWorkingMode();
                }
                else if (temp.MachineMode == MachineMode.WorkCompleted)
                {

                    Debug.Log(temp.MachineName + " " + "Work Completed");
                    temp.OnMachineTap();

                    // Now here your process food is ready to serve.
                }
                else if (temp.MachineMode == MachineMode.Working)
                {
                    Debug.Log(temp.MachineName + " " + "is Working");
                }
                else
                {
                    Debug.Log(temp.MachineName + " " + "is Working");
                }
            }
            else if (hit && hit.collider != null && hit.collider.GetComponent<Plates>())
            {
                Debug.Log("platestate -  " + hit.collider.GetComponent<Plates>().plateState);
                if(hit.collider.GetComponent<Plates>().plateState != PlateState.Unlocked)
                hit.collider.GetComponent<Plates>().plateState = PlateState.Unlocked;
                Debug.Log("platestate -  " + hit.collider.GetComponent<Plates>().plateState);
            }
            else if (hit && hit.collider != null && hit.collider.GetComponent<CircleCollider2D>())
            {
                Debug.Log("collide with main bread -  " );
                
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
    private void SetPlates()
    {
        for (int i = 0; i < platesSlots.Count; i++)
        {
            GameObject plates = Instantiate(platesPrefab);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            plates.transform.parent = platesSlots[i].transform;
            plates.transform.localPosition = temp;
            if(platesUnlocked > 0)
            {
                plates.GetComponent<Plates>().plateState = PlateState.Unlocked;
                platesUnlocked--;

            }
            Debug.Log("platestate - " + plates.GetComponent<Plates>().plateState);

        }
    }
    

   


}
