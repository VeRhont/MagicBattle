using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float _damage;

    private PlayerController _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                _player.TakeDamage(_damage);
                break;

            case "Enviroment":
                Destroy(collision.gameObject);
                break;
        }
    }
}
