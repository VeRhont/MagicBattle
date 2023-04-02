using UnityEngine;

public class FountainController : MonoBehaviour
{
    public void GiveRandomPower()
    {
        Debug.Log(Random.RandomRange(1, 10));
    }
}
