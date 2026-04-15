using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class Entity : MonoBehaviour
{
    [SerializeField] public GameObject scoreTextPrefab;

    protected PressGesture pressGesture;
    protected SpriteRenderer sprite;
    private PolygonCollider2D polygonCollider;

    [Header("Entity Settings")]
    [SerializeField] private int _score = 0;
    [SerializeField] private float _respawnTimeUpper = 3f;
    [SerializeField] private float _respawnTimeLower = 1f;

    public virtual int score => _score;
    public virtual float respawnTime => Random.Range(_respawnTimeLower, _respawnTimeUpper);

    [Header("Spawn Settings")]
    [SerializeField] protected bool useRandomSpawnPoint = true;
    [SerializeField] protected float spawnRadius = 0.5f;
    private Vector3 initialPosition;

    protected virtual void Awake()
    {
       pressGesture = GetComponent<PressGesture>();
       sprite = GetComponent<SpriteRenderer>();
       polygonCollider = GetComponent<PolygonCollider2D>();

       initialPosition = transform.position;
       if (useRandomSpawnPoint) RandomizeSpawnPosition();
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
                var countItems = GameManager.Instance.countItems;
                countItems[gameObject.name] = countItems.ContainsKey(gameObject.name) ? countItems[gameObject.name] + 1 : 1;
                Debug.Log($"Tapped {gameObject.name}. Total count: {countItems[gameObject.name]}");
                
                ShowFloatingText(score);
                StartCoroutine(waitRespawn());
            }
        }
    }

    protected void RandomizeSpawnPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        transform.position = initialPosition + new Vector3(randomCircle.x, randomCircle.y, 0);
    }

    private IEnumerator waitRespawn()
    {
        if (!sprite.enabled) yield break;
        sprite.enabled = false;

        if(polygonCollider != null) polygonCollider.enabled = false;

        GameManager.Instance.AddScore(score);
        yield return new WaitForSeconds(respawnTime);

        if (useRandomSpawnPoint) RandomizeSpawnPosition();

        sprite.enabled = true;
        if(polygonCollider != null) polygonCollider.enabled = true;

        respawnEffect();
        StartCoroutine(objectTimer());
    }

    protected virtual void respawnEffect()
    {
        
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
