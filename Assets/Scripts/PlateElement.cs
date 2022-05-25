using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateElement : MonoBehaviour
{
    [SerializeField]
    GameObject readyBread;
    List<GameObject> plateList;
    [SerializeField]
    List<GameObject> platesSlots;
    [SerializeField]
    GameObject platePrefab;
    [SerializeField]
    int platesUnlocked;
    [SerializeField]
    Recipe recipe1;
    [SerializeField]
    Recipe recipe2;
    [SerializeField]
    GameObject readyStrawberryPrefab;
    [SerializeField]
    GameObject readyChocolatePrefab;
    [SerializeField]
    GameObject readyPeanutPrefab;
    [SerializeField]
    GameObject readyEggPrefab;


    private void Start()
    {
        EventHandler.Instance.SpwanReadyBread += SpwanBreadOnPlate;
        EventHandler.Instance.OnStrawberryClick += SpwanStarwberryOnBread;
        EventHandler.Instance.OnChocolateClick += SpwanChocolateOnBread;
        EventHandler.Instance.OnPenautClick += SpwanPeanutOnBread;
        EventHandler.Instance.OnEggClick += SpwanEggOnBread;

        plateList = new List<GameObject>();
        SetPlates();
    }

    private void SpwanEggOnBread()
    {
        Debug.Log("Egg spwaned");
        for (int i = 0; i < plateList.Count; i++)
        {
            if (plateList[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite == recipe2.childImages[0] && plateList[i].gameObject.transform.GetChild(0).gameObject.transform.childCount <= 1)
            {
                GameObject egg = Instantiate(readyEggPrefab);
                egg.transform.parent = plateList[i].gameObject.transform.GetChild(0).gameObject.transform;
                egg.transform.localPosition = new Vector3(0f, 0f, 0f);
                break;
            }

        }
    }

    private void SpwanPeanutOnBread()
    {
        Debug.Log("Peanut spwaned");
        for (int i = 0; i < plateList.Count; i++)
        {
            if (plateList[i].gameObject.transform.GetChild(0).gameObject.transform.childCount <= 0)
            {
                GameObject chocolate = Instantiate(readyPeanutPrefab);
                chocolate.transform.parent = plateList[i].gameObject.transform.GetChild(0).gameObject.transform;
                chocolate.transform.localPosition = new Vector3(0f, 0f, 0f);
                break;
            }
        }
    }

    private void SpwanChocolateOnBread()
    {
        Debug.Log("Chocolate spwaned");
        for (int i = 0; i < plateList.Count; i++)
        {
          /*print("Rikhil Plate Count " + plateList.Count);

            if (plateList[i].gameObject.transform.childCount > 0)
            {
                print("Rikhil  Plate Child is " + plateList[i].gameObject.transform.GetChild(0).name);
                GameObject go = plateList[i].gameObject.transform.GetChild(0).gameObject;
                if (go.gameObject.transform.childCount <= 0)
                {

                    GameObject chocolate = Instantiate(readyChocolatePrefab);
                    chocolate.transform.parent = plateList[i].gameObject.transform.GetChild(0).gameObject.transform;
                    chocolate.transform.localPosition = new Vector3(0f, 0f, 0f);
                    print("Rikhil  Plate Child's Child is " + go.gameObject.transform.GetChild(0).name);
                    break;
                }
                
            }*/

            if (plateList[i].gameObject.transform.GetChild(0).gameObject.transform.childCount <= 0)
            {
                GameObject chocolate = Instantiate(readyChocolatePrefab);
                chocolate.transform.parent = plateList[i].gameObject.transform.GetChild(0).gameObject.transform;
                chocolate.transform.localPosition = new Vector3(0f, 0f, 0f);
                break;
            }

        }
    }

    private void SpwanStarwberryOnBread()
    {
        Debug.Log("starwberry spwaned");
        for (int i = 0; i < plateList.Count; i++)
        {
            if (plateList[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite == recipe1.childImages[0] && plateList[i].gameObject.transform.GetChild(0).gameObject.transform.childCount <=1)
            {
                GameObject strawberry = Instantiate(readyStrawberryPrefab);
                strawberry.transform.parent = plateList[i].gameObject.transform.GetChild(0).gameObject.transform;
                strawberry.transform.localPosition = new Vector3(0f, 0f, 0f);
                break;
            }

        }

    }


    private void SetPlates()
    {
        for (int i = 0; i < platesSlots.Count; i++)
        {
            GameObject plates = Instantiate(platePrefab);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            plates.transform.parent = platesSlots[i].transform;
            plates.transform.localPosition = temp;
            plates.GetComponent<Plates>().plateId = i + 1;
            if (platesUnlocked > 0)
            {
                plates.GetComponent<Plates>().plateState = PlateState.Unlocked;
                platesUnlocked--;

            }
            plateList.Add(plates);
            Debug.Log("platestate - " + plates.GetComponent<Plates>().plateState);

        }
    }


    private void SpwanBreadOnPlate()
    {
        for (int i =0; i < plateList.Count; i++)
        {
            if(plateList[i].gameObject.GetComponent<Plates>().plateState == PlateState.Unlocked && plateList[i].gameObject.GetComponent<Plates>().plateStateBread == PlateStateBread.Free)
            {
                GameObject bread = Instantiate(readyBread);
                bread.transform.parent = plateList[i].gameObject.transform;
                bread.transform.localPosition = new Vector3(0f, 0f, 0f);
                plateList[i].gameObject.GetComponent<Plates>().plateStateBread = PlateStateBread.Occupied;
                break;
            }
        }
       
    }

    private void OnDisable()
    {
        EventHandler.Instance.SpwanReadyBread -= SpwanBreadOnPlate;
        EventHandler.Instance.OnStrawberryClick -= SpwanStarwberryOnBread;
        EventHandler.Instance.OnChocolateClick -= SpwanChocolateOnBread;
        EventHandler.Instance.OnPenautClick -= SpwanPeanutOnBread;
        EventHandler.Instance.OnEggClick -= SpwanEggOnBread;

    }
}
