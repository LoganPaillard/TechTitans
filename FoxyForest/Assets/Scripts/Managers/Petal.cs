using UnityEngine;

public class Petal : MonoBehaviour
{
    public SpriteRenderer icon;

    private void Awake() {
        icon = GetComponent<SpriteRenderer>();
    }
}
