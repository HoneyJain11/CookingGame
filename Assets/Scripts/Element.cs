using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element <T>: MonoBehaviour where T: Element<T>
{
    [SerializeField]
     Sprite objectSprite;
    [SerializeField]
     int orderInLayer;

    protected virtual void Start()
    {
        this.gameObject.AddComponent<SpriteRenderer>().sprite = objectSprite;
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        this.gameObject.AddComponent<BoxCollider2D>();
    }
}

