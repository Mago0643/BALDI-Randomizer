using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x020000B1 RID: 177
public class Script : MonoBehaviour
{
    // Token: 0x0600091C RID: 2332 RVA: 0x00020AA5 File Offset: 0x0001EEA5
    private void Start()
	{
		ok = FindAnyObjectByType<ShakeSchool>();
        clipSampleData = new float[sampleDataLength];
    }

	// Token: 0x0600091D RID: 2333 RVA: 0x00020AA7 File Offset: 0x0001EEA7
	private void Update()
	{
		if (!this.audioDevice.isPlaying & this.played)
		{
			Application.Quit();
#if UNITY_EDITOR
            Debug.Break();
#endif
        }

        // https://discussions.unity.com/t/how-do-i-get-the-current-volume-level-amplitude-of-playing-audio-not-the-set-volume-but-how-loud-it-is/162556
        if (audioDevice.isPlaying)
        {
            currentUpdateTime += Time.unscaledDeltaTime;
            if (currentUpdateTime >= updateStep)
            {
                currentUpdateTime = 0f;
                audioDevice.clip.GetData(clipSampleData, audioDevice.timeSamples);
                clipLoudness = 0f;
                foreach (var sample in clipSampleData)
                {
                    clipLoudness += Mathf.Abs(sample);
                }
                clipLoudness /= sampleDataLength;
            }
        }
        ok.shakeAMP = clipLoudness / 2f;
        ok.glitchAMP = clipLoudness / 2f;
        ok.glitchOffset = clipLoudness / 2f;
    }

    public float updateStep = 0.01f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;
    // Token: 0x0600091E RID: 2334 RVA: 0x00020ACB File Offset: 0x0001EECB
    private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player" & !this.played)
		{
			this.audioDevice.Play();
			this.played = true;
            /*Tween ok2 = DOTween.To(() => ok.shakeAMP, val => ok.shakeAMP = val, 0.5f, audioDevice.clip.length);
			ok2.Play();*/
        }
    }

	// Token: 0x040005A7 RID: 1447
	public AudioSource audioDevice;
	ShakeSchool ok;

	// Token: 0x040005A8 RID: 1448
	private bool played;
}
