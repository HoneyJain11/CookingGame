using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterController : MonoBehaviour
{
   
    [SerializeField]
    GameObject breadInToast;
    [SerializeField]
    GameObject breadPlate;
    [SerializeField]
    GameObject strawbarryLayer;
    [SerializeField]
    GameObject chocolateLayer;

   
    void Update()
    {
        MouseClick();
    }

    public void SetUpToaster()
    {
        breadInToast.SetActive(true);
    }

    public void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.zero);

         
            if (hit && hit.collider.GetComponent<ToasterController>())
            {
               
                SetUpBread();
            }
            if (hit && hit.collider.GetComponent<StrawbarryController>())
            {
              
                strawbarryLayer.SetActive(true);

            }
            if (hit && hit.collider.GetComponent<ChocolateController>())
            {
                
                chocolateLayer.SetActive(true);

            }

        }
    }

    public void SetUpBread()
    {
        breadInToast.SetActive(false);
        breadPlate.SetActive(true);

    }

}