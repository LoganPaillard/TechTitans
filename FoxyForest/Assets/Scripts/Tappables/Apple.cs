using UnityEngine;
using System.Collections;

public class Apple : Fruit
{
    public override int score => _isFalling ? 20 : 10;
    public override float respawnTime => Random.Range(5f, 10f);

    [Header("Fall Settings")]
    public float fallPosition = -3f;
    public float fallDuration = 0.5f;
    private Vector3 _spawnPosition;
    private bool _isFalling = false;

    protected override void Awake()
    {
        base.Awake();
        _spawnPosition = transform.position;
    }

    protected override void OnEnable()
    {
        transform.position = _spawnPosition;
        base.OnEnable();
    }

    protected override void respawnEffect()
    {
        transform.position = _spawnPosition;
        _isFalling = false;
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