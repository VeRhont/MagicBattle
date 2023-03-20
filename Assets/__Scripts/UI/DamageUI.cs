using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{
    public static DamageUI Instance { get; private set; }

    private class ActiveText
    {
        public TextMeshProUGUI UIText;
        public Vector3 UnitPosition;
        public float MaxTime;
        public float Timer;

        public void MoveText(Camera camera)
        {
            float delta = 1.0f - (MaxTime / Timer);

            var position = UnitPosition + new Vector3(0, delta, 0);
            position = camera.WorldToScreenPoint(position);
            position.z = 0;

            UIText.transform.position = position;
        }
    }

    [Header("ObjectPool")]
    [SerializeField] private TextMeshProUGUI _textPrefab;
    private List<ActiveText> _activeTextList = new List<ActiveText>(); 
    private Queue<TextMeshProUGUI> _textPool = new Queue<TextMeshProUGUI>();
    const int POOL_SIZE = 64;

    private Camera _camera;
    private Transform _transform;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _camera = Camera.main;
        _transform = transform;

        for (int i = 0; i < POOL_SIZE; i++)
        {
            var temp = Instantiate(_textPrefab, _transform);
            temp.gameObject.SetActive(false);
            _textPool.Enqueue(temp);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _activeTextList.Count; i++)
        {
            var activeText = _activeTextList[i];
            activeText.Timer -= Time.deltaTime;

            if (activeText.Timer <= 0.0f)
            {
                activeText.UIText.gameObject.SetActive(false);
                _textPool.Enqueue(activeText.UIText);
                _activeTextList.RemoveAt(i);
                i--;
            }
            else
            {
                var color = activeText.UIText.color;
                color.a = activeText.Timer / activeText.MaxTime;
                activeText.UIText.color = color;

                activeText.MoveText(_camera);
            }
        }
    }

    public void AddText(int damage, Vector3 unitPosition)
    {
        var temp = _textPool.Dequeue();
        temp.text = damage.ToString();
        temp.gameObject.SetActive(true);

        var activeText = new ActiveText() { MaxTime = 1.0f };
        activeText.Timer = activeText.MaxTime;
        activeText.UIText = temp;
        activeText.UnitPosition = unitPosition + Vector3.up;

        activeText.MoveText(_camera);

        _activeTextList.Add(activeText);
    }
}
