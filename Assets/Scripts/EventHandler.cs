using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : GenericSingleton<EventHandler>
{
    public event Action OnBreadClick;


    public void InvokeOnBreadClick()
    {
        OnBreadClick?.Invoke();
    }
}
