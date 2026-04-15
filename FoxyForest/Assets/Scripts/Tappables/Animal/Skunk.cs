using UnityEngine;
using TouchScript.Gestures;

public class Skunk: Animal
{
    [SerializeField] private GameObject stinkCloudPrefab;
    private Animator animator;
    private TapGesture tapGesture;

    private bool skunkIsWalking = true;
    
    public float speed = 1f;
    public float leftBound = -10f;
    public float rightBound = 10f;
    public override int score => -50;
    public override float respawnTime => Random.Range(1f, 3f);


    private void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        tapGesture = GetComponent<TapGesture>();
    }

    void Update()
    {
        if (animator != null) {
            animator.SetBool("skunkIsWalking", skunkIsWalking);
        }
        if (!skunkIsWalking) return;

        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.position.x > rightBound)
        {
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        }
    }

    protected override void PressedHandler(object sender, System.EventArgs e)
    {
        if (sprite != null && sprite.enabled)
        {
            skunkIsWalking = false;
            if (animator != null) {
                animator.SetTrigger("skunkIsTapped");
            }
            StinkCloud();
            base.PressedHandler(sender, e);  
        }
    }

    protected override void respawnEffect()
    {
        base.respawnEffect();
        skunkIsWalking = true;
    }

    private void StinkCloud()
{
    if (stinkCloudPrefab != null)
    {
        Quaternion upsideDownRotation = Quaternion.Euler(0, 0, 90f);
        GameObject cloud = Instantiate(stinkCloudPrefab, transform.position, upsideDownRotation);
        
        // Start the growth process
        StartCoroutine(GrowAndDestroy(cloud, 2f));
    }
}

private System.Collections.IEnumerator GrowAndDestroy(GameObject cloud, float duration)
{
    float elapsed = 0f;
    Vector3 initialScale = new Vector3(0.1f, 0.1f, 1f); // Start small
    Vector3 targetScale = new Vector3(1.5f, 1.5f, 1f);  // End size

    cloud.transform.localScale = initialScale;

    while (elapsed < duration)
    {
        // Smoothly transition the scale over time
        cloud.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);
        elapsed += Time.deltaTime;
        yield return null; // Wait for the next frame
    }

    Destroy(cloud);
}
}