using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] AnimationSprite;

    public float animations�resi = 1.0f;

    private SpriteRenderer _spriteRenderer;

    private int _animationFrame; //neden alt �izgi koymay�nca hata veriyor ��nk� animationFrame isimli bir de�i�ken zaten �nity de var e�er kendin t�rk�e bir yaz� yazarsan hata alm�yorsun

    public System.Action killed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

        InvokeRepeating(nameof(animateSprite), animations�resi, animations�resi);
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
