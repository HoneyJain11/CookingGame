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

    
    private void Start()
    { //placing all the object on it's specific slots
        SetLeftMachineItems();
        SetRightMachineItems();
        SetPlates();
        SetTrays();
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

            
        }
    }
    //placing leftside machine on proper leftsideslot.
    private void SetLeftMachineItems()
    {
        for (int i = 0; i < leftMachineSlots.Count; i++)
        {
            GameObject leftMachine = Instantiate(leftMachinePrefab);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            leftMachine.transform.parent = leftMachineSlots[i].transform;
            leftMachine.transform.localPosition = temp;

        }
    }
    //placing rightside machine on proper rightsideslot.
    private void SetRightMachineItems()
    {
        for (int i = 0; i < rightMachineSlots.Count; i++)
        {
            GameObject rightMachine = Instantiate(rightMachinePrefab);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            rightMachine.transform.parent = rightMachineSlots[i].transform;
            rightMachine.transform.localPosition = temp;

        }
    }
    //placing plates on correct slot.
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
    //placing tarys on correct slot.needs array of prefabs ex - chocolate tary prefab, peanuts tray prefab. 
    private void SetTrays()
    {
        for (int i = 0; i < traySlots.Count; i++)
        {
            GameObject tray = Instantiate(trayPrefabs[i]);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            tray.transform.parent = traySlots[i].transform;
            tray.transform.localPosition = temp;

        }
    }

}
