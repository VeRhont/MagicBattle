using UnityEngine;

public class PositionGenerator : MonoBehaviour
{
    public static PositionGenerator Instance;

    [Header("Coordinates")]
    [SerializeField] private Transform _leftUpCorner;
    [SerializeField] private Transform _rightDownCorner;

    private float _rightBound;
    private float _leftBound;
    private float _upBound;
    private float _downBound;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        _rightBound = _rightDownCorner.position.x;
        _leftBound = _leftUpCorner.position.x;
        _downBound = _rightDownCorner.position.y;
        _upBound = _leftUpCorner.position.y;
    }

    public Vector2 GetRandomPosition()
    {
        int x = Mathf.RoundToInt(Random.Range(_leftBound, _rightBound));
        int y = Mathf.RoundToInt(Random.Range(_downBound, _upBound));

        return new Vector2(x, y);
    }

    public Vector2 GetPositionAroundPlayer(float radius, Transform playerTransform)
    {
        if (radius >= (_rightBound - _leftBound) / 2)
        {
            radius = (_rightBound - _leftBound) / 2 - 1;
        }

        var playerPosition = playerTransform.transform.position;
        var generatedPosition = new Vector2();

        do
        {
            var angle = Random.Range(0, 360);
            var x = radius * Mathf.Cos(angle);
            var y = radius * Mathf.Sin(angle);

            generatedPosition = new Vector2(playerPosition.x + x, playerPosition.y + y);
        }
        while (IsInsideBounds(generatedPosition) == false);

        return generatedPosition;
    }

    public bool IsInsideBounds(Vector2 position)
    {
        var xBound = (_leftBound < position.x) && (position.x < _rightBound);
        var yBound = (_downBound < position.y) && (position.y < _upBound);
        return xBound && yBound;
    }
}