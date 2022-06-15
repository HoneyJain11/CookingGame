using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : GenericSingleton<EventHandler>
{
    public event Action OnBreadClick;
    public event Action OnFindEmptyToaster;
    public event Action <int> OnToasterClick;
    public event Action OnToasterWorkCompleted;
    public event Action SpwanReadyBread;
    public event Action OnStrawberryClick;
    public event Action OnChocolateClick;
    public event Action OnPenautClick;
    public event Action OnEggClick;
    public event Action <GameObject> OnReadyBreadClick;
    public event Action OnCoffeeMachineWorkCompleted;
    public event Action <GameObject> OnStartCoffeeMachineAnimation;
    public event Action OnFindFreeCoffeeMachine;
    public event Action <int> OnCoffeeMachineClick;
    public event Action<List<Recipe> , int> GiveRecipeSOToMenuManager;
    public event Action GetMenuItemsFromMenuManger;
    public event Action <List<Recipe>, int> SendMenuListToCustomer;
    public event Action<Vector3 , int> GiveSlotTransformToCustomer;

    public void InvokeOnBreadClickEvent()
    {
        OnBreadClick?.Invoke();
    }

    public void InvokeOnFindEmptyToaster()
    {
        OnFindEmptyToaster?.Invoke();
    }

    public void InvokeOnToasterClickEvent(int machineId)
    {
        OnToasterClick?.Invoke(machineId);
    }

    public void InvokeOnToasterWorkCompletedEvent()
    {
        OnToasterWorkCompleted?.Invoke();
    }

    public void InvokeSpwanReadyBreadEvent()
    {
        SpwanReadyBread?.Invoke();
    }

    public void InvokeGiveSlotTransformToCustomer(Vector3 position , int slotId)
    {
       GiveSlotTransformToCustomer?.Invoke(position , slotId);
    }

    public void InvokeOnStrawberryClickEvent()
    {
        OnStrawberryClick?.Invoke();
    }

    public void InvokeOnChocolateClickEvent()
    {
        OnChocolateClick?.Invoke();
    }

    public void InvokeOnPenautClickEvent()
    {
        OnPenautClick?.Invoke();
    }

    public void InvokeOnEggClickEvent()
    {
        OnEggClick?.Invoke();
    }

    public void InvokeOnReadyBreadClickEvent(GameObject gameObject)
    {
        OnReadyBreadClick?.Invoke(gameObject);
    }

    public void InvokeCoffeeMachineWorkCompleted()
    {
        OnCoffeeMachineWorkCompleted?.Invoke();
    }

    public void InvokeCoffeeMachineAnimation(GameObject gameObject)
    {
        OnStartCoffeeMachineAnimation?.Invoke(gameObject);
    }

    public void InvokeFindFreeCoffeeMachine()
    {
        OnFindFreeCoffeeMachine?.Invoke();
    }

    public void InvokeOnCoffeeMachineClickEvent(int machineId)
    {
        OnCoffeeMachineClick?.Invoke(machineId);
    }

    public void InvokeGiveRecipeSOToMenuManager(List<Recipe> recipe , int maxRecipeItem)
    {
        GiveRecipeSOToMenuManager?.Invoke(recipe , maxRecipeItem);
    }

    public void InvokeGetMenuItemsFromMenuManger()
    {
        GetMenuItemsFromMenuManger?.Invoke();
    }

    public void InvokeSendMenuListToCustomer(List<Recipe> tempList , int maxRecipeItem)
    {
        SendMenuListToCustomer?.Invoke(tempList, maxRecipeItem);
    }
}


