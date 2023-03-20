using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private int _minPointsCount = 100;
    [SerializeField] private int _maxPointsCount = 1000;

    private ParticleSystem _particles;

    private void Start()
    {
        _particles = GameObject.FindGameObjectWithTag("ChestParticles").GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _particles.transform.position = transform.position;
            _particles.Play();

            var points = Random.Range(_minPointsCount, _maxPointsCount);

            DamageUI.Instance.AddText(points, collision.transform.position + Vector3.up);

            collision.gameObject.GetComponent<PlayerController>().UpdateScore(points);
            Destroy(gameObject);
        }
    }
}
