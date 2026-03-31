using UnityEngine;
using System.Collections;

public class FallingEntity : Fruit
{
    [Header("Fall Settings")]
    public float fallPositionUpper = -2f;
    public float fallPositionLower = -4f;
    private float fallPosition;
    public float fallDuration = 1f;

    [Header("Shake Settings")]
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public float shakeSpeed = 20f;
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
        yield return StartCoroutine(Shake());
        yield return StartCoroutine(Fall());
    }

    private IEnumerator Shake()
    {
        float timer = 0;

        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            float OffsetX = Mathf.Sin(timer * shakeSpeed) * shakeMagnitude;
            transform.position = _spawnPosition + new Vector3(OffsetX, 0, 0);
            yield return null;
        }

        transform.position = _spawnPosition;
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
