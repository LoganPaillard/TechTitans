using UnityEngine;

public class Tappable : MonoBehaviour
{
    [SerializeField] public int scoreValue = 0; // Score value for tapping this object

    public void OnTapped()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameRunning)
        {
            GameManager.Instance.AddScore(scoreValue);
            gameObject.SetActive(false); // Deactivate the object after tapping
        }
    }
}
