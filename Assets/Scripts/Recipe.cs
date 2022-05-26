using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RecipeScriptableObject", menuName = "ScriptableObjects/RecipeScriptableObject")]
public class Recipe : ScriptableObject
{
    
   public  Sprite parentImage;
   
   public Sprite [] childImages;
    public int recipeId;

}
