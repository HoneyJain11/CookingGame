using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Level1SO : ScriptableObject
{
    
}

[CreateAssetMenu(fileName = "BreadScriptableObject", menuName = "ScriptableObjects/BreadScriptableObject")]
public class BreadSO : ScriptableObject
{ 
  public GameObject toastBreadPrefab;
  public GameObject plateBreadPrefab;
  public GameObject[] toastMachinePrefab;

  public  event  Action <GameObject> OnBreadClick;   

  public void InvokeOnBreadClickEvent()
  {
        Debug.Log("InvokeOnBreadClickEvent called");
        OnBreadClick?.Invoke(toastBreadPrefab);
  }

    public void  SetToastBreadOnToaster()
    {
        for(int i =0; i < toastMachinePrefab.Length; i++)
        {
            if(toastMachinePrefab[i].gameObject.GetComponent<Machine>().MachineMode == MachineMode.Idle)
            {
                GameObject toastBread = Instantiate(toastBreadPrefab);
                toastBread.transform.position = new Vector3(0f, 0f, 0f);
                toastMachinePrefab[i].gameObject.GetComponent<Machine>().OnWorkingMode();
                //toastMachinePrefab[i].gameObject.GetComponent<Machine>().MachineMode = MachineMode.Working;
                Debug.Log(" " + toastMachinePrefab[i].gameObject.GetComponent<Machine>().MachineMode);
                break;
            }

        }
    }
}



[CreateAssetMenu(fileName = "RightMachineScriptableObject", menuName = "ScriptableObjects/RightMachineScriptableObject")]
public class RightSideMachineSO : ScriptableObject
{
    public GameObject rightSideMachinePrefab;
}

[CreateAssetMenu(fileName = "LeftMachineScriptableObject", menuName = "ScriptableObjects/LeftMachineScriptableObject")]
public class LeftSideMachineSO : ScriptableObject
{
    public List <GameObject> leftSideMachinePrefab;
}

