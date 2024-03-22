using System.Collections;
using DG.Tweening;
using UnityEngine;

// the file came from my private mod
public class FireInTheHole : MonoBehaviour
{
    Tween shakeTween;
    // SubtitleManager sm;
    AudioSource src;
    public BaldiScript baldi;
    public bool isOrigin = true;
    public GameControllerScript gc;
    public float offset = 2.5f;
    public float shakeAMP = 2.5f;
    public GameObject explosion;
    bool fuck = false;
    public GameObject[] windows;
    public Material brokenWindow;
    public AudioClip windowShatter;
    public CameraScript plrCam;
    public PlayerScript playerSrc;
    public AudioClip BANG;

    private void Start()
    {
        src = GetComponent<AudioSource>();
        // windows = GameObject.FindGameObjectsWithTag("Window");
        // sm = FindAnyObjectByType<SubtitleManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name != "Player" & !isOrigin & !fuck)
        {
            fuck = true;
            // *BOOM*
            src.Play();
            Transform offset = base.transform;
            offset.position = new Vector3(offset.position.x, offset.position.y + this.offset, offset.position.z);
            GameObject obj = Instantiate(explosion, offset);
            obj.GetComponent<Animator>().Play(0);
            src.PlayOneShot(BANG);
            // its stupid
            /*if (collision.gameObject.name.Contains("Machine"))
            {
                string name = collision.transform.name.Replace("Machine", "").ToLower();
                int itemID = 0;
                switch (name)
                {
                    case "bsoda":
                        itemID = 4;
                        break;
                    case "zesty":
                        itemID = 1;
                        break;
                }

                if (itemID > 0)
                {
                    if (baldi.isActiveAndEnabled) baldi.Hear(base.transform.position, 90f);
                    if (UnityEngine.Random.Range(0, 100) >= 80)
                        gc.CollectItem(itemID);
                    // collision.gameObject.GetComponent<AudioSource>().PlayOneShot(windowShatter);
                    // collision.gameObject.GetComponent<AudioSource>().PlayOneShot(BANG);
                }
            }*/
                if (baldi.isActiveAndEnabled)
                    baldi.Hear(transform.position, 5f);

            shakeTween = DOTween.To(() => shakeAMP, value => plrCam.shakeAmp = value, 0f, src.clip.length);

            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(GetComponentInChildren<SpriteRenderer>());
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(src.clip.length);
        Destroy(gameObject);
    }
}
