using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToasterSO", menuName = "Scriptable Objects/Make Machine")]
public class ToasterSO : ScriptableObject
{
    [SerializeField]
    int machineId=0;
    public string machineName;
    [SerializeField]
    MachineType machineType;
    //[SerializeField]
    // Sprite machineSprite;
    //[SerializeField]
    //GameObject rawMaterial;
    //[SerializeField] GameObject greenTimer;
    //[SerializeField] GameObject burnTimer;
   
   
   
    public MachineMode machineMode = MachineMode.Idle;
    //Duration time will be given by raw material to machine. ex- bread need 2s , loaf needs 4s
    [SerializeField] int duration;
    //burnig Duration time will be given by raw material to machine. ex- bread need 2s , loaf needs 4s
    [SerializeField] int burningDuration;
    int remainigBurningDuration;
    //[SerializeField] Sprite timerFill;
    int remainigDuration;


    
    public GameObject toasterPrefab;

    [SerializeField]
    Sprite objectSprite;
    [SerializeField]
    int orderInLayer;

    private void OnEnable()
    {
        if(!this.toasterPrefab.GetComponent<SpriteRenderer>().sprite)
        {
            this.toasterPrefab.AddComponent<SpriteRenderer>().sprite = objectSprite;
            this.toasterPrefab.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
            this.toasterPrefab.AddComponent<BoxCollider2D>().enabled = false;

        }
        this.toasterPrefab.GetComponent<BoxCollider2D>().enabled = false;
        if(machineMode != MachineMode.Idle)
        {

            machineMode = MachineMode.Idle;
        }
    }
  

    public async void OnWorkingMode()
    {
        remainigDuration = duration;
        machineMode = MachineMode.Working;
        //greenTimer.SetActive(true);
        while (remainigDuration > 0)
        {
            await new WaitForSeconds(1);
            remainigDuration--;
            Debug.Log("Duration  " + remainigDuration);
        }
        FoodBurning();
    }
    // when correect timer will over this FN will call, this FN will set machine state to work completed state and start the burn timer
    private async void FoodBurning()
    {
        this.toasterPrefab.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        machineMode = MachineMode.WorkCompleted;
        remainigBurningDuration = burningDuration;
        //greenTimer.SetActive(false);
        //burnTimer.SetActive(true);
        while ((remainigBurningDuration > 0) && (machineMode == MachineMode.WorkCompleted))
        {
            await new WaitForSeconds(1);
            remainigBurningDuration--;
            Debug.Log("Burning Duration  " + remainigBurningDuration);
            //this.toasterPrefab.gameObject.GetComponent<SpriteRenderer>
        }
    }
    // this FN will work only when green timer stops , and change machine mode to idle and stop burn timer.
    public void OnMachineTap()
    {
        this.toasterPrefab.GetComponent<BoxCollider2D>().enabled = false;
        machineMode = MachineMode.Idle;
        //burnTimer.SetActive(false);

    }

}
