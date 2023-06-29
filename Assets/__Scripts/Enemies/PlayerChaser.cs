using UnityEngine;

public class PlayerChaser
{
    private Transform _playerTransform;
    private float _offset;

    public PlayerChaser(Transform player, float offset)
    {
        _playerTransform = player;
        _offset = offset;
    }

    public Vector2 GetPlayerPosition()
    {
        return PositionGenerator.Instance.GetPositionAroundPlayer(_offset, _playerTransform);
    }
}