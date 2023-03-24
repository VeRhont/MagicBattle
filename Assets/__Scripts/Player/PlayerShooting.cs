using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //[SerializeField] private AudioSource _audioSource;

    [Header("Shooting")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletForce;
    [SerializeField] private float _timeBetweenShoot;
    private float _lastShotTime = 1f;

    [Header("Bomb")]
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private float _throwingForce = 5f;

    private void Update()
    {
        _lastShotTime -= Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            if (_lastShotTime <= 0)
            {
                Shoot();
            }           
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowBomb();
        }
    }

    private void Shoot()
    {
        //_audioSource.pitch = Random.Range(0.8f, 1.2f);
        //_audioSource.panStereo = Random.Range(-0.2f, 0.2f);
        //_audioSource.Play();

        var bullet = Instantiate<GameObject>(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        _lastShotTime = _timeBetweenShoot;
    }

    private void ThrowBomb()
    {
        var bomb = Instantiate(_bombPrefab, _firePoint.position, _firePoint.rotation);
        var velocity = bomb.transform.up;

        bomb.gameObject.GetComponent<Rigidbody2D>().AddForce(velocity * _throwingForce, ForceMode2D.Impulse);
    }
}