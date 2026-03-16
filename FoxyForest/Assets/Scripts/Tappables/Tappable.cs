using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class Tappable : MonoBehaviour
{
    [SerializeField] public GameObject scoreTextPrefab;

    public virtual int score => 0;
    public virtual float respawnTime => Random.Range(1f, 3f);

    protected PressGesture pressGesture;
    protected SpriteRenderer sprite;

    protected virtual void Awake()
    {
       pressGesture = GetComponent<PressGesture>();
       sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        pressGesture.Pressed += PressedHandler;
        StartCoroutine(objectTimer());
    }

    protected virtual void OnDisable()
    {
        pressGesture.Pressed -= PressedHandler;
    }

    protected virtual void PressedHandler(object sender, System.EventArgs e)
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameRunning)
        {
            if (sprite.enabled)
            {
                ShowFloatingText(score);
                StartCoroutine(waitRespawn());
            }
        }
    }

    private IEnumerator waitRespawn()
    {
        if (!sprite.enabled) yield break;
        sprite.enabled = false;

        GameManager.Instance.AddScore(score);
        yield return new WaitForSeconds(respawnTime);

        sprite.enabled = true;
        StartCoroutine(objectTimer());
    }

    private IEnumerator objectTimer()
    {
        yield return new WaitForSeconds(respawnTime);

        if (sprite.enabled)
        {
            GameManager.Instance.RemoveScore(score);
            StartCoroutine(waitRespawn());
        }
    }

    private void ShowFloatingText(int value)
    {
        if (scoreTextPrefab != null)
        {
            GameObject textObj = Instantiate(scoreTextPrefab, transform.position, UnityEngine.Quaternion.identity);
            textObj.GetComponent<FloatingText>().SetText(value);
        }
    }
}
