using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 1f;
    public UnityEngine.Vector3 offset = new UnityEngine.Vector3(0,2,0);
        void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
    }
    
    public void SetText(int value)
    {
        TextMeshPro textObj = GetComponentInChildren<TextMeshPro>();
        if (value > 0)
        {
            textObj.text = "+" + value;
            textObj.color = new Color(0f, 0.6f, 0f, 1f);
        }
        else
        {
            textObj.text = value.ToString();
            textObj.color = Color.red;
        }
    }
}
