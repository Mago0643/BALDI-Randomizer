using UnityEngine;

public class Audio : MonoBehaviour
{
    private void Start()
    {
        AudioSource check = GetComponent<AudioSource>();
        if (check != null)
        {
            SetRandomPitch(check);
        }
    }

    public static AudioSource SetRandomPitch(AudioSource src)
    {
        // to prevent stack overflow
        int repeat = 0;
        float pitch = Random.Range(-0.5f, 2f);
        // idk why unity dosent play audio when pitch's 0
        while (pitch > -0.5 & pitch < 0.5)
        {
            pitch = Random.Range(-0.5f, 2f);
            repeat++;
            if (repeat >= 50)
            {
#if UNITY_EDITOR
                Debug.LogError("Stack Overflowed While choosing Audio.");
                Debug.Break();
#endif
                break;
            }
        }
        src.pitch = pitch;
        return src;
    }
}