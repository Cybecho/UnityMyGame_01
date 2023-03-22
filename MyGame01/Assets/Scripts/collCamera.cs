using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collCamera : MonoBehaviour
{
    Collider coll;

    void Awake()
    {
        coll = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "pillar")
            Debug.Log("Hello World");
    }
}
