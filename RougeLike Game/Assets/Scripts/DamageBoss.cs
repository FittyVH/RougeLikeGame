using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoss : MonoBehaviour
{
    int hp = 5;

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sword")
        {
            Debug.Log("ouch");
        }
    }
}
