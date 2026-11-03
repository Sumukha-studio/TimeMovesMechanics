using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    private Transform _playerTransform;

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
        if (_playerTransform == null) return;

        Vector3 targetPosition = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);
        transform.LookAt(targetPosition);

        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}