using System.Collections;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public GameObject player;
    public EnableRandomAttack enableRandomAttack;

    public BulletAttack bulletAttack;

    GameObject spawnedBullet;
    Vector3 moveDirection;

    public float moveSpeed;
    public float spawnInterval = 5f;
    public float launchDelay = 2f;  // Fixed typo "lauchDelay" to "launchDelay"
    public int maxIterations = 5;

    private bool isSpawning = false;
    bool bulletSpawned;

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
        // Moved bullet movement logic to a separate method
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
        moveDirection = (player.transform.position - spawnPoint.position).normalized;
        bulletSpawned = true;
        maxIterations--;
        StartCoroutine(DelayedTranslate(spawnedBullet, launchDelay));  // Only call once per bullet
    }

    IEnumerator DelayedTranslate(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait before moving the bullet
        // Set the bullet to move in the next Update cycle
        bulletSpawned = true;
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
            spawnedBullet.transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    void EndAttack()
    {
        // End the attack, mark attack as over for EnableRandomAttack
        if (spawnedBullet != null)
        {
            Destroy(spawnedBullet);  // Clean up the bullet
        }
        enableRandomAttack.attackOver = true;
        bulletAttack.enabled = false;
    }
}