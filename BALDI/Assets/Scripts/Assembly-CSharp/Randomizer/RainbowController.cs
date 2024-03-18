using UnityEngine;

public class RainbowController : MonoBehaviour
{
    public Material rainbow;
    public float Dark = 1f;
    public float Bright = 1f;
    public float Speed = 1f;

    // Update is called once per frame
    void Update()
    {
        rainbow.SetFloat("_Dark", Dark);
        rainbow.SetFloat("_Bright", Bright);
        rainbow.SetFloat("_iTime", Time.fixedUnscaledTime);
        rainbow.SetFloat("_Speed", Speed);
    }
}
