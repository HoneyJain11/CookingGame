using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadController : MonoBehaviour
{
    [SerializeField]
    Transform player;
    float speed = 10;
    Vector2 originalBreadPosition;
    
  

   
   private void OnEnable()
    {
        originalBreadPosition = this.gameObject.transform.position;
       

    }
    private void OnDisable()
    {
        
        SetUpBread();
        
    }
    void Update()
    {
        MouseClick();
    }

    public void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.zero);
            if (hit && hit.collider.GetComponent<BreadController>())
            {

                MoveBreadToPlayer();

            }

        }
    }
   private async void MoveBreadToPlayer()
    {
        if(this.transform.position != player.position)
        {
            var step = speed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.position, step);
            await new WaitForSeconds(0.1f);
            MoveBreadToPlayer();
        }
        else
        {
            this.gameObject.transform.position = originalBreadPosition;
            this.gameObject.SetActive(false);

        }
       
      
    }
    private void SetUpBread()
    { 
        int childcount = this.gameObject.transform.childCount;
        for (int i= 0; i < childcount;i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }


    }

}