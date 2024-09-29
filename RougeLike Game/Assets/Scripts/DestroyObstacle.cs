using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boundry")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "ground")
        {
            Destroy(gameObject);
        }
    }
}
