using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    [SerializeField] private float _speed = 2f; 

    private Transform _player;
    private Rigidbody2D _rigidBody;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidBody = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var direction = ((Vector2)_player.position - _rigidBody.position).normalized;
        _rigidBody.MovePosition(_rigidBody.position + direction * _speed * Time.fixedDeltaTime);
    }
}
