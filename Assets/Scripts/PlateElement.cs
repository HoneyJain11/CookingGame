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
    

    private void Start()
    {
        EventHandler.Instance.SpwanReadyBread += SpwanBreadOnPlate;
        plateList = new List<GameObject>();
        SetPlates();
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
}
