using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnviromentMovement : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animator.SetTrigger("Move");
    }
}
