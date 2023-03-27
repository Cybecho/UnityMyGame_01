using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collPillar : MonoBehaviour
{
    Collider coll;

    void Awake()
    {
        coll = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            gameObject.SetActive(true);
        }
    }
}
