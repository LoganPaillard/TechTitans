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
    public int score = 0;
    public bool sceneChanging = false;

    [Header("Timer")]
    public float endTime;
    public float timeLimit = 5f;
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
        sceneChanging = false;

        switch (scene.name)
        {
            case "MainMenu":
            {
                isGameRunning = false;
                Debug.Log("Main Menu Loaded.");
                break;
            }

            case "Spring":
            case "Summer":
            case "Autumn":
            case "Winter":
            {
                StartGame(scene);
                break;
            }

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameRunning || timerCircleImage == null) return;

        if (Time.time < endTime)
        {
            timerCircleImage.fillAmount = (endTime - Time.time) / timeLimit;
        }
        else
        {
            nextLevel();
        }
    }

    public void StartGame(Scene scene)
    {
        isGameRunning = true;
        endTime = Time.time + timeLimit;
        UpdateScore();
        Debug.Log(scene.name + " Loaded.");
    }

    public void EndGame()
    {
        isGameRunning = false;
        Debug.Log("Game Over! Final Score: " + score);
        LevelManager.Instance.LoadScene(SceneID.MainMenu, TransitionID.CrossFade);
    }

    public void nextLevel()
    {
        if (sceneChanging) return;
        sceneChanging = true;
        
        SceneID nextScene = SceneID.Spring;

        if (SceneManager.GetActiveScene().name == "Spring")
            nextScene = SceneID.Summer;
        else if (SceneManager.GetActiveScene().name == "Summer")
            nextScene = SceneID.Autumn;
        else if (SceneManager.GetActiveScene().name == "Autumn")
            nextScene = SceneID.Winter;
        else if (SceneManager.GetActiveScene().name == "Winter")
        {
            EndGame();
            return;
        }

        LevelManager.Instance.LoadScene(nextScene, TransitionID.SeasonWipe);
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
