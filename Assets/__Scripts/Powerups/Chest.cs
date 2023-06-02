using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private ChestType _type;
    private int _minCount;
    private int _maxCount;

    private ParticleSystem _particles;
    private AudioClip _pickupSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(_particles, transform.position, _particles.transform.rotation);

            var randomCount = Random.Range(_minCount, _maxCount);

            switch (_type)
            {
                case ChestType.coins:
                    PlayerWallet.Instance.Coins += randomCount;
                    break;

                case ChestType.soul:
                    PlayerWallet.Instance.Soul += randomCount;
                    break;

                case ChestType.crystals:
                    PlayerWallet.Instance.Crystals += randomCount;
                    break;
            }
            
            UI_Manager.Instance.UpdateResourcesCount();
            DamageUI.Instance.AddText(randomCount, collision.transform.position + Vector3.up);
            AudioManager.Instance.PlaySound(_pickupSound);

            Destroy(gameObject);
        }
    }

    public void SetChestValues(ChestObject chest)
    {
        _spriteRenderer.sprite = chest.ChestSprite;
        _type = chest.Type;
        _minCount = chest.MinCount;
        _maxCount = chest.MaxCount;
        _particles = chest.Particles;
        _pickupSound = chest.PickupSound;
    }
}
