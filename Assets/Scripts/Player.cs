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


        //karakterin lazer ��karma mant��� yani ate� etme mant���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        //laseri olu�tural�m instantiate ile
        if (!laserActive)
        {
            Projectile projectile = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            projectile.destroyed += laserDestroyed;
            laserActive = true;
            //laser aktif de�ilse olu�tur sonra true yap de�erini ki ard arda olu�turmayal�m sonra bu laser aktif de�erini projectile k�sm�nda false �eviricez laseri destroy ettikten sonra
            //buradaki ama� laser destroy oldktan sonra ate� etmene izin veriyor olmas� yani ard� ard�na ate� etmek yerine teker teker ate� etmene izin veriyor olmas�.
        }
    }

    private void laserDestroyed()
    {
        laserActive = false;
    }

    //player'�n olmesi

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader") || collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
