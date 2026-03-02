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

    private PressGesture pressGesture;

    void Awake()
    {
       pressGesture = GetComponent<PressGesture>();
    }

    void OnEnable()
    {
        pressGesture.Pressed += PressedHandler;
    }

    void OnDisable()
    {
        pressGesture.Pressed -= PressedHandler;
    }

    private void PressedHandler(object sender, System.EventArgs e)
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameRunning)
        {
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
    }

    private int getScoreFromID(objectID id)
    {
        switch (id)
        {
            case objectID.Cherry:
                return 10;

            case objectID.Mushroom:
                return 420;

            case objectID.Gem:
                return 69;
            
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
}
