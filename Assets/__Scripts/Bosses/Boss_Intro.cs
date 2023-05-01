using UnityEngine;

public class Boss_Intro : StateMachineBehaviour
{
    [SerializeField] private float _destructionRadius;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 point = animator.GetComponentInParent<Transform>().position;
        var hits = Physics2D.OverlapCircleAll(point, _destructionRadius);

        foreach (var e in hits)
        {
            if (e.CompareTag("Enviroment"))
            {
                Destroy(e.gameObject);
            }
        }
    }
}
