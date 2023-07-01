using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(_hitSound);
            var player = collision.gameObject.GetComponent<PlayerController>();

            var damage = Random.Range(_damage - 3, _damage + 3);
            player.TakeDamage(damage);
        }
        Die();
    }
}
