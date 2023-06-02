using UnityEngine;

public class Boss_LeftDash : StateMachineBehaviour
{
    [SerializeField] private float _speed = 4f;
    private Rigidbody2D _rigidBody;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rigidBody = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rigidBody.MovePosition(_rigidBody.position + Vector2.left * _speed * Time.fixedDeltaTime);
    }
}
