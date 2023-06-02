using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 yon;

    public float speed;

    public System.Action destroyed;

    void Update()
    {
        transform.position += yon * speed * Time.deltaTime;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyed != null)
        {
            destroyed.Invoke();
        }
        
        Destroy(gameObject);
    }
}
