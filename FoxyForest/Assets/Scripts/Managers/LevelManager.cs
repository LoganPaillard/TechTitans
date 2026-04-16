using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public enum SceneID {
    MainMenu,
    Spring,
    Summer,
    Autumn,
    Winter
}

public enum TransitionID {
    CrossFade,
    CircleWipe,
    SeasonWipe
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public GameObject touchBlocker;
    public Slider progressBar;
    public GameObject transitionsContainer;
    public TextMeshProUGUI seasonMessage;
    private SceneTransition[] transitions;
 
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
 
    private void Start()
    {
        transitions = transitionsContainer.GetComponentsInChildren<SceneTransition>();
    }
 
    public void LoadScene(SceneID sceneID, TransitionID transitionID)
    {
        StartCoroutine(LoadSceneAsync(sceneID, transitionID));
    }
 
    private IEnumerator LoadSceneAsync(SceneID sceneID, TransitionID transitionID)
    {
        touchBlocker.SetActive(true);

        SceneTransition transition = transitions.First(t => t.name == transitionID.ToString());
 
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneID.ToString());
        scene.allowSceneActivation = false;
 
        yield return transition.AnimateTransitionIn();

        GetSeasonMessage(sceneID);
        progressBar.gameObject.SetActive(true);
 
        do
        {
            progressBar.value = scene.progress;
            yield return null;
        } while (scene.progress < 0.9f);
 
        yield return new WaitForSeconds(2f);
 
        scene.allowSceneActivation = true;
 
        progressBar.gameObject.SetActive(false);
 
        yield return transition.AnimateTransitionOut();

        touchBlocker.SetActive(false);
    }

    private void GetSeasonMessage(SceneID sceneID)
    {
        switch (sceneID)
        {
            case SceneID.Spring:
                seasonMessage.text = "Spring";
                seasonMessage.color = new Color(0.5f, 1f, 0.5f);
                break;
            case SceneID.Summer:
                seasonMessage.text = "Summer";
                seasonMessage.color = new Color(1f, 0.85f, 0.5f);
                break;
            case SceneID.Autumn:
                seasonMessage.text = "Autumn";
                seasonMessage.color = new Color(1f, 0.5f, 0f);
                break;
            case SceneID.Winter:
                seasonMessage.text = "Winter";
                seasonMessage.color = new Color(0.5f, 0.5f, 1f);
                break;
            case SceneID.MainMenu:
                seasonMessage.text = "";
                break;
        }
    }
}