using UnityEngine;
using UnityEngine.UI;

public class TimerConnector : MonoBehaviour
{
    private Image timerCircleImage;

    void Awake()
    {
        timerCircleImage = GetComponent<Image>();

        if (GameManager.Instance != null && timerCircleImage != null)
        {
            GameManager.Instance.timerCircleImage = timerCircleImage;
        }
        else
        {
            Debug.LogError("GameManager instance not found or timerCircleImage is null. Timer will not work.");
        }
    }
}