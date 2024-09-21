using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int hp = 3;

    void Start()
    {

    }

    void Update()
    {
        if (hp <= 0)
        {
            Debug.Log("maa chuda le");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "obstacle")
        {
            Debug.Log(hp);
            hp--;
        }
    }
}