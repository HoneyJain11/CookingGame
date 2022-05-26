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
}


