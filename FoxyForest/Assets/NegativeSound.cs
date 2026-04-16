using UnityEngine;
using TouchScript.Gestures;

public class NegativeSoundTouch : MonoBehaviour
{
    // We hardcode this to Buzzer or set it in the Inspector
    [SerializeField] private SoundID negativeSound = SoundID.Buzzer;
    private PressGesture pressGesture;

    void Awake() => pressGesture = GetComponent<PressGesture>();

    void OnEnable() => pressGesture.Pressed += (s, e) => {
        // Just call the manager directly!
        SoundManager.Instance.PlaySound2D(negativeSound);
    };

    void OnDisable() => pressGesture.Pressed -= (s, e) => { }; 
}