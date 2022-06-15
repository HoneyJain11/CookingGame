using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuListManager : MonoBehaviour
{
    List<Recipe> RecipeSO;
    List<Recipe> orderList;
    List<Recipe> tempList;
    int maxRecipeItem;

    private void OnEnable()
    {
        EventHandler.Instance.GiveRecipeSOToMenuManager += TakeRecipeSOFromLevelManager;
        EventHandler.Instance.GetMenuItemsFromMenuManger += GiveMenuItemsToCustomer;
    }
    private void Start()
    {
        orderList = new List<Recipe>();
        orderList.Clear();

    }
    private void GiveMenuItemsToCustomer()
    {
        tempList=GetRandom(RecipeSO);
        for (int i =0;i< tempList.Count;i++)
        {
            print("Menu Items To Customer " + tempList[i].recipeId);
        }
        SendMenuItemsToCustomer();
    }

    private void SendMenuItemsToCustomer()
    {
        for(int i =0; i < tempList.Count;i++)
        {
            orderList.Add(tempList[i]);
        }
        for (int i = 0; i < orderList.Count; i++)
        {
            print("Total Ordered Menu Items " + orderList[i].recipeId);
        }
        EventHandler.Instance.InvokeSendMenuListToCustomer(tempList, Random.Range(1, maxRecipeItem));
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
