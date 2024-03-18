using DG.Tweening;
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
        transform.DOKill(false);
        transform.localScale = new Vector3(10f, 10f, 1f);
        transform.DOScale(new Vector3(1f, 1f, 1f), vineboom.length).SetEase(Ease.OutExpo);
    }
}