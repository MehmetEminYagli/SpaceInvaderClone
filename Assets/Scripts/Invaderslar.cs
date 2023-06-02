using UnityEngine.SceneManagement;
using UnityEngine;

public class Invaderslar : MonoBehaviour
{
    //ýnvaderlar yani düþmanlar satýr ve kolon þeklinde sýralanacak yani bir nevi grid sistemi

    public Invader[] dusmanprefabs;

    public int rows = 5;

    public int columns = 10;

   // public AnimationCurve speed;
    public float speed = 1f;

    public Projectile missilePrefab;

    private Vector3 yon = Vector2.right;

    public int amountKilled { get; private set; } //public al private olarak deðiþtir

    public int amountAlive => totalInvader - amountKilled;
    public int totalInvader => rows * columns;
    public float percentKilled => (float)amountKilled / (float)totalInvader;

    public float missileattackRate = 1f;

    public void Awake()
    {
        for (int row = 0; row < rows; row++)
        {
            float width = 2f * (columns - 1);
            float height = 2f * (rows - 1);
            Vector2 ortalama = new Vector2(-width / 2, -height / 2);

           
            Vector3 rowPosition = new Vector3(ortalama.x, ortalama.y + (row * 2.0f), 0.0f);

            for (int col = 0; col < columns; col++)
            {
                //bu kodu 5 10 þeklinde verdiðimiz düþman prefablarýný oluþturuyor ama hepsi ayný yerde oluyor þimdi onu düzeltmemiz gerekiyor
                Invader invader = Instantiate(dusmanprefabs[row], transform);

                invader.killed += InvaderKilled;
                
                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), missileattackRate, missileattackRate);
    }

    private void Update()
    {
        //þimdi düþman ýnvaderslarýn hareketlerini kodlayalým
        transform.position += yon * speed * Time.deltaTime; //sag tarafa doðru durmadan hareket ediyorlar :) ama bunu belirli bir alandan sonra sol tarafa doðru döndürmemiz lazým ki haritanýn dýþýna çýkmasýnlar

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero); //kamera yardýmýyla sol kenarý hesaplayabiliriz
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            //neden foreach çünkü oyun baþladýðýnda spawn olan her clone düþmanlar için yap bunu yani onlarýn child'larýna da uygulamamýz gerekiyor

            if (!invader.gameObject.activeInHierarchy) //düþmanlar aktif deðilse devam et çalýþmaya
            {
                continue;
            }

            if (yon == Vector3.right && invader.position.x >= (rightEdge.x - 1f))
            {
                AdvanceRow();
            }
            else if (yon == Vector3.left && invader.position.x <= (leftEdge.x + 1f))
            {
                AdvanceRow();
            }

        }
    }

    private void AdvanceRow() // bu arkadaþ invaderlar her bir kenara geldiklerinde bir satýr aþaðýya doðru bize doðru geliyolar
    {//bu kod bloðu bu iþe yarýyor y deðerini 1 azaltarak düþmanlarý bize doðru getiriyor
        yon.x *= -1f;

        Vector3 position = transform.position;
        position.y -= 1f;
        transform.position = position;

    }
    private void MissileAttack()
    {
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1f / (float)amountAlive)) //random value 0 ile 1 arasýnda deðer döndürür
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void InvaderKilled()
    {
        amountKilled++;

        if (amountKilled >= totalInvader)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
