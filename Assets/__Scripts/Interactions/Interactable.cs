using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject _outline;
    [SerializeField] private KeyCode _keyToInteract;
    [SerializeField] private UnityEvent _interactAction;
    [SerializeField] private UnityEvent _exitRangeAction;

    private bool _isInRange = false;

    private void Update()
    {
        if (_isInRange)
        {
            if (Input.GetKeyDown(_keyToInteract))
            {
                _interactAction?.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isInRange = true;
            _outline.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isInRange = false;
            _outline.SetActive(false);

            if (_exitRangeAction != null)
            {
                _exitRangeAction?.Invoke();
            }
        }
    }
}
