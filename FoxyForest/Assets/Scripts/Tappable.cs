using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public enum objectID
{
    Cherry,
    Mushroom,
    Gem,
    Skunk,
    Raccoon
}

public class Tappable : MonoBehaviour
{
    [SerializeField] public objectID objectID;
    [SerializeField] public GameObject scoreTextPrefab;

    private PressGesture pressGesture;

    void Awake()
    {
       pressGesture = GetComponent<PressGesture>();
    }

    void OnEnable()
    {
        pressGesture.Pressed += PressedHandler;
        StartCoroutine(objectTimer());
    }

    void OnDisable()
    {
        pressGesture.Pressed -= PressedHandler;
    }

    private void PressedHandler(object sender, System.EventArgs e)
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameRunning)
        {
            int scoreValue = getScoreFromID(objectID);
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            if (sprite.enabled == true)
            {
                ShowFloatingText(scoreValue);
            }
            StartCoroutine(waitRespawn());
        }
    }

    private IEnumerator waitRespawn()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (!sprite.enabled) yield break;
        sprite.enabled = false;

        GameManager.Instance.AddScore(getScoreFromID(objectID));
        yield return new WaitForSeconds(getRespawnTimeFromID(objectID));

        sprite.enabled = true;
        StartCoroutine(objectTimer());
    }

    private IEnumerator objectTimer()
    {
        yield return new WaitForSeconds(getRespawnTimeFromID(objectID));
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (sprite.enabled)
        {
            GameManager.Instance.RemoveScore(getScoreFromID(objectID));
            StartCoroutine(waitRespawn());
        }
    }

    private int getScoreFromID(objectID id)
    {
        switch (id)
        {
            case objectID.Cherry:
                return 10;

            case objectID.Mushroom:
                return 42;

            case objectID.Gem:
                return 67;
            
            case objectID.Skunk:
                return -50;

            case objectID.Raccoon:
                return -100;

            default:
                return 0;
        }
    }

    private float getRespawnTimeFromID(objectID id)
    {
        switch (id)
        {
            case objectID.Cherry:
                return Random.Range(1f, 3f);

            case objectID.Mushroom:
                return Random.Range(2f, 4f);

            case objectID.Gem:
                return Random.Range(3f, 5f);
            
            case objectID.Skunk:
                return Random.Range(1f, 3f);

            case objectID.Raccoon:
                return Random.Range(2f, 4f);

            default:
                return 0f;
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
