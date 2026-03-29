using UnityEngine;
using System.Collections;

public class Fruit : Entity
{
    [Header("Growth Settings")]
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

    protected virtual IEnumerator Grow()
    {
        float timer = 0;
        transform.localScale = Vector3.zero;

        while (timer < growDuration)
        {
            timer += Time.deltaTime;
            
            float progress = timer / growDuration;
            transform.localScale = Vector3.Lerp(Vector3.zero, finalScale, progress);
            
            yield return null;
        }

        transform.localScale = finalScale;
    }
}