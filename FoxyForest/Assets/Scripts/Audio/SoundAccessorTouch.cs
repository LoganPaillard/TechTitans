using UnityEngine;
using TouchScript.Gestures;
using UnityEngine.UI;

public class SoundAccessorTouch : MonoBehaviour
{
    [SerializeField]
    private SoundID hoverSound;
    
    [SerializeField]
    private SoundID clickSound;

    private PressGesture pressGesture;

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
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        
        if (sprite != null)
        {
            if (!sprite.enabled) return;
            SoundManager.Instance.PlaySound2D(hoverSound);
        }
        else {
            SoundManager.Instance.PlaySound2D(clickSound);
        }
    }
}
