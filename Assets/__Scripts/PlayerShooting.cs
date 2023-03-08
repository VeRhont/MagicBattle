using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [Header("Shooting")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletForce;
    [SerializeField] private float _timeBetweenShoot;
    private float _lastShotTime = 0f;

    [Header("Bomb")]
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private float _throwingForce = 5f;

    private void Awake()
    {
        _lastShotTime = _timeBetweenShoot;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (_lastShotTime <= 0)
            {
                Shoot();
                _lastShotTime = _timeBetweenShoot;
            }

            _lastShotTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowBomb();
        }
    }

    private void Shoot()
    {
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.panStereo = Random.Range(-0.2f, 0.2f);
        _audioSource.Play();

        var bullet = Instantiate<GameObject>(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        var bulletRb = bullet.GetComponent<Rigidbody2D>();

        bulletRb.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);
    }

    private void ThrowBomb()
    {
        var bomb = Instantiate(_bombPrefab, _firePoint.position, _firePoint.rotation);
        var velocity = bomb.transform.up;

        bomb.gameObject.GetComponent<Rigidbody2D>().AddForce(velocity * _throwingForce, ForceMode2D.Impulse);
    }
}