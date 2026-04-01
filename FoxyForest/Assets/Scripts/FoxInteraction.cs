using UnityEngine;
using TouchScript.Gestures;

public class Foxinteraction : MonoBehaviour
{
    private Animator animator;
    private TapGesture tapGesture;

    void Awake()
    {
        animator = GetComponent<Animator>();
        tapGesture = GetComponent<TapGesture>();
    }

    void OnEnable()
    {
        tapGesture.Tapped += OnTapped;
    }

    void OnDisable()
    {
        tapGesture.Tapped -= OnTapped;
    }

    private void OnTapped(object sender, System.EventArgs e)
    {
        Debug.Log("Fox tapped!");
        animator.SetTrigger("wag");
    }
}
