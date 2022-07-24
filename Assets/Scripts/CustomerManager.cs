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
    public int orderID;
    public Order order;
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

    private void SetRecipeOnWishList(Order order)
    {
        if(this.customerId == slotID)
        {
            this.order = order;
            this.orderID = order.OrderID;

            for (int i = 0; i < order.RecipeList.Count; i++)
            {
                GameObject newEmptyGameObject = Instantiate(emptyGameObject);
                newEmptyGameObject.AddComponent<SpriteRenderer>().sprite = order.RecipeList[i].parentImage;
                newEmptyGameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                newEmptyGameObject.transform.parent = recipeSpwanPoints[i].gameObject.transform;
                newEmptyGameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

                for (int j = 0; j < order.RecipeList[i].childImages.Length; j++)
                {
                    GameObject childObject = Instantiate(emptyGameObject);
                    childObject.AddComponent<SpriteRenderer>().sprite = order.RecipeList[i].childImages[j];
                    childObject.GetComponent<SpriteRenderer>().sortingOrder = newEmptyGameObject.GetComponent<SpriteRenderer>().sortingOrder + 1 + j;
                    childObject.transform.parent = newEmptyGameObject.gameObject.transform;
                    childObject.transform.localPosition = new Vector3(0f, 0f, 0f);

                }

            }

        }

    }

    
    private async void MovePlayer()
    {
 
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
