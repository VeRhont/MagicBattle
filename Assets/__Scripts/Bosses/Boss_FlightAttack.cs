using UnityEngine;

public class Boss_FlightAttack : StateMachineBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidBody;
    private Collider2D[] _colliders;
    private Transform _player;
    private Vector2 _playerPosition;

    private float _timer = 20;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rigidBody = animator.GetComponentInParent<Rigidbody2D>();
        _colliders = _rigidBody.GetComponentsInParent<Collider2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }

        _timer = 20;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(_timer);

        if (_timer > 10f)
        {
            _rigidBody.position = new Vector2(_rigidBody.position.x, _rigidBody.position.y + _speed * Time.fixedDeltaTime);
            _playerPosition = _player.position;
        }
        else if (_timer > 0)
        {
            _rigidBody.position = new Vector2(_playerPosition.x, _rigidBody.position.y - _speed * Time.fixedDeltaTime);
        }

        _timer -= Time.fixedDeltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
    }
}
