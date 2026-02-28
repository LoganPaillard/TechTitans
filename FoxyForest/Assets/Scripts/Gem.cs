using UnityEngine;

public class Gem : Tappable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnMouseDown()
    {
        OnTapped();
    }
}
