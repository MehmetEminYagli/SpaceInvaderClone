using UnityEngine.SceneManagement;
using UnityEngine;

public class Invaderslar : MonoBehaviour
{
    //�nvaderlar yani d��manlar sat�r ve kolon �eklinde s�ralanacak yani bir nevi grid sistemi

    public Invader[] dusmanprefabs;

    public int rows = 5;

    public int columns = 10;

   // public AnimationCurve speed;
    public float speed = 1f;

    public Projectile missilePrefab;

    private Vector3 yon = Vector2.right;

    public int amountKilled { get; private set; } //public al private olarak de�i�tir

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
                //bu kodu 5 10 �eklinde verdi�imiz d��man prefablar�n� olu�turuyor ama hepsi ayn� yerde oluyor �imdi onu d�zeltmemiz gerekiyor
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
        //�imdi d��man �nvaderslar�n hareketlerini kodlayal�m
        transform.position += yon * speed * Time.deltaTime; //sag tarafa do�ru durmadan hareket ediyorlar :) ama bunu belirli bir alandan sonra sol tarafa do�ru d�nd�rmemiz laz�m ki haritan�n d���na ��kmas�nlar

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero); //kamera yard�m�yla sol kenar� hesaplayabiliriz
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            //neden foreach ��nk� oyun ba�lad���nda spawn olan her clone d��manlar i�in yap bunu yani onlar�n child'lar�na da uygulamam�z gerekiyor

            if (!invader.gameObject.activeInHierarchy) //d��manlar aktif de�ilse devam et �al��maya
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

    private void AdvanceRow() // bu arkada� invaderlar her bir kenara geldiklerinde bir sat�r a�a��ya do�ru bize do�ru geliyolar
    {//bu kod blo�u bu i�e yar�yor y de�erini 1 azaltarak d��manlar� bize do�ru getiriyor
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

            if (Random.value < (1f / (float)amountAlive)) //random value 0 ile 1 aras�nda de�er d�nd�r�r
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
