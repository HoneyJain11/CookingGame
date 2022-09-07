using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeGlass : Element<CoffeeGlass>
{
    public int itemId = 0;
    protected override void Start()
    {
        base.Start();
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }
}
