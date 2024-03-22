using UnityEngine;

public class Rotato : MonoBehaviour
{
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    bool isRect = false;
    RectTransform rect;

    private void Start()
    {
        if (GetComponent<RectTransform>() != null)
        {
            rect = GetComponent<RectTransform>();
            isRect = true;
        }
    }

    private void Update()
    {
        if (!isRect)
            transform.eulerAngles += new Vector3(x, y, z) * Time.deltaTime;
        else
            rect.rotation = Quaternion.Euler(rect.eulerAngles + new Vector3(x, y, z) * Time.deltaTime);
    }
}