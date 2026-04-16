using UnityEngine;
using TouchScript.Gestures;

public class NegativeSoundTouch : MonoBehaviour
{
    [SerializeField] private SoundID negativeSound = SoundID.Buzzer;
    private PressGesture pressGesture;

    void Awake() => pressGesture = GetComponent<PressGesture>();

    void OnEnable() => pressGesture.Pressed += (s, e) => {
        SoundManager.Instance.PlaySound2D(negativeSound);
    };

    void OnDisable() => pressGesture.Pressed -= (s, e) => { }; 
}