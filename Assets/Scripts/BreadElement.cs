using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadElement : MonoBehaviour
{
    // Here We broadcast an Event to its Listner through EventHandler Singleton
    // Class.



    private void Start()
    {
        EventHandler.Instance.OnBreadClick += FindEmptyToaster;
        
    }

    // LevelManger is broadcaster

    private void OnDisable()
    {
        EventHandler.Instance.OnBreadClick -= FindEmptyToaster;
    }

    private void FindEmptyToaster()
    {
        // Output of Raw Material Bread
        print("In Method Find Empty Toaster");
        EventHandler.Instance.InvokeOnFindEmptyToaster();
    }


}