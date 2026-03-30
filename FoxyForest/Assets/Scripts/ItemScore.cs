using UnityEngine;
using TMPro;

public class ItemScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Awake() {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
}
