using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("Game State")]
    public bool isGameRunning = false;
    public TextMeshProUGUI scoreText;
    public int score;

    [Header("Timer")]
    public float initialTime = 60f; // Total game time in seconds
    private float timeRemaining;
    public Image timerCircleImage;    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void OnEnable() => SceneManager.sceneLoaded += OnLevelLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnLevelLoaded;

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu")
        {
            StartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameRunning || timerCircleImage == null) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerCircleImage.fillAmount = timeRemaining / initialTime;
        }
        else
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        isGameRunning = true;
        timeRemaining = initialTime;
        score = 0;
        Debug.Log("Game Started!");
    }

    public void EndGame()
    {
        isGameRunning = false;
        Debug.Log("Game Over! Final Score: " + score);
        LevelManager.Instance.LoadScene(SceneID.MainMenu, TransitionID.CrossFade);
    }

    public void AddScore(int value)
    {
        this.score += value;
        UpdateScore();
    }

    public void RemoveScore(int value)
    {
        this.score -= value;
        UpdateScore();
    }

    public void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
            Debug.Log("Score updated: " + score);
        }
        else
        {
            Debug.Log("ScoreText reference is null. Cannot update score display.");
        }
    }

    public void togglePause()
    {
        if (isGameRunning)
        {
            isGameRunning = false;
            Time.timeScale = 0f;
        }
        else
        {
            isGameRunning = true;
            Time.timeScale = 1f;
        }
    }
}
