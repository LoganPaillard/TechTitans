using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightController : MonoBehaviour
{
    public Light2D globalLight;
    public Gradient dayNightGradient;

    public void UpdateLight(float timeOfDay)
    {
        globalLight.color = dayNightGradient.Evaluate(timeOfDay);
    } 
}
