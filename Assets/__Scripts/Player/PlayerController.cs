using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;

    [Header("PlayerStats")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _speedDuringShooting;
    private float _health;

    [Header("Weapon")]
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Transform _weaponPosition;
    private Rigidbody2D _weaponRb;



    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private Camera _camera;
    private Rigidbody2D _playerRb;

    private Animator _playerAnimator;

    private bool _isMoving = false;
    private Vector2 _movement;
    private Vector2 _mousePosition;

    [Header("Health")]
    [SerializeField] private Image _healthBarImage;


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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Teleport();
        }

        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _isMoving = (_movement != Vector2.zero);

        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        RotateWeapon();
    }

    private void FixedUpdate()
    {
        _playerAnimator.SetBool("IsMoving", _isMoving);

        _playerRb.MovePosition(_playerRb.position + _movement * _movementSpeed * Time.fixedDeltaTime);
    }

    private void RotateWeapon()
    {       
        var lookDirection = _mousePosition - _weaponRb.position;
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 25f;

        _weapon.transform.eulerAngles = new Vector3(0, 0, angle);
        _weapon.transform.position = _weaponPosition.position;
    }

    private void Teleport()
    {
        transform.position = Vector3.zero;
    }

    private void UpdateHealth()
    {
        _healthBarImage.fillAmount = _health / _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);

        UpdateHealth();

        if (_health == 0)
        {
            Die();
        }
    }

    public void Heal(float hp)
    {
        _health = Mathf.Clamp(_health + hp, 0, _maxHealth);

        UpdateHealth();
    }

    private void Die()
    {
        _playerAnimator.SetBool("IsDead", true);
        Debug.Log("Player is dead");
    }

    public void UpdateScore(int newScore)
    {
        _score += newScore;

        _scoreText.SetText($"Score: {_score}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimePart"))
        {
            _movementSpeed = 0.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimePart"))
        {
            _movementSpeed = 3f;
        }
    }
}