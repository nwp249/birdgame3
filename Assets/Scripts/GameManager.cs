using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Spawning")]
    public GameObject starPrefab;
    public int numberOfStars = 50;
    public float spawnRadius = 500f;
    public float minHeight = 50f;
    public float maxHeight = 300f;
    
    [Header("UI")]
    public Text scoreText;
    
    private int score = 0;
    
    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        SpawnCollectibles();
        UpdateScoreUI();
    }
    
    void SpawnCollectibles()
    {
        for (int i = 0; i < numberOfStars; i++)
        {
            // Random position in a circle
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            float randomHeight = Random.Range(minHeight, maxHeight);
            
            Vector3 spawnPos = new Vector3(randomCircle.x, randomHeight, randomCircle.y);
            
            Instantiate(starPrefab, spawnPos, Quaternion.identity);
        }
    }
    
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }
    
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}
