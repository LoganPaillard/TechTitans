using UnityEngine;
using TMPro;

public class ScoreConnector : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

        if (GameManager.Instance != null && scoreText != null)
        {
            GameManager.Instance.scoreText = scoreText;
        }
        else
        {
            Debug.LogError("GameManager instance not found. Score will not work.");
        }
    }
}
