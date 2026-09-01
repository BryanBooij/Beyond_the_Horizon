using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed = 3f;

    void Update()
    {
        transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}