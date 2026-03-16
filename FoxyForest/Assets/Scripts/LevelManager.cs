using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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
 
    private SceneTransition[] transitions;
    private readonly WaitForSeconds _waitHalfSec = new(0.5f);
 
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
 
        progressBar.gameObject.SetActive(true);
 
        do
        {
            progressBar.value = scene.progress;
            yield return null;
        } while (scene.progress < 0.9f);
 
        yield return _waitHalfSec;
 
        scene.allowSceneActivation = true;
 
        progressBar.gameObject.SetActive(false);
 
        yield return transition.AnimateTransitionOut();

        touchBlocker.SetActive(false);
    }
}