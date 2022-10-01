using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClick1 : MonoBehaviour
{
    private double doubleClickTime = 0.25f;
    public bool movement;
    public GameObject playerSpwanPreafb;
    GameObject player;
    Vector3 givePosition;
    [SerializeField]
    int speed;
    [SerializeField]
    Camera mainCamera;
    public float pcRotSpeed;

    private void Start()
    {
        StartCoroutine(ClickListener());
    }

    private IEnumerator ClickListener()
    {
        while (enabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                yield return MouseClick();
            }
            yield return null;
        }
    }

    private IEnumerator MouseClick()
    {
        yield return new WaitForEndOfFrame();

        float timeCount = 0;
        while (timeCount < doubleClickTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DoubleClick();
                yield break;
            }
            timeCount += Time.deltaTime;
            yield return null;
        }
        SingleClick();
    }

    private void SingleClick()
    {

    }

    private void DoubleClick()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Debug.Log($"  - {hit.point}");
            player = Instantiate(playerSpwanPreafb);
            player.transform.position = hit.point;
            givePosition = new Vector3(player.transform.position.x, -8.5f, player.transform.position.z);
            movement = true;

        }

    }

    private void Update()
    {
        if (movement == true)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, givePosition, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(mainCamera.transform.position, givePosition) < 2.5f)
            {
                // Swap the position of the cylinder.
                givePosition *= -1.0f;
                movement = false;
                Destroy(player.gameObject);
            }

        }

        if (Input.GetMouseButton(0))
        {

            mainCamera.transform.eulerAngles += pcRotSpeed * new Vector3(Mathf.Clamp(-Input.GetAxis("Mouse Y"), -45f, 45f), Input.GetAxis("Mouse X"), 0f);

        }
    }
}

