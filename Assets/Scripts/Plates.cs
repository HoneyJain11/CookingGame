using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// CL dealing with Plates sprites, colliders and plates enum.
public class Plates : MonoBehaviour
{
    [SerializeField]
    Sprite plateSprite;
    int spriteSortingOrder = 3;
    public PlateState plateState = PlateState.Locked;

    private void Start()
    {
        this.gameObject.AddComponent<SpriteRenderer>().sprite = plateSprite;
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteSortingOrder;
        this.gameObject.AddComponent<CircleCollider2D>();
    }

}
