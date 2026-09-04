using UnityEngine;

public class Projectile_Lazer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float Damage = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * (moveSpeed * Time.deltaTime));
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletBoundary"))
        {
            Destroy(gameObject);
        }
    }
}
