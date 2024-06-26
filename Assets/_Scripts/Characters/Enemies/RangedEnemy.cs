using UnityEngine;

public class RangedEnemy : Character
{
    public float approachSpeed = 3f;
    public float detectionRadius = 10f; // Radius within which the enemy detects the player
    public float stopDistance = 2f; // Distance at which the enemy stops approaching the player
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float shootCooldown = 2f; // Time between shots
    public float bulletSpawnOffset = 1f; // Distance in front of the enemy to spawn bullet
    [SerializeField] float distanceToPlayer;
    [SerializeField] Vector2 direction;


    private Transform playerTransform;
    private float lastShotTime;
    private Player player;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = playerTransform.GetComponent<Player>();
        lastShotTime = -shootCooldown; // Allow shooting immediately on start
    }

    void Update()
    {
        ApproachAndShootPlayer();
    }

    private void ApproachAndShootPlayer()
    {
        if (playerTransform == null || player.GetInvisibilityStatus())
        {
            return; // Do not approach or target the player if they are invisible
        }

            distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < detectionRadius)
        {
            direction = (playerTransform.position - transform.position).normalized;

            // Move towards the player if outside the stop distance
            if (distanceToPlayer > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, approachSpeed * Time.deltaTime);
            }

            // Update movementDirection for flipping
            if (direction.x > 0)
            {
                movementDirection = 1;
            }
            else if (direction.x < 0)
            {
                movementDirection = -1;
            }
            Flip();

            // Shoot at the player if within detection radius
            ShootAtPlayer(direction);
        }
    }
    private void ShootAtPlayer(Vector2 direction)
    {
        if (Time.time - lastShotTime < shootCooldown)
        {
            return;
        }

        lastShotTime = Time.time;

        // Calculate bullet spawn position with offset
        Vector3 bulletSpawnPosition = transform.position + (Vector3.right * movementDirection * bulletSpawnOffset);

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);

        // Set bullet direction
        Vector2 bulletDirection = (playerTransform.position - bulletSpawnPosition).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * 10f; // Set bullet speed
    }
}
