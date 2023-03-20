using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;

    [Header("PlayerStats")]
    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private Transform _weapon;

    [SerializeField] private float _movementSpeed;

    private Camera _camera;
    private Rigidbody2D _playerRb;
    private Rigidbody2D _weaponRb;
    private Animator _playerAnimator;

    private bool _isMoving = false;
    private Vector2 _movement;
    private Vector2 _mousePosition;

    [Header("Health")]
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private float _maxHealth;
    private float _health;

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

        if (_movement != Vector2.zero)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }

        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _playerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            _playerAnimator.SetBool("IsMoving", false);
        }

        _playerRb.MovePosition(_playerRb.position + _movement * _movementSpeed * Time.fixedDeltaTime);
        _weaponRb.MovePosition(_playerRb.position + _movement * _movementSpeed * Time.fixedDeltaTime);

        var lookDirection = _mousePosition - _weaponRb.position;
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 25f;

        _weaponRb.rotation = angle;
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
            _movementSpeed = 0.2f;
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