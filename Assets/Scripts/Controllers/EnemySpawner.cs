using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private float _spawnDistance = 15f;

    private Transform _playerTransform;
    private float _timer;

    private void Start()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            _playerTransform = player.transform;
        }
    }

    private void Update()
    {
        if (_playerTransform == null || _enemyPrefab == null) return;

        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            SpawnEnemy();
            _timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized * _spawnDistance;
        Vector3 spawnPosition = new Vector3(
            _playerTransform.position.x + randomCircle.x,
            _playerTransform.position.y,
            _playerTransform.position.z + randomCircle.y
        );

        Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
    }
}