using UnityEngine;
using System.Collections;

public class ShockWave : MonoBehaviour
{
    private float _radius = 0f;
    private float _force = 0f;
    private float _cooldownTime = 0f;
    private float _usingTime = 0f;
    private bool _isActive = true;

    public void Initialize(float radius, float force, float cooldownTime, float usingTime)
    {
        _radius = radius;
        _force = force;
        _cooldownTime = cooldownTime;
        _usingTime = usingTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && _isActive)
            Explode();
    }

    private void Explode()
    {
        StartCoroutine(StartCooldown());

        var overlappedArea = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (var collider in overlappedArea)
        {
            if (collider.CompareTag("Enemy"))
            {
                var enemyRb = collider.attachedRigidbody;
                StartCoroutine(PushEnemy(enemyRb));
            }
        }
    }

    private IEnumerator PushEnemy(Rigidbody2D enemyRb)
    {
        var enemy = enemyRb.GetComponent<Enemy>();
        enemy.enabled = false;
        enemyRb.velocity = Vector2.zero;

        var direction = enemyRb.transform.position - transform.position;
        enemyRb.AddForce(direction * _force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_usingTime);

        enemy.enabled = true;
    }

    private IEnumerator StartCooldown()
    {
        _isActive = false;
        yield return new WaitForSeconds(_cooldownTime);
        _isActive = true;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
