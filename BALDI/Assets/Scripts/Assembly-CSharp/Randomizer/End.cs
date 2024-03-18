using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public Image blackThing;
    public Camera cam;
    public AudioSource aud;
    public SpriteRenderer sprite;
    void Start()
    {
        blackThing.DOColor(new Color(0f, 0f, 0f, 0f), 0.5f);
        cam.transform.DOMoveZ(-10f, 1f).SetEase(Ease.OutBack);
    }

    // Update is called once per frame
    void Update()
    {
        if (aud.isPlaying & aud.time >= 7.666f)
        {
            cam.transform.position = new Vector3(
                Random.Range(-2.5f, 2.5f),
                Random.Range(-2.5f, 2.5f),
                -10f
            );
            sprite.material.SetFloat("_Speed", 4f);
        }
        sprite.material.SetFloat("_iTime", Time.fixedTime);
    }
}
