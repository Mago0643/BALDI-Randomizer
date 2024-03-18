using UnityEngine;

public class Vineboom : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip vineboom;
    
    bool canExit = false;
    private void Update()
    {
        if (!aud.isPlaying & canExit)
        {
            Application.Quit();
#if UNITY_EDITOR
            Debug.Break();
#endif
        }
    }

    public void ExitThing()
    {
        aud.clip = vineboom;
        aud.Play();
        canExit = true;
    }
}