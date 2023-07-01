using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] protected AudioClip _hitSound;
    [SerializeField] protected float _damage;

    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;

    private void Update()
    {
        if (_lifeTime <= 0) Die();

        transform.Translate(Vector2.up * _speed * Time.deltaTime);
        _lifeTime -= Time.deltaTime;
    }

    protected void Die()
    {
        var effect = Instantiate(_hitEffect, transform.position, _hitEffect.transform.rotation);

        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}