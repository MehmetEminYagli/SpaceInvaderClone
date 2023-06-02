using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] AnimationSprite;

    public float animationsüresi = 1.0f;

    private SpriteRenderer _spriteRenderer;

    private int _animationFrame; //neden alt çizgi koymayýnca hata veriyor çünkü animationFrame isimli bir deðiþken zaten ünity de var eðer kendin türkçe bir yazý yazarsan hata almýyorsun

    public System.Action killed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

        InvokeRepeating(nameof(animateSprite), animationsüresi, animationsüresi);
    }

    private void animateSprite()
    {

        _animationFrame++;

        if (_animationFrame >= this.AnimationSprite.Length)
        {
            _animationFrame = 0;
        }

        _spriteRenderer.sprite = this.AnimationSprite[_animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            killed.Invoke();
            gameObject.SetActive(false);
        }
    }
}
