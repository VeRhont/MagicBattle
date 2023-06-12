using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnviromentMovement : MonoBehaviour
{
    [SerializeField] private AudioClip _foliageSound;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animator.SetTrigger("Move");
        
        if (_foliageSound != null && collision.CompareTag("Player"))
            AudioManager.Instance.PlaySound(_foliageSound);
    }
}
