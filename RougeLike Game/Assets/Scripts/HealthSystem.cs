using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int hp = 3;

    public GameObject[] heartUi;

    void Update()
    {
        if (hp < 0)
        {
            Time.timeScale = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "obstacle" && hp >=0)
        {
            heartUi[hp].SetActive(false);
            hp--;
        }
    }
}