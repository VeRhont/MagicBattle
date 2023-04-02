using UnityEngine;

public class PortalController : MonoBehaviour
{
    public void TeleportToBattle()
    {
        ChangeScene.Instance.FadeToScene(SceneType.EndlessMode);
    }
}
