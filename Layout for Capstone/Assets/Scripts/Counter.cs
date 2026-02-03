using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    private int count = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counterText.text = count.ToString();
    }

    // Update is called once per frame
    public void IncreaseCounter()
    {
        count++;
        counterText.text = count.ToString();
    }

    public void DecreaseCounter()
    {
        count--;
        counterText.text = count.ToString();
    }
}
