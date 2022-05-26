using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
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
    [SerializeField]
    Recipe [] RecipeSO;

    private void Start()
    {
        SetRecipeOnWishList();
      
    }

    private void SetRecipeOnWishList()
    {
        for (int i = 0; i < recipeSpwanPoints.Length; i++)
        {
            GameObject khalikhoka = Instantiate(emptyGameObject);
            khalikhoka.AddComponent<SpriteRenderer>().sprite = RecipeSO[i].parentImage;
            khalikhoka.GetComponent<SpriteRenderer>().sortingOrder = 2;
            khalikhoka.transform.parent = recipeSpwanPoints[i].gameObject.transform;
            khalikhoka.transform.localPosition = new Vector3(0f, 0f, 0f);

            for (int j = 0; j < RecipeSO[i].childImages.Length; j++)
            {
                GameObject childObject = Instantiate(emptyGameObject);
                childObject.AddComponent<SpriteRenderer>().sprite = RecipeSO[i].childImages[j];
                childObject.GetComponent<SpriteRenderer>().sortingOrder = khalikhoka.GetComponent<SpriteRenderer>().sortingOrder + 1 + j;
                childObject.transform.parent = khalikhoka.gameObject.transform;
                childObject.transform.localPosition = new Vector3(0f, 0f, 0f);

            }

        }
    }

    void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {

        this.transform.position = new Vector2((this.transform.position.x + move.x * speed * Time.deltaTime),

                                              this.transform.position.y);


        if (this.transform.position.x < 0)
        {
            speed = 0;
            wishList.SetActive(true);

        }
    }

    
}
