// StarCollectible.cs
using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public int pointValue = 10;
    
    void Update()
    {
        // Spin the star
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add score (we'll create GameManager next)
            GameManager.instance?.AddScore(pointValue);
            
            // Destroy the star
            Destroy(gameObject);
        }
    }
}