using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance;
    public GameObject springTimer;
    public GameObject summerTimer;
    public GameObject autumnTimer;
    public DayNightController dayNightController;
    public float timeLimit = 5f;
    public GameObject timerRoot;
    public TextMeshProUGUI timeUpMessage;

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
            if (SceneManager.GetActiveScene().name != "Winter")
                dayNightController.UpdateLight(1f - timeRemaining);
        }
        else
        {
            timerRunning = false;
            petalCoroutine = null;
            if (SceneManager.GetActiveScene().name != "Winter")
                StartCoroutine(TimeUpSequence());
            else
                GameManager.Instance.NextLevel();
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        timeUpMessage.gameObject.SetActive(false);

        if (scene.name == "MainMenu")
        {
            dayNightController.UpdateLight(0.5f);
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
        if (SceneManager.GetActiveScene().name != "Winter")
            dayNightController.UpdateLight(1f);
        else
            dayNightController.UpdateLight(0.5f);
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
            float elapsed = 0f;
            while (elapsed < interval)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / interval);
                float alpha = Mathf.Lerp(1f, 0f, t);
                Color c = petal.icon.color;
                c.a = alpha;
                petal.icon.color = c;
                yield return null;
            }
            petal.icon.enabled = false;
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
                Color c = petal.icon.color;
                c.a = enable ? 1f : 0f;
                petal.icon.color = c;
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

    private IEnumerator TimeUpSequence()
    {
        if (timeUpMessage != null)
        {
            timeUpMessage.gameObject.SetActive(true);
            LevelManager.Instance.touchBlocker.SetActive(true);
            yield return new WaitForSeconds(3f);
            GameManager.Instance.NextLevel();
        }
    }
}
