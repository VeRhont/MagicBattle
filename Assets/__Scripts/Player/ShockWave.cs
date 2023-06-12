using UnityEngine;
using System.Collections;

public class ShockWave : MonoBehaviour
{
    private float _radius;
    private float _force;
    private float _cooldownTime;
    private float _usingTime;

    public void Initialize(float radius, float force, float cooldownTime, float usingTime)
    {
        _radius = radius;
        _force = force;
        _cooldownTime = cooldownTime;
        _usingTime = usingTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            Explode();
    }

    private void Explode()
    {
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

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
