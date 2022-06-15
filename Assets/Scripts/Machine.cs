using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Machine : Element<Machine>
{
    public int machineId =0;
    [SerializeField]
    string machineName;
    [SerializeField]
    public MachineType machineType;
    //[SerializeField]
    // Sprite machineSprite;
    [SerializeField]
    GameObject rawMaterial;
    [SerializeField]public GameObject greenTimer;
    [SerializeField] GameObject burnTimer;
    //GameObject animaton;
    //GameObject[] machines;
    [SerializeField]
    MachineMode machineMode = MachineMode.Idle;
    //Duration time will be given by raw material to machine. ex- bread need 2s , loaf needs 4s
    [SerializeField]int duration;
    //burnig Duration time will be given by raw material to machine. ex- bread need 2s , loaf needs 4s
    [SerializeField] int burningDuration;
    int remainigBurningDuration;
    //[SerializeField] Sprite timerFill;
    int remainigDuration;
   // int spriteSortingOrder = 3;
    Timer timer;
    [SerializeField]
    BreadSO breadSO;

    // Getter Setter
    #region

    public MachineMode MachineMode { get => machineMode; set => machineMode = value; }
    public string MachineName { get => machineName;}
    #endregion
    private void Awake()
    {
         timer = new Timer();
    }

    /* private void Start()
     {
         //giving proper sprite at runtime to particular machine at giving order in layer nad adding collider.
         this.gameObject.AddComponent<SpriteRenderer>().sprite = machineSprite;
         this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteSortingOrder;
         this.gameObject.AddComponent<BoxCollider2D>();
     }*/
    // when click detect on maching this FN will call.changes machine state to working mode and starts green timer
    public async void OnWorkingMode()
    {
        remainigDuration = duration;
        MachineMode = MachineMode.Working;
        greenTimer.SetActive(true);
        if (machineType == MachineType.CoffeeMachine)
        {
            Debug.Log("In Invoke find Free FreeCoffeeMachine");
            EventHandler.Instance.InvokeFindFreeCoffeeMachine();
        }
        while (remainigDuration > 0)
        {
            await new WaitForSeconds(1);
            remainigDuration--;
            Debug.Log("remainigDuration  " + remainigDuration);
        }
        if (machineType == MachineType.Toaster)
        {
            Debug.Log("In working mode in toaster condition");
            FoodBurning();
        }
        else if(machineType == MachineType.CoffeeMachine)
        {
            Debug.Log("In working mode in coffee condition");
            MachineMode = MachineMode.WorkCompleted;
            EventHandler.Instance.InvokeCoffeeMachineWorkCompleted();
        }
            

    }
    // when correect timer will over this FN will call, this FN will set machine state to work completed state and start the burn timer
    private async void FoodBurning()
    {
        MachineMode = MachineMode.WorkCompleted;
        if(machineType == MachineType.Toaster)
        {
            EventHandler.Instance.InvokeOnToasterWorkCompletedEvent();

        }
        remainigBurningDuration = burningDuration;
        greenTimer.SetActive(false);
        burnTimer.SetActive(true);
        while ((remainigBurningDuration > 0) && (MachineMode == MachineMode.WorkCompleted))
        {
            await new WaitForSeconds(1);
            remainigBurningDuration--;
            Debug.Log("remainigBurningDuration  " + remainigBurningDuration);
        }
       
    }
    // this FN will work only when green timer stops , and change machine mode to idle and stop burn timer.
    public void OnMachineTap()
    {
        MachineMode = MachineMode.Idle;
        if (machineType == MachineType.CoffeeMachine)
        {


            Debug.Log("In Coffee machine idle condition");
        }

            if (machineType == MachineType.Toaster)
        {
            burnTimer.SetActive(false);
            EventHandler.Instance.InvokeSpwanReadyBreadEvent();

        }

    }


}
