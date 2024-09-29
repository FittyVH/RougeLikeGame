using System.Collections;
using UnityEngine;

public class WallAttack : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public GameObject player;
    public EnableRandomAttack enableRandomAttack;

    public WallAttack wallAttack;

    GameObject spawnedBullet;
    Vector3 moveDirection;

    public float moveSpeed;
    public float spawnInterval = 5f;
    public float launchDelay = 2f;
    public int maxIterations = 5;

    private bool bulletSpawned = false;
    private bool isSpawning = false;

    void OnEnable()
    {
        // Reset state when re-enabled
        maxIterations = 5;
        bulletSpawned = false;
        isSpawning = true;
        StartCoroutine(SpawnInterval());
    }

    void Update()
    {
        // Move the bullet if it has been spawned
        if (spawnedBullet != null && bulletSpawned)
        {
            MoveBullet();
        }

        if (maxIterations <= 0 && !isSpawning)
        {
            Invoke("EndAttack", 2f);  // End the attack after all bullets are spawned
        }
    }

    void SpawnBullet()
    {
        // Spawn the bullet and calculate the direction to the player
        spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        moveDirection = new Vector3(
            spawnPoint.transform.position.x - player.transform.position.x, 
            0f, 
            spawnPoint.transform.position.z - player.transform.position.z
        ).normalized;

        maxIterations--;
        StartCoroutine(DelayedTranslate(spawnedBullet, launchDelay));  // Only call once per bullet
    }

    IEnumerator DelayedTranslate(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait before moving the bullet
        bulletSpawned = true;  // Set the bullet to move after the delay
    }

    IEnumerator SpawnInterval()
    {
        while (maxIterations > 0)
        {
            SpawnBullet();
            yield return new WaitForSeconds(spawnInterval);  // Wait before spawning the next bullet
        }
        isSpawning = false;  // Mark that spawning is complete
    }

    void MoveBullet()
    {
        // Move the spawned bullet in the calculated direction
        if (spawnedBullet != null)
        {
            spawnedBullet.transform.position += -moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    void EndAttack()
    {
        // End the attack and clean up the spawned bullet
        if (spawnedBullet != null)
        {
            Destroy(spawnedBullet);
        }
        enableRandomAttack.attackOver = true;
        wallAttack.enabled = false;
    }
}