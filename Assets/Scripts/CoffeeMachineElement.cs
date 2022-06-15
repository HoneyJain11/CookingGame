using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachineElement : MonoBehaviour
{
    [SerializeField]
    GameObject coffeeMachinePrefab;
    [SerializeField]
    List<GameObject> rightMachineSlots;
    List<GameObject> coffeeMachineList;
    [SerializeField]
    GameObject coffeeGlassPrefab;
    [SerializeField]
    Sprite fullCoffeeGlass;
    [SerializeField]
    Recipe recipeSO;

    [SerializeField]
    GameObject coffeeAnimatioPrefab;
    [SerializeField]
    Sprite[] coffeeAnimationSprite;
    [SerializeField]
    int coffeeAnimationDuration;
    int remaingcoffeeAnimationDuration = 0;


    private void Start()
    {
        EventHandler.Instance.OnCoffeeMachineWorkCompleted += ChangeGlass;
        EventHandler.Instance.OnFindFreeCoffeeMachine += CoffeeMachineAnimation;
        EventHandler.Instance.OnStartCoffeeMachineAnimation += StartCoffeeAnimation;
        EventHandler.Instance.OnCoffeeMachineClick += CoffeeMachineClick;
        remaingcoffeeAnimationDuration = coffeeAnimationDuration;
        coffeeMachineList = new List<GameObject>();
        SetCoffeeMachine(rightMachineSlots , coffeeMachinePrefab);
        SetCoffeeGlassOnMachine();
    }

    private void CoffeeMachineClick(int machineId)
    {
        print("CoffeeMachineClick");
        for (int i = 0; i < coffeeMachineList.Count; i++)
        {
            GameObject temp = coffeeMachineList[i].gameObject;
            if (temp.GetComponent<Machine>().machineId == machineId && temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite == recipeSO.parentImage)
            {
                print("CoffeeMachineClick");
                print("checked with recipe item coffe done");
                coffeeMachineList[i].gameObject.GetComponent<Machine>().OnMachineTap();
                Debug.Log(" " + " " + coffeeMachineList[i].gameObject.GetComponent<Machine>().machineId + machineId);
                Debug.Log(" " + coffeeMachineList[i].gameObject.GetComponent<Machine>().MachineMode);
                break;

            }
        }
    }

    private void CoffeeMachineAnimation()
    {
        
        for (int i = 0; i < coffeeMachineList.Count; i++)
        {
            if (coffeeMachineList[i].gameObject.GetComponent<Machine>().MachineMode == MachineMode.Working)
            {
       
                GameObject coffeeAnimation = Instantiate(coffeeAnimatioPrefab);
                coffeeAnimation.transform.parent = coffeeMachineList[i].gameObject.transform;
                coffeeAnimation.transform.localPosition = new Vector3(-0.2f, -0.2f, 0f);
                Debug.Log(" " + coffeeMachineList[i].gameObject.GetComponent<Machine>().MachineMode);
                EventHandler.Instance.InvokeCoffeeMachineAnimation(coffeeAnimation);
            }
        }
        
    }

    private async void StartCoffeeAnimation(GameObject gameObject)
    {
        Debug.Log("In startCoffeeAnimation ");
        if(remaingcoffeeAnimationDuration >= 0)
        {
            Debug.Log("In startCoffeeAnimation condition ");
            for (int i = 0; i < coffeeAnimationSprite.Length; i++)
            {
                Debug.Log("In startCoffeeAnimation condition loop ");
                gameObject.GetComponent<SpriteRenderer>().sprite = coffeeAnimationSprite[i];
                await new WaitForSeconds(0.8f);
            }
            
            remaingcoffeeAnimationDuration--;
            StartCoffeeAnimation(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    private void ChangeGlass()
    {
        print("Into ChangeGlass");
        for (int i = 0; i < coffeeMachineList.Count; i++)
        {
            if (coffeeMachineList[i].gameObject.GetComponent<Machine>().MachineMode == MachineMode.WorkCompleted && coffeeMachineList[i].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite != fullCoffeeGlass)
            {
                print("Into ChangeGlass Loop");
                coffeeMachineList[i].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = fullCoffeeGlass;
                coffeeMachineList[i].transform.GetChild(1).gameObject.GetComponent<CoffeeGlass>().itemId = 3;
                coffeeMachineList[i].gameObject.GetComponent<Machine>().greenTimer.SetActive(false);
                Debug.Log(" " + coffeeMachineList[i].gameObject.GetComponent<Machine>().MachineMode);
                break;
            }
        }
    }

    private void SetCoffeeMachine(List<GameObject> SpawnSlots, GameObject initiatePrefab)
    {
        for (int i = 0; i < SpawnSlots.Count; i++)
        {
            GameObject CoffeeMachine = Instantiate(initiatePrefab);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            CoffeeMachine.transform.parent = SpawnSlots[i].transform;
            CoffeeMachine.transform.localPosition = temp;
            CoffeeMachine.GetComponent<Machine>().machineId = i + 1;
            coffeeMachineList.Add(CoffeeMachine);
        }
    }

   public  void SetCoffeeGlassOnMachine()
    {
        print("SetCoffeeGlassOnMachine");
        for (int i = 0; i < coffeeMachineList.Count; i++)
        {
            if (coffeeMachineList[i].gameObject.GetComponent<Machine>().MachineMode == MachineMode.Idle)
            {
                GameObject coffeeGlass = Instantiate(coffeeGlassPrefab);
                coffeeGlass.transform.parent = coffeeMachineList[i].gameObject.transform;
                coffeeGlass.transform.localPosition = new Vector3(-0.214f, -0.293f, 0f);
                coffeeMachineList[i].gameObject.GetComponent<Machine>().OnWorkingMode();
                Debug.Log(" " + coffeeMachineList[i].gameObject.GetComponent<Machine>().MachineMode);
                
            }
        }

    }
    private void OnDisable()
    {
        EventHandler.Instance.OnCoffeeMachineWorkCompleted -= ChangeGlass;
        EventHandler.Instance.OnFindFreeCoffeeMachine -= CoffeeMachineAnimation;
        EventHandler.Instance.OnStartCoffeeMachineAnimation -= StartCoffeeAnimation;
        EventHandler.Instance.OnCoffeeMachineClick -= CoffeeMachineClick;
    }



}
