using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// CL dealing with Plates sprites, colliders and plates enum.
public class Plates : Element<Plates>
{
    //[SerializeField]
    //Sprite plateSprite;
    //int spriteSortingOrder = 3;
    public PlateState plateState = PlateState.Locked;
    public PlateStateBread plateStateBread = PlateStateBread.Free;
    public int plateId=0;


    /* private void Start()
     {
         //giving proper sprite at runtime to particular plate at giving order in layer nad adding collider.
         this.gameObject.AddComponent<SpriteRenderer>().sprite = plateSprite;
         this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteSortingOrder;
         this.gameObject.AddComponent<CircleCollider2D>();
     }*/
  
    
   
}
