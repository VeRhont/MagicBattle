using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [Header("Constants")]
    private const string LEFT_MOUSE_BUTTON = "Fire1";
    private const string RIGHT_MOUSE_BUTTON = "Fire2";

    [Header("Shooting")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletForce;
    [SerializeField] private float _timeBetweenShoot;
    [SerializeField] private AudioClip _shootingSound;
    private float _lastShotTime = 1f;

    [Header("Charge")]
    [SerializeField] private GameObject _powerfulChargePrefab;
    [SerializeField] private float _chargeSpeed = 5f;
    [SerializeField] private float _timeToCharge;
    private float _timeFromChargeStart = 0;
    private bool _isChargeAvailable = false;

    [Header("LaserBeam")]
    [SerializeField] private GameObject _laserBeamPrefab;
    [SerializeField] private int _beamDamage;
    [SerializeField] private float _laserBeamDuration;
    [SerializeField] private float _timeBetweenUpdates;
    private bool _isLaserBeamActive = false;
    private bool _readyToUpdate = true;
    private GameObject _laserBeam;
    private LaserBeam _line;

    private void Awake()
    {
        LoadWeaponData();
    }

    private void Update()
    {
        if (Input.GetButton(LEFT_MOUSE_BUTTON)) 
        {   
            if (_isLaserBeamActive)
            {
                if (_readyToUpdate) StartCoroutine(UpdateLaserBeamPosition());
            }
            else if (_lastShotTime <= 0)
            {
                PlayerController.Instance.IsShooting = true;
                Shoot();
            }        
        }
        else if (Input.GetButtonUp(LEFT_MOUSE_BUTTON))
        {
            if (_isLaserBeamActive)
            {
                _laserBeam.SetActive(false);
            }

            PlayerController.Instance.IsShooting = false;
        }
        else
        {
            if (_isChargeAvailable && Input.GetButton(RIGHT_MOUSE_BUTTON))
            {
                PlayerController.Instance.IsCharging = true;
                _timeFromChargeStart += Time.deltaTime;
            }
            else if (Input.GetButtonUp(RIGHT_MOUSE_BUTTON))
            {
                PlayerController.Instance.IsCharging = false;
                if (_timeFromChargeStart >= _timeToCharge)
                {
                    ShootPowerfulCharge();              
                }
                _timeFromChargeStart = 0;
            }
        }
        _lastShotTime -= Time.deltaTime;
    }

    private void Shoot()
    {
        AudioManager.Instance.PlaySound(_shootingSound);
        var bullet = Instantiate<GameObject>(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        _lastShotTime = _timeBetweenShoot;
    }

    private void ShootPowerfulCharge()
    {
        var charge = Instantiate(_powerfulChargePrefab, _firePoint.position, _firePoint.rotation);
        var velocity = charge.transform.up;

        charge.gameObject.GetComponent<Rigidbody2D>().AddForce(velocity * _chargeSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LaserPowerup"))
        {
            StartCoroutine(ActivateLaserBeam());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator ActivateLaserBeam()
    {
        _isLaserBeamActive = true;
        CreateLaserBeam();

        yield return new WaitForSeconds(_laserBeamDuration);

        _isLaserBeamActive = false;
        Destroy(_laserBeam);
    }

    private void CreateLaserBeam()
    {
        _laserBeam = Instantiate(_laserBeamPrefab, _firePoint.position, _firePoint.rotation);
        _line = _laserBeam.GetComponent<LaserBeam>();

        _laserBeam.SetActive(false);
    }

    private IEnumerator UpdateLaserBeamPosition()
    {
        _readyToUpdate = false;

        _laserBeam.SetActive(true);
        _line.SetPoints(_firePoint, _beamDamage);

        yield return new WaitForSeconds(_timeBetweenUpdates);

        _readyToUpdate = true;
    }

    private void LoadWeaponData()
    {
        var weaponData = SaveSystem.Instance.LoadWeaponData();
        _timeBetweenShoot = weaponData.FireRate;

        _beamDamage = (int)PlayerPrefs.GetFloat("laserUpgradeValue", 1);
        _timeToCharge = PlayerPrefs.GetFloat("powerfulChargeUpgradeValue", 3);
        _isChargeAvailable = (PlayerPrefs.GetInt("powerfulCharge", 0) == 1);
    }
}