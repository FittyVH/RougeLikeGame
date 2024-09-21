using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class SpwanObstacle : MonoBehaviour
{
    //references
    public GameObject player;
    public Transform spawnPoint;
    public GameObject[] allObstacles;

    //variables
    GameObject spawnedObstacle;
    Vector3 moveDirection;
    
    //float variables
    public float lauchDelay = 2f;
    public float moveSpeed = 100f;
    public float spawnInterval = 5f;

    //bools
    bool obstacleSpawned = false;

    void Start()
    {
        StartCoroutine(SpawnInterval());
    }

    void Update()
    {
        if (obstacleSpawned)
        {
            StartCoroutine(DelayedTranslate(spawnedObstacle, lauchDelay));
        }
    }

    void SpwanObject()
    {
        int randomInt = Random.Range(0, allObstacles.Length);

        spawnedObstacle = Instantiate(allObstacles[randomInt], spawnPoint.position, spawnPoint.rotation);
        obstacleSpawned = true;
        moveDirection = new Vector3(transform.position.x - player.transform.position.x, 0f, transform.position.z - player.transform.position.z);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boundry")
        {
            Destroy(spawnedObstacle);
        }
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
            SpwanObject();

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
