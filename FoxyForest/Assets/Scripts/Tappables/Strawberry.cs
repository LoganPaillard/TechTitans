using UnityEngine;
using System.Collections;

public class Strawberry: Tappable
{
    public override int score => 16;
    public override float respawnTime => Random.Range(2f, 4f);

    [Header("Growth Settings")]
    // [Range(0.1f, 1f)] // This adds a slider to the Inspector!
    // public float scaleFactor = 0.5f;
    public float growDuration = 0.8f;
    public Vector3 finalScale = Vector3.one;

    protected override void OnEnable()
    {
        base.OnEnable(); 
        transform.localScale = Vector3.zero;
        StartCoroutine(Grow());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        transform.localScale = Vector3.zero;
    }

    protected override void respawnEffect()
    {
        StopAllCoroutines();
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        float timer = 0;
        transform.localScale = Vector3.zero;

        while (timer < growDuration)
        {
            timer += Time.deltaTime;
            
            float progress = timer / growDuration;
            transform.localScale = Vector3.Lerp(Vector3.zero, finalScale, progress);
            //transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(scaleFactor, scaleFactor, scaleFactor), progress);
            
            yield return null;
        }

        transform.localScale = finalScale;
    }
}