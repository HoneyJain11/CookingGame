using System.Collections.Generic;

public class Order
{
   // List<int> list = new List<int>(new int[size]);

    int orderID;
    List<Recipe> recipeList;
    public List<Recipe> RecipeList { get { return recipeList; } }
    List<bool> isItemDelivered;
    bool isOrderDelivered = false;
    public int OrderID { get { return orderID; } }

    
    public bool IsOrderDelivered
    {
        get{return isOrderDelivered;}
        set{isOrderDelivered=IsOrderDelivered;}
    }

    public List<bool> IsItemDelivered { get => isItemDelivered; set => isItemDelivered = value; }

    public Order()
    {

    }
   public Order(int orderID,List<Recipe> recipeList, List<bool> isItemDelivered)
    {
        this.orderID = orderID;
        this.recipeList = recipeList;
        this.isItemDelivered = isItemDelivered;

    }
}
