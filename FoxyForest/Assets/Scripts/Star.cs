using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public Image starIcon;

    private void Awake() {
        starIcon = GetComponent<Image>();
    }
}
