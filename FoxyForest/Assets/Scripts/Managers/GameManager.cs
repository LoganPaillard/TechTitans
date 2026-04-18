using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("Game State")]
    public bool isGameRunning = false;
    public bool sceneChanging = false;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public Dictionary<SceneID, int> scoreSeasons = new();
    public Dictionary<string, int> countItems = new();

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
                MusicManager.Instance.PlayMusic(MusicID.Summer);
                Debug.Log("Main Menu Loaded.");
                break;
            }
            case "Spring":
            {
                scoreSeasons.Clear();
                countItems.Clear();
                MusicManager.Instance.PlayMusic(MusicID.Spring);
                StartGame(scene);
                break;
            }
            case "Summer":
            {
                scoreSeasons[SceneID.Spring] = score;
                MusicManager.Instance.PlayMusic(MusicID.Summer);
                StartGame(scene);
                break;
            }
            case "Autumn":
            {
                scoreSeasons[SceneID.Summer] = score;
                MusicManager.Instance.PlayMusic(MusicID.Autumn);
                StartGame(scene);
                break;
            }
            case "Winter":
            {
                scoreSeasons[SceneID.Autumn] = score;
                MusicManager.Instance.PlayMusic(MusicID.Spring);
                StartGame(scene);
                break;
            }
            default:
                break;
        }
    }

    public void StartGame(Scene scene)
    {
        isGameRunning = true;
        ZeroScore();
        Debug.Log(scene.name + " Loaded.");
    }

    public void EndGame()
    {
        Debug.Log("Game Over! Final Score: " + score);
        ResetAll();
        LevelManager.Instance.LoadScene(SceneID.MainMenu, TransitionID.CrossFade);
    }

    private void ResetAll()
    {
        isGameRunning = false;
        TimerController.Instance.dayNightController.UpdateLight(0.5f);
        scoreSeasons.Clear();
        countItems.Clear();
        ZeroScore();
        Debug.Log("Game Reset.");
    }

    public void NextLevel()
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

        LevelManager.Instance.LoadScene(nextScene, TransitionID.CrossFade);
    }

    public void ZeroScore()
    {
        score = 0;
        UpdateScore();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScore();
    }

    public void RemoveScore(int value)
    {
        score -= value;
        UpdateScore();
    }

    public void UpdateScore()
    {
        if (scoreText != null)
        {
            if (score < 0)
            {
                scoreText.color = Color.red;
            }
            else
            {
                scoreText.color = Color.green;
            }

            scoreText.text = score.ToString();
        }
        else
        {
            Debug.Log("ScoreText reference is null. Cannot update score display.");
        }
    }
}
