using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletForce;
    [SerializeField] private float _timeBetweenShoot;
    private float _lastShotTime = 1f;

    [Header("Charge")]
    [SerializeField] private GameObject _powerfulChargePrefab;
    [SerializeField] private float _chargeSpeed = 5f;
    [SerializeField] private float _timeToCharge;
    private float _timeFromChargeStart;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {          
            if (_lastShotTime <= 0)
            {
                PlayerController.Instance.IsShooting = true;
                Shoot();
            }        
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            PlayerController.Instance.IsShooting = false;
        }

        if (Input.GetButton("Fire2"))
        {
            PlayerController.Instance.IsCharging = true;
            _timeFromChargeStart += Time.deltaTime;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            PlayerController.Instance.IsCharging = false;
            if (_timeFromChargeStart >= _timeToCharge)
            {
                ShootPowerfulCharge();              
            }
            _timeFromChargeStart = 0;
        }

        _lastShotTime -= Time.deltaTime;
    }

    private void Shoot()
    {
        var bullet = Instantiate<GameObject>(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        _lastShotTime = _timeBetweenShoot;
    }

    private void ShootPowerfulCharge()
    {
        var charge = Instantiate(_powerfulChargePrefab, _firePoint.position, _firePoint.rotation);
        var velocity = charge.transform.up;

        charge.gameObject.GetComponent<Rigidbody2D>().AddForce(velocity * _chargeSpeed, ForceMode2D.Impulse);
    }
}