using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
