using UnityEngine;
using System.Collections;

public class DestroyWithTime : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private Vector3 _deltaSize;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
        {
            StartCoroutine(DestroyObject());
        }
    }

    private IEnumerator DestroyObject()
    {
        while (transform.localScale.x > 0.01f)
        {
            transform.localScale -= _deltaSize;

            yield return new WaitForSeconds(0.5f);
        }

        Destroy(gameObject);
    }
}
