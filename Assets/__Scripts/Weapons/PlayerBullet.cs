using UnityEngine;

public class PlayerBullet : Bullet
{
    private void Start()
    {
        _damage = PlayerPrefs.GetFloat("damageUpgradeValue", 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject;

            var damage = Random.Range(_damage - 3, _damage + 3);
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }

        var effect = Instantiate(_hitEffect, transform.position, _hitEffect.transform.rotation);

        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}