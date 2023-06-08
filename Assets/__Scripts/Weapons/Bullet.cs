using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected GameObject _hitEffect;
    [SerializeField] protected float _damage;

    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;

    private void Awake()
    {
        Destroy(gameObject, _lifeTime);
    }

    public void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }
}