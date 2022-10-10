using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    private GameObject player;
    private Vector3 offset = new Vector3(0f, 3.75f, -3f);
    private float yBand = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (transform.position.y > yBand)
        {
            transform.position = player.transform.position + offset;
        }

    }

}
