using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance;
    public GameObject springTimer;
    public GameObject summerTimer;
    public GameObject autumnTimer;
    public DayNightController dayNightController;
    public float timeLimit = 5f;
    public GameObject timerRoot;

    private Petal[] petals;
    private float endTime;
    private Coroutine petalCoroutine;
    private bool timerRunning;

    void OnEnable() => SceneManager.sceneLoaded += OnLevelLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnLevelLoaded;

    private void Awake()
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

    void Start()
    {
        OnLevelLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void Update()
    {
        if (!timerRunning) return;

        if (Time.time < endTime)
        {
            float timeRemaining = (endTime - Time.time) / timeLimit;
            dayNightController.UpdateLight(1f - timeRemaining);
        }
        else
        {
            timerRunning = false;
            dayNightController.UpdateLight(1f);
            petalCoroutine = null;
            GameManager.Instance.nextLevel();
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            SetTimerVisibility(false);
            StopTimer();
            ResetPetals(false);
            return;
        }

        SetSeasonalTimer(scene.name);
        SetTimerVisibility(true);
        ResetPetals(true);
        ResetTimer();
        StartTimer();
    }

    public void ResetTimer()
    {
        endTime = Time.time + timeLimit;
        dayNightController.UpdateLight(0f);
        timerRunning = false;
    }

    public void StartTimer()
    {
        StopTimer();
        petalCoroutine = StartCoroutine(PetalTimer());
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
        if (petalCoroutine != null)
        {
            StopCoroutine(petalCoroutine);
            petalCoroutine = null;
        }
    }

    private void SetSeasonalTimer(string sceneName)
    {
        DisableAllSeasonalTimers();

        GameObject activeTimer = sceneName switch
        {
            "Spring" => springTimer,
            "Summer" => summerTimer,
            "Autumn" => autumnTimer,
            "Winter" => springTimer,
            _ => null
        };

        if (activeTimer != null)
        {
            activeTimer.SetActive(true);
            petals = activeTimer.GetComponentsInChildren<Petal>();
        }
    }

    private void DisableAllSeasonalTimers()
    {
        if (springTimer != null) springTimer.SetActive(false);
        if (summerTimer != null) summerTimer.SetActive(false);
        if (autumnTimer != null) autumnTimer.SetActive(false);
    }

    private IEnumerator PetalTimer()
    {
        if (petals == null || petals.Length == 0)
            yield break;

        float interval = timeLimit / petals.Length;

        foreach (var petal in petals)
        {
            yield return new WaitForSeconds(interval);
            if (petal != null && petal.icon != null)
            {
                petal.icon.enabled = false;
            }
        }
    }

    private void ResetPetals(bool enable)
    {
        if (petals == null) return;

        foreach (var petal in petals)
        {
            if (petal != null && petal.icon != null)
            {
                petal.icon.enabled = enable;
            }
        }
    }

    private void SetTimerVisibility(bool visible)
    {
        if (timerRoot != null)
        {
            timerRoot.SetActive(visible);
        }
    }
}
