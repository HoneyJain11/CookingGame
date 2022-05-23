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
  
//    BreadController breadController;

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
