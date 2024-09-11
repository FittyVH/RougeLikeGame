using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossLook : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;

        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
