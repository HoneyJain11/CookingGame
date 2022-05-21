using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastBread : Element <ToastBread>
{
    [SerializeField] GameObject readyBread;

    private void OnEnable()
    {
        EventHandler.Instance.OnBreadClick += SetBreadOnToaster;
    }
    public void SetBreadOnToaster()
    {

    }
    private void OnDisable()
    {
        EventHandler.Instance.OnBreadClick -= SetBreadOnToaster;
    }
}
