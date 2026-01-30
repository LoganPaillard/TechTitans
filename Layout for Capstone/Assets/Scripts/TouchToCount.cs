
using UnityEngine;
using TouchScript.Gestures;

public class TouchToCount : MonoBehaviour
{
    public Counter counter;
    private PressGesture pressGesture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        counter.IncreaseCounter();
    }
}
