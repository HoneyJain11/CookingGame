using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuListManager : MonoBehaviour
{
    List<Recipe> RecipeSO;
    List<Order> orderMasterList;
    List<Recipe> itemList;
    int maxRecipeItem;
    int j = 0;
   

    private void OnEnable()
    {
        EventHandler.Instance.GiveRecipeSOToMenuManager += TakeRecipeSOFromLevelManager;
        EventHandler.Instance.GetMenuItemsFromMenuManger += GiveMenuItemsToCustomer;
    }
    private void Start()
    {
        orderMasterList = new List<Order>();
        orderMasterList.Clear();

    }
    private void GiveMenuItemsToCustomer(int customerId)
    {
        itemList=GetRandom(RecipeSO);
        for (int i =0;i< itemList.Count;i++)
        {
            print("Menu Items To Customer " + itemList[i].recipeId);
        }
        SendMenuItemsToCustomer(customerId);
    }


    private void SendMenuItemsToCustomer(int customerId)
    {
        List<Recipe> orderList = new List<Recipe>();
        List<bool> isItemDelivered = new List<bool>();
        j++; 
        int recipeItemNo = Random.Range(1, maxRecipeItem);
        for (int i =0; i < recipeItemNo; i++)
        {
            orderList.Add(itemList[i]);
            isItemDelivered.Add(false);


        }
        Order order = new Order(j, orderList , isItemDelivered);
        orderMasterList.Add(order);
        for (int i = 0; i < orderMasterList.Count; i++)
        {
            print("Total Ordered Menu Items " + orderMasterList[i].OrderID);
        }


        // Order List will be pass here with same orderId. No matter there is one item or
        //more than that.
        EventHandler.Instance.InvokeSendMenuListToCustomer(order , customerId);
     
    }


    private List<T> GetRandom<T>(List<T> temp)
    {
        var rnd = new System.Random();
        var randomized = temp.OrderBy(item => rnd.Next()).ToList();

        temp.Clear();
        foreach (var value in randomized)
        {
            print(value.ToString());

            temp.Add(value);
        }

        return temp;
    }


    private void TakeRecipeSOFromLevelManager(List<Recipe> obj , int maxRecipeItem)
    {
        RecipeSO = obj;
        this.maxRecipeItem = maxRecipeItem;
    }

    private void OnDisable()
    {
        EventHandler.Instance.GiveRecipeSOToMenuManager -= TakeRecipeSOFromLevelManager;
        EventHandler.Instance.GetMenuItemsFromMenuManger -= GiveMenuItemsToCustomer;

    }
}
