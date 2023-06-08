using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealPlayer : MonoBehaviour
{
    [SerializeField] private int _healthPoints = 30;

    private ParticleSystem _particles;

    private void Start()
    {
        _particles = GameObject.FindGameObjectWithTag("HealParticles").GetComponent<ParticleSystem>();
        _healthPoints = (int)PlayerPrefs.GetFloat("healPotionUpgradeValue", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _particles.transform.position = transform.position;
            _particles.Play();

            var heal = Random.Range(_healthPoints, _healthPoints + 1);

            DamageUI.Instance.AddText(heal, collision.transform.position + Vector3.up);

            collision.gameObject.GetComponent<PlayerController>().Heal(heal);

            Destroy(gameObject);
        }
    }
}
