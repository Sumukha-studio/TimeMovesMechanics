using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private float _fireRate = 1f;

    private Transform _playerTransform;
    private float _nextFireTime;

    private void Start()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            _playerTransform = player.transform;
        }

        _nextFireTime = Time.time + Random.Range(0.5f, 1.5f);
    }

    private void Update()
    {
        HandleMovement();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void HandleMovement()
    {
        Vector3 targetPosition = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);
        transform.LookAt(targetPosition);

        transform.position += transform.forward * _speed * Time.deltaTime;

        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + (1f / _fireRate);
            Shoot();
        }
    }
    private void Shoot()
    {
        if (_projectilePrefab != null && _projectileSpawnPoint != null)
        {
            Instantiate(_projectilePrefab, _projectileSpawnPoint.position, transform.rotation);
        }
    }
}