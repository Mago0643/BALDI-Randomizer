using UnityEngine;

public class TitleGlitch : MonoBehaviour
{
    public AudioSource Player;
    public GameObject EndlessText;
    public GameObject PlayMode;
    float stopplayin = 0f;
    bool canDestory = false;
    public void DoGlitching()
    {
        if (PlayMode.activeSelf)
        {
            EndlessText.SetActive(false);
            Player.time = Random.Range(0f, Player.clip.length - 1f);
            Player.Play();
            stopplayin = 1f;
            canDestory = true;
            EndlessText.transform.position = new Vector3(1000f, 1000f, 1000f);
            transform.position = new Vector3(1000f, 1000f, 1000f);
        }
    }

    private void Update()
    {
        if (canDestory & Player.isPlaying & stopplayin > 0f)
        {
            stopplayin -= Time.unscaledDeltaTime;
        }

        if (stopplayin <= 0f & canDestory)
        {
            Player.Stop();
            gameObject.SetActive(false);
        }
    }
}
