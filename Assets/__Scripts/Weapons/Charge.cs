using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] private float _timeToExplode = 5f;
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;

    [SerializeField] private ParticleSystem _explosionParticles;

    private void Update()
    {
        if (_timeToExplode <= 0)
        {
            Explode();
        }

        _timeToExplode -= Time.deltaTime;
    }

    private void Explode()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(_damage);
            }
        }

        Instantiate(_explosionParticles, transform.position, _explosionParticles.transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }
}