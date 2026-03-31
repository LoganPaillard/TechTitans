using UnityEngine;
using System.Collections;

public class FallingEntity : Fruit
{
    [Header("Fall Settings")]
    public float fallPositionUpper = -2f;
    public float fallPositionLower = -4f;
    private float fallPosition;
    public float fallDuration = 1f;
    protected Vector3 _spawnPosition;
    protected bool _isFalling = false;

    protected override void Awake()
    {
        base.Awake();
        _spawnPosition = transform.position;
    }

    protected override void OnEnable()
    {
        transform.position = _spawnPosition;
        fallPosition = Random.Range(fallPositionUpper, fallPositionLower);
        base.OnEnable();
    }

    protected override void respawnEffect()
    {
        transform.position = _spawnPosition;
        _isFalling = false;
        fallPosition = Random.Range(fallPositionUpper, fallPositionLower);
        base.respawnEffect();
    }
    protected override IEnumerator Grow()
    {
        yield return base.Grow();
        StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        _isFalling = true;
        float timer = 0;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, fallPosition, startPosition.z);

        while (timer < fallDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fallDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, progress);
            yield return null;
        }

        transform.position = endPosition;
        _isFalling = false;
    }
}
