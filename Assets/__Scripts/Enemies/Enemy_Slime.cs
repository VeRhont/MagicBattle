using UnityEngine;

public class Enemy_Slime : Enemy
{
    [SerializeField] private float _speed;

    [Header("Slime_Death")]
    [SerializeField] private int _slimesCount;
    [SerializeField] private float _radius;
    [SerializeField] private GameObject[] _slimeParts;
    [SerializeField] private ParticleSystem _deathParticles;

    [Header("Slime_Attack")]
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _timeBetweenAttack;
    [SerializeField] private GameObject _projectilePrefab;

    private float _timeFromLastAttack;

    private bool isCloseEnough => (distanceToPlayer <= _distanceToAttack);

    private void Update()
    {
        _timeFromLastAttack -= Time.deltaTime;

        if (_timeFromLastAttack <= 0 && isCloseEnough)
        {
            Attack();
            _timeFromLastAttack = _timeBetweenAttack;
        }   
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    private void Attack()
    {
        var lookDirection = GetDirectionVectorToPlayer();

        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        var rotation = Quaternion.Euler(0, 0, angle);

        var bullet = Instantiate(_projectilePrefab, transform.position, rotation);
    }   

    private void MoveToPlayer()
    {
        _enemyAnimator.SetBool("IsMoving", true);
        
        var direction = GetDirectionVectorToPlayer();
        _enemyRb.MovePosition(_enemyRb.position + direction *_speed * Time.deltaTime);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {      
            Die();
        }
    }

    protected override void Die()
    {
        Instantiate(_deathParticles, transform.position, Quaternion.identity);
        SpawnSlimes();
        base.Die();
    }

    private void SpawnSlimes()
    {
        var angleDelta = 360f / _slimesCount;
        var angle = 0f;

        for (int i = 0; i < _slimesCount; i++)
        {
            var spawnPosition = (Vector2) transform.position + Random.insideUnitCircle * Random.Range(1.5f, _radius);
            var rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

            Instantiate(_slimeParts[Random.Range(0, _slimeParts.Length)], spawnPosition, rotation);

            angle += angleDelta;
        }
    }
}
