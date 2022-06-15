using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    GameObject wishList;
    Vector2 move = Vector2.left;
    [SerializeField]
     float speed;
    [SerializeField]
    GameObject[] recipeSpwanPoints;
    [SerializeField]
    GameObject emptyGameObject;
    private Vector3 target;
    float step;
    public int customerId;
    int slotID;
    private void OnEnable()
    {
        EventHandler.Instance.GiveSlotTransformToCustomer += SetCustomerOnSlot;
        EventHandler.Instance.SendMenuListToCustomer += SetRecipeOnWishList;
    }


    private void SetCustomerOnSlot(Vector3 position ,int slotID)
    {
        target = position;
        this.slotID = slotID;
        if(this.customerId == slotID)
        {
            MovePlayer();
        }
       
    }

    private void SetRecipeOnWishList(List<Recipe> temp , int maxRecipeItem)
    {
        if(this.customerId == slotID)
        {
            for (int i = 0; i < maxRecipeItem; i++)
            {
                GameObject newEmptyGameObject = Instantiate(emptyGameObject);
                newEmptyGameObject.AddComponent<SpriteRenderer>().sprite = temp[i].parentImage;
                newEmptyGameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                newEmptyGameObject.transform.parent = recipeSpwanPoints[i].gameObject.transform;
                newEmptyGameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

                for (int j = 0; j < temp[i].childImages.Length; j++)
                {
                    GameObject childObject = Instantiate(emptyGameObject);
                    childObject.AddComponent<SpriteRenderer>().sprite = temp[i].childImages[j];
                    childObject.GetComponent<SpriteRenderer>().sortingOrder = newEmptyGameObject.GetComponent<SpriteRenderer>().sortingOrder + 1 + j;
                    childObject.transform.parent = newEmptyGameObject.gameObject.transform;
                    childObject.transform.localPosition = new Vector3(0f, 0f, 0f);

                }

            }

        }

    }

    
    private async void MovePlayer()
    {

        /* this.transform.position = new Vector2((this.transform.position.x + move.x * speed * Time.deltaTime),

                                               this.transform.position.y);


         if (this.transform.position.x < 0)
         {
             speed = 0;
             wishList.SetActive(true);

         }*/
 
        step = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, this.target, step);
        if (this.transform.position != this.target)
        {
            await new WaitForSeconds(0.01f);
            MovePlayer();
        }
        else
        {
            EventHandler.Instance.InvokeGetMenuItemsFromMenuManger();
            this.wishList.SetActive(true);
        }

    }

    private void OnDisable()
    {
        EventHandler.Instance.SendMenuListToCustomer -= SetRecipeOnWishList;
        EventHandler.Instance.GiveSlotTransformToCustomer -= SetCustomerOnSlot;
    }
}
