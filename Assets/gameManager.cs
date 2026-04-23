using System.Collections;
using UnityEngine;
using TMPro; // Assuming you are using TextMeshPro for UI

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject startButton;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI surviveTimeText;
    public GameObject gameOverPanel;

    [Header("Audio")]
    public AudioSource backgroundMusic;
    public AudioClip deathSound;
    private AudioSource sfxSource;

    [Header("References")]
    public EnemySpawner spawner;

    private float timeSurvived = 0f;
    private bool isPlaying = false;

    void Start()
    {
        sfxSource = gameObject.AddComponent<AudioSource>();
        gameOverPanel.SetActive(false);
        countdownText.gameObject.SetActive(false);
        surviveTimeText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlaying)
        {
            timeSurvived += Time.deltaTime;
        }
    }

    // Link this method to your UI Start Button's "On Click" event
    public void StartGame()
    {
        startButton.SetActive(false);
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);
        for (int i = 5; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        
        countdownText.gameObject.SetActive(false);
        surviveTimeText.gameObject.SetActive(false); // Hide until death
        
        // Start the game!
        isPlaying = true;
        timeSurvived = 0f;
        backgroundMusic.Play();
        spawner.StartSpawning();
    }

    public void GameOver()
    {
        isPlaying = false;
        backgroundMusic.Stop();
        sfxSource.PlayOneShot(deathSound);
        
        spawner.StopSpawning();
        
        // Despawn all active enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Show Game Over UI
        gameOverPanel.SetActive(true);
        surviveTimeText.gameObject.SetActive(true);
        surviveTimeText.text = "You Survived: " + timeSurvived.ToString("F2") + " seconds";
    }
}