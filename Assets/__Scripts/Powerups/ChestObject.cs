using UnityEngine;

public enum ChestType
{
    coins,
    soul,
    crystals
}

[CreateAssetMenu(fileName = "Chest")]
public class ChestObject : ScriptableObject
{
    [SerializeField] private ChestType _type;
    [SerializeField] private Sprite _chestSprite;
    [SerializeField] private int _minCount;
    [SerializeField] private int _maxCount;
    [SerializeField, Range(0, 1)] private float _valueToSpawn;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private AudioClip _pickupSound;

    public ChestType Type { get { return _type; } }
    public Sprite ChestSprite { get { return _chestSprite; } }
    public int MinCount { get { return _minCount; } }
    public int MaxCount { get { return _maxCount; } }
    public float ValueToSpawn { get { return _valueToSpawn; } }
    public ParticleSystem Particles { get { return _particles; } }
    public AudioClip PickupSound { get { return _pickupSound; } }
}
