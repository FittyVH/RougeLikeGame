using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public GameObject player;

    GameObject spawnedBullet;
    Vector3 moveDirection;

    public float moveSpeed;
    public float spawnInterval = 5f;
    public float lauchDelay = 2f;
    public int maxIterations = 5;

    bool bulletSpawned;

    void Start()
    {
        StartCoroutine(SpawnInterval());
    }

    void Update()
    {
        if (bulletSpawned)
        {
            StartCoroutine(DelayedTranslate(spawnedBullet, lauchDelay));
        }
    }

    void SpawnBullet()
    {
        spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        bulletSpawned = true;
        moveDirection = spawnPoint.transform.position - player.transform.position;
        maxIterations--;
    }
    IEnumerator DelayedTranslate(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        obj.transform.position += -moveDirection.normalized * moveSpeed * Time.deltaTime;
    }
    IEnumerator SpawnInterval()
    {
        while (true)
        {
            if (maxIterations > 0)
            {
                SpawnBullet();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
