using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoss : MonoBehaviour
{
    int hp = 5;

    void Update()
    {
        if (hp < 0)
        {
            Debug.Log("maa chod di");
            Time.timeScale = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sword")
        {
            Debug.Log("ouch");
            hp--;
        }
    }
}
