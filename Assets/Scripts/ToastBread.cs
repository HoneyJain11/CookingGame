using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastBread : Element <ToastBread>
{
    public int itemId = 0;
    protected override void Start()
    {
        base.Start();
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
    }
}

