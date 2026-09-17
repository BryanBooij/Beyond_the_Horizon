using UnityEngine;

public class BoomScript : MonoBehaviour
{
    public float lifetime = 2f;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip explosionSound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Play explosion sound
        audioSource.PlayOneShot(explosionSound);
        
        Destroy(gameObject, lifetime);
    }
}
