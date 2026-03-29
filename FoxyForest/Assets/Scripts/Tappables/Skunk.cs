using UnityEngine;
using TouchScript.Gestures;

public class Skunk: Animal
{
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

    private void OnTapped(object sender, System.EventArgs e)
    {
      
        skunkIsWalking = false;
        animator.SetTrigger("skunkIsTapped");

        PressedHandler(sender, e);  

    }
}