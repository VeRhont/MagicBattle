using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;

    [Header("PlayerStats")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _speedDuringShooting;
    [SerializeField] private float _speedDuringCharging;
    [SerializeField] private float _speedInSlime;
    private float _currentSpeed;
    private float _health;
    private int _score;
    private int _kills;

    public bool IsShooting { get; set; }
    public bool IsCharging { get; set; }

    private bool _isMoving = false;
    private Vector2 _movement;
    private Vector2 _mousePosition;

    [Header("Weapon")]
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Transform _weaponPosition;
    private Rigidbody2D _weaponRb;

    [Header("Components")]
    private Camera _camera;
    private Rigidbody2D _playerRb;
    private Animator _playerAnimator;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _camera = Camera.main;
        _playerRb = GetComponent<Rigidbody2D>();
        _weaponRb = _weapon.GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();

        _health = _maxHealth;
        _currentSpeed = _normalSpeed;

        LoadPlayerData();
    }

    private void GetInput()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _isMoving = (_movement != Vector2.zero);
        _playerAnimator.SetBool("IsMoving", _isMoving);

        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Update()
    {

        FlipPlayer();
        GetInput();
        RotateWeapon();

        if (IsShooting)
        {
            _currentSpeed = _speedDuringShooting;
        }
        else if (IsCharging)
        {
            _currentSpeed = _speedDuringCharging;
        }
        else
        {
            _currentSpeed = _normalSpeed;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _playerRb.MovePosition(_playerRb.position + _movement * _currentSpeed * Time.fixedDeltaTime);
    }

    private void FlipPlayer()
    {
        if (_mousePosition.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void RotateWeapon()
    {       
        var lookDirection = _mousePosition - _weaponRb.position;
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 25f;

        _weapon.transform.eulerAngles = new Vector3(0, 0, angle);
        _weapon.transform.position = _weaponPosition.position;
    }

    public void TakeDamage(float damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
        UpdateHealthBar();

        if (_health == 0)
        {
            Die();
        }
    }

    public void Heal(float hp)
    {
        _health = Mathf.Clamp(_health + hp, 0, _maxHealth);
        UpdateHealthBar();
    }

    private void Die()
    {
        _playerAnimator.SetBool("IsDead", true);
        GameManager.Instance.SendMessage("EndGame");
    }

    private void UpdateHealthBar()
    {
        UI_Manager.Instance.UpdatePlayerHealth(_health, _maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimePart"))
        {
            _currentSpeed = _speedInSlime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimePart"))
        {
            _currentSpeed = _normalSpeed;
        }
    }

    private void LoadPlayerData()
    {
        SaveSystem.Instance.LoadPlayerData();
    }
}