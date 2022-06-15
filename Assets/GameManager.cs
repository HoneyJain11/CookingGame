using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer backgroundImage;

    // Start is called before the first frame update
    void Start()
    {
        SetCameraOrthographicSize();
    }


    private void SetCameraOrthographicSize()
    {
        // Setting camera orthographic size accroding to the background image of
        //Game.If you want to calculate width and height use the below commented
        //code and comment below line
       // Camera.main.orthographicSize = backgroundImage.bounds.size.y / 2;

        // this code will calculate camera orthograhpic size using width and height ratio
        // uncomment this block of code

        float screenRatio = (float)Screen.width / (float) Screen.height;
        float targetRatio = backgroundImage.bounds.size.x / backgroundImage.bounds.size.y;
        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = backgroundImage.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = (backgroundImage.bounds.size.y / 2)*differenceInSize;
        }
    }
}
