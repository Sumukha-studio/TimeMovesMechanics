using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }


    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            enemy.Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}