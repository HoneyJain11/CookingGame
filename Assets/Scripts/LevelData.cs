using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/CreateLevelData")]
public class LevelData : ScriptableObject
{
    [Range(1, 5)]
    public int MaxToasterMachine;
    [Range(0, 3)]
    public int MaxCoffeeMachine;
    [Range(1, 5)]
    public int MaxPlates;
    [Range(1, 5)]
    public int MaxTrays;
    [Range(1,20)]
    public int MaxCustomers;
    [Range(1, 3)]
    public int maxRecipeOnWishList;
    public List<Recipe> LevelRecipes;
    public int levelGoal;
    public string levelGoalName;
    //public List<bool> enableCoffeeMachineList = new List<bool>(coffeeMachineCount);
    //public List<bool> enableCoffeeMachineList = new List<bool>(4);


}

/*[CreateAssetMenu(fileName = "BreadScriptableObject", menuName = "ScriptableObjects/BreadScriptableObject")]
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
}*/

