using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterElement : MonoBehaviour
{
    public GameObject BreadPrefab;
    public GameObject toastMachinePrefab;
    [SerializeField]
    List<GameObject> leftMachineSlots;
    List<GameObject> toasterList;
    [SerializeField]
    Sprite toastedBread;


    private void Start()
    {
        toasterList = new List<GameObject>();
        EventHandler.Instance.OnFindEmptyToaster += SetBreadOnToaster;
        EventHandler.Instance.OnToasterClick += StopToaster;
        EventHandler.Instance.OnToasterWorkCompleted += ChangeBreadSprite;
        SetToasterObjects(leftMachineSlots, toastMachinePrefab);
    }

    private void ChangeBreadSprite()
    {
        print("Into ChangeBreadSprite");
        for (int i = 0; i < toasterList.Count; i++)
        {
            if (toasterList[i].gameObject.GetComponent<Machine>().MachineMode == MachineMode.WorkCompleted && toasterList[i].transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite != toastedBread)
            {
                print("Into ChangeBreadSprite Loop");
                toasterList[i].transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = toastedBread;
                Debug.Log(" " + toasterList[i].gameObject.GetComponent<Machine>().MachineMode);
                break;
            }
        }
    }

    private void StopToaster(int machineId)
    {
        print("Into Stop Toaster");
        for (int i = 0; i < toasterList.Count; i++)
        {
            if (toasterList[i].gameObject.GetComponent<Machine>().MachineMode == MachineMode.WorkCompleted && toasterList[i].gameObject.GetComponent<Machine>().machineId == machineId)
            {
                print("Into Stop Toaster Loop");
                toasterList[i].gameObject.GetComponent<Machine>().OnMachineTap();
                Debug.Log(" " + toasterList[i].gameObject.GetComponent<Machine>().MachineMode);
                break;

            }
        }
    }

    public void SetBreadOnToaster()
    {
        print("In Method Set Up Bread On Toaster");
        for (int i = 0; i < toasterList.Count; i++)
        {
            if (toasterList[i].gameObject.GetComponent<Machine>().MachineMode == MachineMode.Idle)
            {
                GameObject toastBread = Instantiate(BreadPrefab);
                toastBread.transform.parent = toasterList[i].gameObject.transform;
                toastBread.transform.localPosition = new Vector3 (-0.1f,0.57f,0f);
                toasterList[i].gameObject.GetComponent<Machine>().OnWorkingMode();
                Debug.Log(" " + toasterList[i].gameObject.GetComponent<Machine>().MachineMode);
                break;
            }
        }
    }

    private void SetToasterObjects(List<GameObject> SpawnSlots, GameObject initiatePrefab)
    {
        for (int i = 0; i < SpawnSlots.Count; i++)
        {
            GameObject childGameObject = Instantiate(initiatePrefab);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            childGameObject.transform.parent = SpawnSlots[i].transform;
            childGameObject.transform.localPosition = temp;
            childGameObject.GetComponent<Machine>().machineId = i + 1;
            toasterList.Add(childGameObject);
        }
    }

    private void OnDisable()
    {
        EventHandler.Instance.OnFindEmptyToaster -= SetBreadOnToaster;
        EventHandler.Instance.OnToasterClick -= StopToaster;
        EventHandler.Instance.OnToasterWorkCompleted -= ChangeBreadSprite;
    }

}