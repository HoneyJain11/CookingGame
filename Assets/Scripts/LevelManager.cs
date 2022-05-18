using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    ToasterController toasterController;

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

            
            if (hit && hit.collider.GetComponent<CircleCollider2D>())
            {
                toasterController.SetUpToaster();
            }

        }
    }
}
