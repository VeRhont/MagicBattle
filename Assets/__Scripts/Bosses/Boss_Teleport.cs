using UnityEngine;

public class Boss_Teleport : StateMachineBehaviour
{
    private Transform _player;
    private Rigidbody2D _rigidBody;

    [SerializeField] private float _timer = 5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidBody = animator.GetComponentInParent<Rigidbody2D>();
        _timer = 5f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_timer <= 0)
        {
            var offset = new Vector3(Random.Range(-3, 3), 0, 0);
            _rigidBody.position = _player.position + offset;
            _timer = 10000f;
        }

        _timer -= Time.fixedDeltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Teleport");
    }
}
