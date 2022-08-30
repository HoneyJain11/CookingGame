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
    private GameObject emptyGameObject;
    public Vector3 target;
    float step;
    public int customerId;
    int slotID;
    public int orderID;
    public Order order;
    public Vector3 gatePosition;
    public PlayerState playerState = PlayerState.Idle;
   

    private void OnEnable()
    {
        EventHandler.Instance.GiveSlotTransformToCustomer += SetCustomerOnSlot;
        EventHandler.Instance.SendMenuListToCustomer += SetRecipeOnWishList;
        EventHandler.Instance.OrderDelivered += MovePlayerToGate;

    }
   
    private void SetCustomerOnSlot(Vector3 target, int slotID)
    {

         this.slotID = slotID;
         if (customerId == slotID)
         {

             this.target = target;
             MovePlayer();
         }
       
        
       
    }

    private void SetRecipeOnWishList(Order order , int customerId)
    {
        if(this.customerId == customerId)
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
        this.playerState = PlayerState.Waliking;
        if (this.transform.position != this.target)
        {
            await new WaitForSeconds(0.01f);
            MovePlayer();
        }
        else
        {
            EventHandler.Instance.InvokeGetMenuItemsFromMenuManger(this.customerId);
            this.wishList.SetActive(true);
            this.playerState = PlayerState.Waiting;
        }

    }
    private void MovePlayerToGate(int id)
    {
        if (this.customerId == id)
        {
            MovePlayerOut();
            this.wishList.SetActive(false);
            this.playerState = PlayerState.GotOrder;
        }

    }
   private async void MovePlayerOut()
    {
        step = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, this.gatePosition, step);
        if (this.transform.position != this.gatePosition)
        {
            await new WaitForSeconds(0.01f);
            MovePlayerOut();
        }
        else
        {
            this.customerId = this.customerId + 6;
            this.playerState = PlayerState.Idle;
            CustomerPooler.Instance.SetPooledObjectInPool(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        EventHandler.Instance.SendMenuListToCustomer -= SetRecipeOnWishList;
        EventHandler.Instance.GiveSlotTransformToCustomer -= SetCustomerOnSlot;
        EventHandler.Instance.OrderDelivered -= MovePlayerToGate;
    }
}
