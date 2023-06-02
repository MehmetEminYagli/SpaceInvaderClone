using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float speed = 5f;

    public Projectile laserPrefab;

    private bool laserActive;

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }


        //karakterin lazer çýkarma mantýðý yani ateþ etme mantýðý
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        //laseri oluþturalým instantiate ile
        if (!laserActive)
        {
            Projectile projectile = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            projectile.destroyed += laserDestroyed;
            laserActive = true;
            //laser aktif deðilse oluþtur sonra true yap deðerini ki ard arda oluþturmayalým sonra bu laser aktif deðerini projectile kýsmýnda false çeviricez laseri destroy ettikten sonra
            //buradaki amaç laser destroy oldktan sonra ateþ etmene izin veriyor olmasý yani ardý ardýna ateþ etmek yerine teker teker ateþ etmene izin veriyor olmasý.
        }
    }

    private void laserDestroyed()
    {
        laserActive = false;
    }

    //player'ýn olmesi

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader") || collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
