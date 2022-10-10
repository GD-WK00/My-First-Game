using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionalItem : MonoBehaviour
{
    public GameObject bridge;

    private Color myColor = new Color(0, 255, 0);

    void OnTriggerEnter(Collider other)
    {
        //bridge trigger
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Renderer>().material.color = myColor;
            bridge.transform.position = new Vector3(bridge.transform.position.x, 1.5f, bridge.transform.position.z);
        }
    }
}
