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
    public int no = 5;
    // give customer time by level SO;
    public int customerWaitingTime;
    GameObject newEmptyGameObject;
    GameObject childObject;
    [SerializeField]
    GameObject greenProgressLine;
    [SerializeField]
    GameObject timerBarBackGround;
    Vector2 progressLineLocalScale;
    Vector2 saveGreenProgressBarValue;
   
    private void OnEnable()
    {
        EventHandler.Instance.GiveSlotTransformToCustomer += SetCustomerOnSlot;
        EventHandler.Instance.SendMenuListToCustomer += SetRecipeOnWishList;
        EventHandler.Instance.OrderDelivered += MovePlayerToGate;
        customerWaitingTime = LevelManager.Instance.levelDataSO.levelCustomerWaitingTime;
        saveGreenProgressBarValue = greenProgressLine.transform.localScale;
        progressLineLocalScale = greenProgressLine.transform.localScale;
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

    private void SetRecipeOnWishList(Order order, int customerId)
    {
        if(this.customerId == customerId)
         {
             this.order = order;
             this.orderID = order.OrderID;
            // Have to changed this logic with down comment code. to overcome instiate- destroy
            for (int i = 0; i < order.RecipeList.Count; i++)
            {
               
                    newEmptyGameObject = Instantiate(emptyGameObject);
                    newEmptyGameObject.AddComponent<SpriteRenderer>().sprite = order.RecipeList[i].parentImage;
                    newEmptyGameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    newEmptyGameObject.transform.parent = recipeSpwanPoints[i].gameObject.transform;
                    newEmptyGameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

                    for (int j = 0; j < order.RecipeList[i].childImages.Length; j++)
                    {
                        childObject = Instantiate(emptyGameObject);
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
            this.greenProgressLine.SetActive(true);
            this.timerBarBackGround.SetActive(true);
            CustomerTimer();
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
            this.playerState = PlayerState.Idle;
            SetImageNullOfWishListChild();
            CustomerPooler.Instance.SetPooledObjectInPool(this.gameObject);
            CheckWinOrLose();
            this.gameObject.SetActive(false);
           
        }
    }

    private void CheckWinOrLose()
    {
        if(this.customerId == LevelManager.Instance.levelDataSO.totalCustomerWantToSpwan)
        {
            LevelManager.Instance.OpenWinLosePanel();
        }
    }

    private async void CustomerTimer()
    {
        if(this.customerWaitingTime > 0)
        {
            await new WaitForSeconds(2f);
            this.progressLineLocalScale.y -= Math.Abs(0.05f);
            this.greenProgressLine.transform.localScale = this.progressLineLocalScale;
            this.customerWaitingTime--;
            CustomerTimer();

        }
        else
        {

            this.wishList.SetActive(false);
            greenProgressLine.transform.localScale = saveGreenProgressBarValue;
            this.greenProgressLine.SetActive(false);
            this.timerBarBackGround.SetActive(false);
            MovePlayerOut();
            this.playerState = PlayerState.NotGotOrder;
            EventHandler.Instance.InvokeOnCallNextCustomer(this.target);
        }

    }
    private void SetImageNullOfWishListChild()
    {
        for (int i = 0; i < this.order.RecipeList.Count; i++)
        {
            Destroy(this.recipeSpwanPoints[i].transform.GetChild(0).gameObject);
        }
           
    }

    private void OnDisable()
    {
        EventHandler.Instance.SendMenuListToCustomer -= SetRecipeOnWishList;
        EventHandler.Instance.GiveSlotTransformToCustomer -= SetCustomerOnSlot;
        EventHandler.Instance.OrderDelivered -= MovePlayerToGate;

    }
}


//Have to changed logic in this code new code but not proper so, have to do proper and then put in SetRecipeOnWishList() method.
/*
for (int i = 0; i < order.RecipeList.Count; i++)
{
    if (recipeSpwanPoints[i].transform.childCount > 0)
    {
        var parent = recipeSpwanPoints[i].transform.GetChild(0).gameObject;
        parent.GetComponent<SpriteRenderer>().sprite = order.RecipeList[i].parentImage;

        for (int j = 0; j < order.RecipeList[i].childImages.Length; j++)
        {
            if (parent.transform.GetChild(j) != null)
            {
                parent.transform.GetChild(j).GetComponent<SpriteRenderer>().sprite = order.RecipeList[i].childImages[j];
            }
            else
            {
                childObject = Instantiate(emptyGameObject);
                childObject.AddComponent<SpriteRenderer>().sprite = order.RecipeList[i].childImages[j];
                childObject.GetComponent<SpriteRenderer>().sortingOrder = newEmptyGameObject.GetComponent<SpriteRenderer>().sortingOrder + 1 + j;
                childObject.transform.parent = newEmptyGameObject.gameObject.transform;
                childObject.transform.localPosition = new Vector3(0f, 0f, 0f);


            }


        }


    }
    else
    {
        newEmptyGameObject = Instantiate(emptyGameObject);
        newEmptyGameObject.AddComponent<SpriteRenderer>().sprite = order.RecipeList[i].parentImage;
        newEmptyGameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        newEmptyGameObject.transform.parent = recipeSpwanPoints[i].gameObject.transform;
        newEmptyGameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

        for (int j = 0; j < order.RecipeList[i].childImages.Length; j++)
        {
            childObject = Instantiate(emptyGameObject);
            childObject.AddComponent<SpriteRenderer>().sprite = order.RecipeList[i].childImages[j];
            childObject.GetComponent<SpriteRenderer>().sortingOrder = newEmptyGameObject.GetComponent<SpriteRenderer>().sortingOrder + 1 + j;
            childObject.transform.parent = newEmptyGameObject.gameObject.transform;
            childObject.transform.localPosition = new Vector3(0f, 0f, 0f);

        }
    }


}*/