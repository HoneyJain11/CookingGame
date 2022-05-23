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

}


