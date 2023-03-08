using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime = 5f;

    private void Awake()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject;

            var damage = Random.Range(_damage - 3, _damage + 3);
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();

            var damage = Random.Range(_damage - 3, _damage + 3);
            player.TakeDamage(damage);
        }

        var effect = Instantiate(_hitEffect, transform.position, _hitEffect.transform.rotation);

        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}