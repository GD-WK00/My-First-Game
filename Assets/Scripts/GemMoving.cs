using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMoving : MonoBehaviour
{

    private float downLimit;
    private float upLimit;
    private float limitDifference = 0.8f;
    private float moveSpeed = 0.5f;
    private float rotateSpeed = 10f;
    //boolen is setting the direction of the gem (true - up limit, false - down limit)
    private bool bandDetector = true;


    void Start()
    {
        downLimit = transform.position.y;
        upLimit = transform.position.y + limitDifference;
    }


    void Update()
    {
        GemIsMoving();
        RotateTheGem();
    }

    //Method which contain the code to moving gem up and down
    private void GemIsMoving()
    {
        //Setting bool depending on the gem y position at the moment
        if(transform.position.y < downLimit)
        {
            bandDetector = true;
        }
        if (transform.position.y > upLimit)
        {
            bandDetector = false;
        }

        //Transforming the position of the gem
        if(bandDetector)
        {
            transform.position += Vector3.up * Time.deltaTime * moveSpeed;
        }
        if(!bandDetector)
        {
            transform.position += Vector3.down * Time.deltaTime * moveSpeed;
        }
    }

    //Method which contain the code to rotate the gem around its y axis
    private void RotateTheGem()
    {
        transform.Rotate(0f, Time.deltaTime * rotateSpeed, 0f);
    }
}
